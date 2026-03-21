import { Component } from '@angular/core';
import {LinkShortenerForm} from "../../common-ui/link-shortener-form/link-shortener-form";
import {LinksDashboard} from "../../common-ui/links-dashboard/links-dashboard";
import {LoginButton} from '../../common-ui/login-button/login-button';
import {LoginModal} from '../../common-ui/login-modal/login-modal';

@Component({
  selector: 'app-main-page',
    imports: [
        LinkShortenerForm,
        LinksDashboard,
        LoginButton,
        LoginModal
    ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
  isLoginModalOpen = false;

  openLoginModal() {
    this.isLoginModalOpen = true;
  }

  closeLoginModal() {
    this.isLoginModalOpen = false;
  }

  onGoogleLoginSelected() {
    // TODO: Replace with Google auth redirect/call when backend integration is ready.
    console.log('Google login selected');
    this.closeLoginModal();
  }
}
