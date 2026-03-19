import { Routes } from '@angular/router';
import {MainPage} from './pages/main-page/main-page';
import {AuthRedirectPage} from './pages/auth-redirect-page/auth-redirect-page';

export const routes: Routes = [
  {path: '', component: MainPage},
  {path: 'auth/google', component: AuthRedirectPage}
];
