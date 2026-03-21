import {inject, Injectable} from '@angular/core';
import {catchError, Observable, throwError} from 'rxjs';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class GoogleOauthService {
  private httpClient = inject(HttpClient)
  private baseUrl = 'http://localhost:5100/google';

  callback(request: GoogleOAuthCallbackRequest) : Observable<TokenValidationResultResponse> {
    return this.httpClient.post<TokenValidationResultResponse>(this.baseUrl + '/callback', request)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }
}
