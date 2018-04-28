import { Component } from '@angular/core';
import { BaseComponent } from './components/base.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    title = 'app';

    get showProgress() {
        return BaseComponent.progressIsShown;
    }
}
