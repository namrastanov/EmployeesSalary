import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base.component';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Employee } from '../../models/employee';
import { ApiResponse } from '../../models/api-response';

@Component({
    selector: 'app-employee',
    templateUrl: './employee.component.html',
    styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent extends BaseComponent implements OnInit {
    id: string;
    private sub: any;

    employee: Employee;
    editEmployeeForm: FormGroup;
    submitted: boolean = false;

    constructor(
        private route: ActivatedRoute,
        public employeeService: EmployeeService,
        fb: FormBuilder,
        public routeParams: ActivatedRoute,
        private router: Router
    ) {
        super(employeeService);

        this.employee = new Employee();

        this.editEmployeeForm = fb.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            salary: ['', Validators.required]
        });

    }

    ngOnInit() {
        var data = this.routeParams.snapshot.data;
        if (data) {
            this.employee = <Employee>data['event'];
        }
    }

    onSubmit() {
        if (!this.editEmployeeForm.valid) {
            this.submitted = false;
            return;
        }
        super.showProgress();

        if (this.employee.id) {
            this.update()
        } else {
            this.add();
        }

        this.submitted = true;
    }

    update() {
        var self = this;

        this.employeeService.updateEmployee(this.employee).subscribe(
            response => {

                self.router.navigate(['/']);
            },
            error => {
                super.hideProgress();
                this.errorProcess(error);
            });
    }

    add() {
        var self = this;

        this.employeeService.addEmployee(this.employee).subscribe(
            response => {

                self.router.navigate(['/']);
            },
            error => {
                super.hideProgress();
                this.errorProcess(error);
            });
    }

}
