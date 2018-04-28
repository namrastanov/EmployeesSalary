import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ApiResponse } from '../models/api-response';

const SIZE_LIMIT = 5000;

@Injectable()
export class FileUploaderService {

    private _headers: { [name: string]: any; };

    /**
     * @param Observable<number>
     */
    public progress$: Observable<{}>;

    /**
     * @type {number}
     */
    public progress: number = 0;

    public progressObserver: any;

    constructor() {
        this.progress$ = new Observable(observer => {
            this.progressObserver = observer;
        });
    }

    setHeaders(headers: { [name: string]: any; }) {
        this._headers = headers;
    }

    /**
     * @returns {Observable<number>}
     */
    public getObserver(): Observable<{}> {
        return this.progress$;
    }

    public validator(file: File, size: number): string {
        let message = null;
        if (file.size == 0) {
            message = "errors.file-empty";
        }
        if ((file.size / 1024) >= size) {
            message = "errors.file-too-long";
        }
        return message;
    }

    /**
     * Upload files through XMLHttpRequest
     *
     * @param url
     * @param files
     * @returns {Promise<T>}
     */
    public upload(url: string, files: File[], size?: number): Promise<ApiResponse> {
        let apiResponse = new ApiResponse();

        for (let i = 0; i < files.length; i++) {
            let validator = this.validator(files[i], size || SIZE_LIMIT)
            if (validator != null) {
                apiResponse.message = validator;
                return new Promise((resolve, reject) => { reject(apiResponse); });
            };
        };

        return new Promise((resolve, reject) => {
            let formData: FormData = new FormData(),
                xhr: XMLHttpRequest = new XMLHttpRequest();

            for (let i = 0; i < files.length; i++) {
                formData.append("uploads[]", files[i], files[i].name);
            }

            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4) {
                    if (xhr.status === 200) {
                        apiResponse = <ApiResponse>JSON.parse(xhr.response);
                        apiResponse.status = xhr.status;
                        resolve(apiResponse);
                    } else {
                        if (xhr.status === 0) {
                            apiResponse.message = "Server error.";
                        }
                        else {
                            if (typeof JSON.parse(xhr.response).Message !== "undefined") {
                                apiResponse = <ApiResponse>JSON.parse(xhr.response);
                            }
                            else {
                                apiResponse.message = xhr.responseText;
                            }
                        }
                        apiResponse.status = xhr.status;
                        console.log("error", apiResponse);
                        reject(apiResponse);
                    }
                }
            };

            xhr.open('POST', url, true);

            var token = this.getToken();
            if (token) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token);
            }

            if (this._headers) {
                for (let key in this._headers) {
                    xhr.setRequestHeader(key, this._headers[key]);
                }
            }

            xhr.send(formData);
        });
    }

    getToken(): string {
        return localStorage.getItem("_jwt");
    }

}
