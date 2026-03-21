import {Component, input, output} from '@angular/core';
import {NgOptimizedImage} from '@angular/common';

@Component({
  selector: 'app-user-menu',
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.scss',
  imports: [
    NgOptimizedImage
  ]
})
export class UserMenu {
  userName = input('');
  userPicture = input('');
  isOpen = input(false);

  toggleRequested = output<void>();
  logoutRequested = output<void>();

  onToggleClick() {
    this.toggleRequested.emit();
  }

  onLogoutClick() {
    this.logoutRequested.emit();
  }
}
