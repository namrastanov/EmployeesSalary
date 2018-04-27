import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './components/main/main.component';
import { ListComponent } from './components/list/list.component';

const appRoutes: Routes = [
  { path: '', component: MainComponent },
  { path: 'list', component: ListComponent }
];

export const routing = RouterModule.forRoot(appRoutes);
