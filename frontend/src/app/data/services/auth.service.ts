import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private authData = new BehaviorSubject<TokenValidationResultResponse | null>(null)
  authData$ = this.authData.asObservable()

  setData(data: TokenValidationResultResponse) : void {
    this.authData.next(data)
  }
}
