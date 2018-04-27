import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { FileUploaderService } from './file-uploader.service';

@Injectable()
export class EmployeeService {

    public _uploadFileUrl: string = 'api/employee/upload/';

    constructor(
        private dataService: DataService,
        private fileService: FileUploaderService) { }

    sendFile(files: File[]) {
        return this.fileService.upload(this._uploadFileUrl, files);
    }


}
