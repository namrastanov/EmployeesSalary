import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { FileUploaderService } from './file-uploader.service';
import { Employee } from '../models/employee';

@Injectable()
export class EmployeeService {

    public _getEmployeeUrl: string = 'api/employee';
    public _getEmployeesUrl: string = 'api/employee/list';
    public _uploadFileUrl: string = 'api/employee/upload';
    public _addEmployeeUrl: string = 'api/employee';
    public _deleteEmployeeUrl: string = 'api/employee';
    public _updateEmployeeUrl: string = 'api/employee/update';
    public _getTotalSalaryUrl: string = 'api/employee/totalSalary';

    constructor(
        private dataService: DataService,
        private fileService: FileUploaderService) { }

    getEmployee(id: any) {
        if (!id) {
            return;
        }

        this.dataService.set(`${this._getEmployeeUrl}/${id}`);
        return this.dataService.get();
    }

    addEmployee(employee: Employee) {
        let params: URLSearchParams = new URLSearchParams();

        if (employee.firstName)
            params.set('firstName', employee.firstName);

        if (employee.lastName)
            params.set('lastName', employee.lastName);

        if (employee.phoneNumber)
            params.set('phoneNumber', employee.phoneNumber);

        if (employee.salary)
            params.set('salary', employee.salary.toString());

        this.dataService.set(`${this._addEmployeeUrl}?${params.toString()}`);
        return this.dataService.post();
    }

    updateEmployee(employee: Employee) {
        let params: URLSearchParams = new URLSearchParams();

        if (employee.id)
            params.set('id', employee.id);

        if (employee.firstName)
            params.set('firstName', employee.firstName);

        if (employee.lastName)
            params.set('lastName', employee.lastName);

        if (employee.phoneNumber)
            params.set('phoneNumber', employee.phoneNumber);

        if (employee.salary)
            params.set('salary', employee.salary.toString());

        this.dataService.set(`${this._updateEmployeeUrl}?${params.toString()}`);
        return this.dataService.post();
    }

    deleteEmployee(id) {
        this.dataService.set(`${this._deleteEmployeeUrl}/${id}`);
        return this.dataService.delete();
    }

    getEmployees(page) {
        this.dataService.set(`${this._getEmployeesUrl}/${page}`);
        return this.dataService.get();
    }

    getTotalSalary() {
        this.dataService.set(`${this._getTotalSalaryUrl}`);
        return this.dataService.get();
    }

    sendFile(files: File[]) {
        return this.fileService.upload(this._uploadFileUrl, files);
    }
}
