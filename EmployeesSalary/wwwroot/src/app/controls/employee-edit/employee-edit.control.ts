import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Employee } from '../../models/employee';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { BaseComponent } from '../../components/base.component';

@Component({
    selector: 'employee-edit',
    templateUrl: './employee-edit.control.html',
    styleUrls: ['./employee-edit.control.scss']
})
export class EmployeeEditControl extends BaseComponent implements OnInit {

    @Input() employee: Employee;
    @Output() onFinish: EventEmitter<any> = new EventEmitter<any>();

    editEmployeeForm: FormGroup;
    submitted: boolean;

    constructor(
        public employeeService: EmployeeService,
        fb: FormBuilder) {
        super(employeeService);

        this.editEmployeeForm = fb.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            phoneNumber: ['', Validators.required],
            salary: ['', Validators.required]
        });
    }

    ngOnInit() {

    }

    saveEmployee() {
        if (!this.editEmployeeForm.valid) {
            this.submitted = true;
            return;
        }
        super.showProgress();

        this.onFinish.emit(true);
        this.submitted = false;
    }
}
