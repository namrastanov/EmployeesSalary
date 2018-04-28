import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BaseComponent } from './components/base.component';
import { MainComponent } from './components/main/main.component';
import { ImportComponent } from './components/import/import.component';
import { FilesComponent } from './components/files/files.component';
import { DataService } from './services/data.service';
import { routing } from './app.routes';
import { EmployeeService } from './services/employee.service';
import { FileUploaderService } from './services/file-uploader.service';
import { HttpClientModule } from '@angular/common/http';

import {
    MatToolbarModule,
    MatButtonModule,
    MatPaginatorModule,
    MatTableModule,
    MatProgressBarModule,
    MatFormFieldModule,
    MatInputModule
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SalaryRatioPipe } from './pipes/salary-ratio.pipe';
import { EmployeeEditControl } from './controls/employee-edit/employee-edit.control';
import { EmployeeComponent } from './components/employee/employee.component';
import { EmployeeResolve } from './resolves/employee.resolve';


@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatToolbarModule,
        MatButtonModule,
        MatProgressBarModule,
        MatPaginatorModule,
        MatTableModule,
        MatFormFieldModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        routing
    ],
    declarations: [
        AppComponent,
        MainComponent,
        ImportComponent,
        FilesComponent,
        BaseComponent,
        SalaryRatioPipe,
        EmployeeEditControl,
        EmployeeComponent
    ],
    providers: [
        DataService,
        EmployeeService,
        FileUploaderService,
        EmployeeResolve
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
