import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { ApiResponse } from '../../models/api-response';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  showLoading: boolean = false;

  constructor(
    private employeeService: EmployeeService
  ) { }

  ngOnInit() {

  }

  showProgress() {
    this.showLoading = true;
  }

  hideProgress() {
    this.showLoading = true;
  }

  uploadFile(item: any) {

    this.showProgress();
    var result = this.employeeService.sendFile(item.target.files);
    var self = this;
    result.then(r => {
      var uploadResult = <ApiResponse>r;
      if (uploadResult.Data) {
        //this.event.PartnerUrl = <string>uploadResult.Data;
      }
      self.hideProgress();
    }).catch(err => {
      self.hideProgress();
      alert(err.Message);
    });
  }

}
