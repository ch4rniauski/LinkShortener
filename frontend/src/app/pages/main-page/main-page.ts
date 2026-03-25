import {Component, inject, OnInit} from '@angular/core';
import {LinkShortenerForm} from "../../common-ui/link-shortener-form/link-shortener-form";
import {LinksDashboard} from "../../common-ui/links-dashboard/links-dashboard";
import {LoginButton} from '../../common-ui/login-button/login-button';
import {LoginModal} from '../../common-ui/login-modal/login-modal';
import {UserMenu} from '../../common-ui/user-menu/user-menu';
import {GoogleOauthService} from '../../data/services/google-oauth.service';
import {ActivatedRoute} from '@angular/router';
import {filter} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {AuthService} from '../../data/services/auth.service';

@Component({
  selector: 'app-main-page',
    imports: [
        LinkShortenerForm,
        LinksDashboard,
        LoginButton,
        LoginModal,
        UserMenu
    ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage implements OnInit {
  isLoginModalOpen = false;
  isAuthorized = false;
  isUserMenuOpen = false;
  userName = '';
  userPicture = '';

  private googleOAuthService = inject(GoogleOauthService)
  private authService = inject(AuthService)
  private route = inject(ActivatedRoute)

  ngOnInit(): void {
    this.handleOAuthCallback()
    this.subscribeToAuth()
  }

  openLoginModal() {
    this.isLoginModalOpen = true;
  }

  closeLoginModal() {
    this.isLoginModalOpen = false;
  }

  toggleUserMenu() {
    this.isUserMenuOpen = !this.isUserMenuOpen;
  }

  onLogoutClick() {
    console.log('Logout clicked');
    this.isUserMenuOpen = false;
  }

  onGoogleLoginSelected() {
    this.googleOAuthService.redirectToGoogleOAuth()
  }

  private handleOAuthCallback() : void {
    this.route.queryParams
      .pipe(filter(params => params['code']))
      .subscribe(params => {
        const code = params['code']
        const request : GoogleOAuthCallbackRequest = {
          code: code
        }

        this.googleOAuthService.callback(request)
          .subscribe({
            next: (response) => {
              this.authService.setData(response)
            },
            error: (error: HttpErrorResponse) => {
              console.error(error)
            }
          })
      })
  }

  private subscribeToAuth() : void {
    this.authService.authData$.subscribe((authData: TokenValidationResultResponse | null) => {
      if (authData) {
        this.isAuthorized = true;
        this.userName = authData.name;
        this.userPicture = authData.picture;
        this.isLoginModalOpen = false;
        return;
      }

      this.isAuthorized = false;
      this.userName = '';
      this.userPicture = '';
      this.isUserMenuOpen = false;
    })
  }
}
