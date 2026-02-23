import {Component, inject} from '@angular/core';
import {LinkShortenerService} from '../../data/services/link-shortener.service';
import {DatePipe, SlicePipe} from '@angular/common';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-links-dashboard',
  imports: [
    SlicePipe,
    DatePipe
  ],
  templateUrl: './links-dashboard.html',
  styleUrl: './links-dashboard.scss',
})
export class LinksDashboard {
  private linkShortenerService = inject(LinkShortenerService);

  private page = 1;
  private readonly pageSize = 15;

  links: GetShortLinkResponse[] = []

  baseUrl = 'http://localhost:5100/links';

  ngOnInit() {
    this.linkShortenerService.getShortLinksByPage(this.page, this.pageSize)
      .subscribe({
        next: data => {
          this.links = data;
        },
        error: (error: HttpErrorResponse) => {
          console.error(error);
        }
      });
  }

  // ✏️ Редактирование ссылки
  editLink(id: string) {
    console.log('Редактировать ссылку:', id);
    // TODO: Открыть модалку/страницу редактирования
  }

  // 🗑️ Удаление ссылки
  deleteLink(id: string) {
    if (confirm('Удалить эту ссылку?')) {
      // TODO: Вызов API удаления
      // this.linkShortenerService.deleteLink(id).subscribe({
      //   next: () => {
      //     this.loadLinks();  // Перезагрузить таблицу
      //   }
      // });
      console.log('Удалить ссылку:', id);
    }
  }
}
