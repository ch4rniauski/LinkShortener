import {Component, HostListener, input, output} from '@angular/core';

@Component({
  selector: 'app-login-modal',
  templateUrl: './login-modal.html',
  styleUrl: './login-modal.scss',
})
export class LoginModal {
  isOpen = input(false);

  closed = output<void>();
  googleSelected = output<void>();

  @HostListener('document:keydown.escape')
  onEscapePress() {
    if (this.isOpen()) {
      this.closed.emit();
    }
  }

  onBackdropClick(event: MouseEvent) {
    if (event.target === event.currentTarget) {
      this.closed.emit();
    }
  }

  onCloseClick() {
    this.closed.emit();
  }

  onGoogleClick() {
    this.googleSelected.emit();
  }
}
