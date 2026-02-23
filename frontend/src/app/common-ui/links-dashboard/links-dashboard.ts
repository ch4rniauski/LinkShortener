import {Component, inject, signal} from '@angular/core';
import {LinkShortenerService} from '../../data/services/link-shortener.service';
import {DatePipe, SlicePipe} from '@angular/common';

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

  // 📊 Данные (единственное что осталось)
  allLinks = signal<ShortLink[]>([]);

  baseUrl = 'http://localhost:5100';

  ngOnInit() {
    this.loadLinks();
  }

  // 📥 Загрузка данных (ЗАМЕНИТЕ НА API)
  loadLinks() {
    // TODO: Заменить на реальный API вызов
    // this.linkShortenerService.getLinks().subscribe({
    //   next: (response) => {
    //     this.allLinks.set(response);
    //   }
    // });

    // ✅ Мок данные для демонстрации
    this.allLinks.set([
      {
        id: 1,
        originalUrl: 'https://very-long-url-example.com/very/very/long/path/to/some/resource',
        shortToken: 'abc123',
        createdAt: new Date('2026-02-23T10:30:00'),
        clicks: 45
      },
      {
        id: 2,
        originalUrl: 'https://github.com/username/awesome-project',
        shortToken: 'xyz789',
        createdAt: new Date('2026-02-23T09:15:00'),
        clicks: 12
      }
    ]);
  }

  // ✏️ Редактирование ссылки
  editLink(id: number) {
    console.log('Редактировать ссылку:', id);
    // TODO: Открыть модалку/страницу редактирования
  }

  // 🗑️ Удаление ссылки
  deleteLink(id: number) {
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
