import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { ApiResponse } from '../../models/api-response';
import { BaseComponent } from '../base.component';

@Component({
    selector: 'app-import',
    templateUrl: './import.component.html',
    styleUrls: ['./import.component.scss']
})
export class ImportComponent extends BaseComponent implements OnInit {

    constructor(
        public employeeService: EmployeeService
    ) {

        super(employeeService);
    }

    ngOnInit() {

    }

    uploadFile(item: any) {
        this.showProgress();
        var result = this.employeeService.sendFile(item.target.files);
        var self = this;
        result.then(r => {
            var uploadResult = <ApiResponse>r;
            if (uploadResult.data) {
                //this.event.PartnerUrl = <string>uploadResult.Data;
            }
            self.hideProgress();
        }).catch(err => {
            self.hideProgress();
            alert(err.Message);
        });
    }

}
