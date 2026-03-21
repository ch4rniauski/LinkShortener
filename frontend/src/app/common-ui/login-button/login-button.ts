import {Component, output} from '@angular/core';

@Component({
  selector: 'app-login-button',
  templateUrl: './login-button.html',
  styleUrl: './login-button.scss',
})
export class LoginButton {
  openRequested = output<void>();

  onOpenClick() {
    this.openRequested.emit();
  }
}
