import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { ImportComponent } from './components/import/import.component';
import { FilesComponent } from './components/files/files.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { EmployeeResolve } from './resolves/employee.resolve';

const appRoutes: Routes = [
    { path: '', component: MainComponent },
    { path: 'import', component: ImportComponent },
    { path: 'files', component: FilesComponent },
    { path: 'employee', component: EmployeeComponent },
    { path: 'employee/:id', component: EmployeeComponent, resolve: { event: EmployeeResolve } }
];

export const routing = RouterModule.forRoot(appRoutes);
