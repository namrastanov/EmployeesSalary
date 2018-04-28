import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'salaryRatio'
})
export class SalaryRatioPipe implements PipeTransform {

    transform(value: any, arg: number): any {
        return value / arg;
    }

}
