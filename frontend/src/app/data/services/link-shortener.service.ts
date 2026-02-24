import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LinkShortenerService {
  private httpClient = inject(HttpClient);
  private baseUrl = 'http://localhost:5100/links';

  shortTheLink(request: ShortTheLinkRequest) : Observable<ShortTheLinkResponse> {
    return this.httpClient.post<ShortTheLinkResponse>(`${this.baseUrl}/shortener`, request)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      )
  }

  getShortLinksByPage(page: number, pageSize: number): Observable<GetShortLinksByPageResponse> {
    return this.httpClient.get<GetShortLinksByPageResponse>(`${this.baseUrl}?page=${page}&pageSize=${pageSize}`)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      );
  }

  updateLongLink(request: UpdateLongLinkRequestInterface, id: string): Observable<UpdateLongLinkResponseInterface> {
    return this.httpClient.patch<UpdateLongLinkResponseInterface>(`${this.baseUrl}/${id}`, request)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      );
  }

  deleteLink(id: string): Observable<DeleteShortLinkResponse> {
    return this.httpClient.delete<DeleteShortLinkResponse>(`${this.baseUrl}/${id}`)
      .pipe(
        catchError(
          (error: HttpErrorResponse) => throwError(() => error)
        )
      );
  }
}
