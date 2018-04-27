import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { MainComponent } from './components/main/main.component';
import { DataService } from './services/data.service';
import { ListComponent } from './components/list/list.component';
import { routing } from './app.routes';
import { EmployeeService } from './services/employee.service';
import { FileUploaderService } from './services/file-uploader.service';
import { BaseComponent } from './components/base/base.component';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        routing
    ],
    declarations: [
        AppComponent,
        MainComponent,
        ListComponent,
        BaseComponent
    ],
    providers: [
        DataService,
        EmployeeService,
        FileUploaderService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
