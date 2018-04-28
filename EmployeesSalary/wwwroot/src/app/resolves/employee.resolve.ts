import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { Employee } from "../models/employee";
import { Observable } from "rxjs/Observable";
import { EmployeeService } from "../services/employee.service";
import { ApiResponse } from "../models/api-response";


@Injectable()
export class EmployeeResolve implements Resolve<Employee> {
    constructor(
        public employeeService: EmployeeService,
        public router: Router) {
    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Employee> | Promise<Employee> | any {
        var id = route.params['id'];

        var request = this.employeeService.getEmployee(id);

        var result = new Observable(method => {
            request.subscribe(response => {
                var employeeResult = <ApiResponse>response;

                if (employeeResult.data != null) {
                    method.next(<Employee>employeeResult.data);
                    method.complete();
                }
            },
                error => {
                    method.error(error);
                    console.log(error);
                    this.router.navigate(['/']);
                });
        });
        return result;
    }
}
