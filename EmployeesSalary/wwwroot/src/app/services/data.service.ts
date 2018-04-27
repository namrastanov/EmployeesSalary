import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DataService {

    public _pageSize: number;
    public _baseUri: string;
    private _headers: { [name: string]: any; };

    constructor(public http: HttpClient) {

    }

    public dataProcess(data: any) {
        return new Observable<any>(data);
    }

    set(baseUri: string, pageSize?: number): void {
        this._baseUri = baseUri;
        this._pageSize = pageSize;
    }

    setHeaders(headers: { [name: string]: any; }) {
        this._headers = headers;
    }

    get(): Observable<any> {
        var uri = this._baseUri;

        return this.http.get(uri, this.getOptions());
    }

    post(data?: any, mapJson: boolean = true): Observable<any> {
        if (mapJson)
            return this.http.post(this._baseUri, data, this.getOptions());
        else
            return this.http.post(this._baseUri, data, this.getOptions());
    }

    postFormUrlencoded(data: string) {
        var httpOptions = {
            headers: new HttpHeaders({
                'Accept-Language': this.getCulture(),
                'Content-Type': "application/x-www-form-urlencoded"
            })
        }

        return this.http.post<any>(this._baseUri, data, httpOptions);
    }

    put(data?: any, mapJson: boolean = true): Observable<any> {
        if (mapJson)
            return this.http.put(this._baseUri, data, this.getOptions());
        else
            return this.http.put(this._baseUri, data, this.getOptions());
    }

    delete(id?: number): Observable<any> {
        if (id) {
            return this.http.delete(this._baseUri + '/' + id.toString(), this.getOptions());
        }

        return this.http.delete(this._baseUri, this.getOptions());
    }

    deleteResource(resource: string) {
        return this.http.delete(resource, this.getOptions());
    }

    getOptions(): any {

        const httpOptions = {
            headers: new HttpHeaders({
                'Accept-Language': this.getCulture(),
                'Content-Type': "application/json"
            })
        }

        var token = this.getToken();
        if (token) {
            httpOptions.headers = httpOptions.headers.set('Authorization', `Bearer ${token}`);
        }

        if (this._headers) {
            for (let key in this._headers) {
                httpOptions.headers = httpOptions.headers.set(key, this._headers[key]);
            }
        }
        return httpOptions;
    }

    getCulture(): string {
        return localStorage.getItem('culture') || 'ru';
    }

    getToken(): string {
        return localStorage.getItem("_jwt");
    }

}
