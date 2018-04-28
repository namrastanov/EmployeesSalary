import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base.component';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../../services/employee.service';
import { FormBuilder } from '@angular/forms';
import { ApiResponse } from '../../models/api-response';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss']
})
export class MainComponent extends BaseComponent implements OnInit {

    employees: Employee[] = [];
    totalSalary: number = 0;

    empty: boolean = false;
    page: number = 1;
    totalPages: number = 0;
    totalItems: number = 0;

    constructor(
        public employeeService: EmployeeService,
        private formBuilder: FormBuilder
    ) {
        super(employeeService);
    }

    ngOnInit() {

        this.fetch();
    }

    fetch() {
        this.page = 1;
        this.fetchEmployees();
    }

    fetchEmployees() {
        super.showProgress();

        this.employeeService
            .getEmployees(this.page)
            .subscribe(response => {
                var result = <ApiResponse>response;

                this.totalSalary = result.data.totalSalary;
                this.employees = <Employee[]>result.data.employeesList.items;

                this.empty = result.data.employeesList.totalCount === 0;
                this.totalPages = result.data.employeesList.totalPages;
                this.totalItems = result.data.employeesList.totalCount;

                super.hideProgress();
            },
            error => {
                super.hideProgress();
                this.errorProcess(error);
            });
    }

    filterByPage(page) {
        if (page === this.page) {
            return;
        }

        this.page = page;
        this.fetchEmployees();
    }

    deleteEmployee(id: any) {
        if (window.confirm('Are sure you want to delete this item?')) {
            var self = this;

            this.showProgress();
            this.employeeService.deleteEmployee(id).subscribe(
                response => {

                    self.fetchEmployees();

                super.hideProgress();
                },
                error => {
                    super.hideProgress();
                    this.errorProcess(error);
                });
        }
        
    }
}
