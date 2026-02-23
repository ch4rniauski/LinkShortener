import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class LinkShortenerService {
  private httpClient = inject(HttpClient);
  private baseUrl = 'http://localhost:5100/links/';

  shortTheLink(originalUrl: string) : string {

  }
}
