import {Component, signal} from '@angular/core';
import {LinkShortenerForm} from './common-ui/link-shortener-form/link-shortener-form';

@Component({
  selector: 'app-root',
  imports: [
    LinkShortenerForm
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('frontend');
}
