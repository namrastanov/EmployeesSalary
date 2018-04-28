import { Component, OnInit } from '@angular/core';
import { ApiResponse } from '../models/api-response';
import { EmployeeService } from '../services/employee.service';

@Component({
    selector: 'app-base',
    template: ''
})
export class BaseComponent {

    totalSalary: number = 0;
    public static progressIsShown: boolean = false;

    constructor(public employeeService: EmployeeService) { }

    showProgress() {
        BaseComponent.progressIsShown = true;
    }

    hideProgress() {
        BaseComponent.progressIsShown = false;
    }

    errorProcess(errorResponse: any) {
        this.hideProgress();

        console.log("Error: ", errorResponse);

        return null;
    }

    refreshTotalSalary() {
        this.showProgress();
        this.employeeService.getTotalSalary().subscribe(response => {
            var result = <ApiResponse>response;
            this.totalSalary = result.data;
        });
    }
}
