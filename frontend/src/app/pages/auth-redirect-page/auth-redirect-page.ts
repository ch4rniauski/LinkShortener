import {Component, inject, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {filter} from 'rxjs';
import {GoogleOauthService} from '../../data/services/google-oauth.service';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-auth-redirect-page',
  imports: [],
  templateUrl: './auth-redirect-page.html',
  styleUrl: './auth-redirect-page.scss',
})
export class AuthRedirectPage implements OnInit {
  private route = inject(ActivatedRoute)
  private googleOAuthService = inject(GoogleOauthService)

  ngOnInit(): void {
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
                console.log(response)
              },
              error: (error: HttpErrorResponse) => {
                console.error(error);
              }
            })
        })
  }
}
