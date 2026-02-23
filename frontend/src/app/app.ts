import {Component, signal} from '@angular/core';
import {LinkShortenerForm} from './common-ui/link-shortener-form/link-shortener-form';
import {LinksDashboard} from './common-ui/links-dashboard/links-dashboard';

@Component({
  selector: 'app-root',
  imports: [
    LinkShortenerForm,
    LinksDashboard
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('frontend');
}
