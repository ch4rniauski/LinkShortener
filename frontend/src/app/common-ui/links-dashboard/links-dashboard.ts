import {Component, inject} from '@angular/core';
import {LinkShortenerService} from '../../data/services/link-shortener.service';
import {DatePipe, SlicePipe} from '@angular/common';
import {HttpErrorResponse} from '@angular/common/http';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-links-dashboard',
  imports: [
    SlicePipe,
    DatePipe,
    FormsModule
  ],
  templateUrl: './links-dashboard.html',
  styleUrl: './links-dashboard.scss',
})
export class LinksDashboard {
  private linkShortenerService = inject(LinkShortenerService);

  page = 1;
  totalPages = 1;
  readonly pageSize = 15;

  links: GetShortLinkResponse[] = [];
  totalLinksCount = 0;
  editingId: string | null = null;
  editUrl: string = '';
  originalUrl: string = '';

  baseUrl = 'http://localhost:5100/links';

  ngOnInit() {
    this.loadLinks()
  }

  private loadLinks() {
    this.linkShortenerService.getShortLinksByPage(this.page, this.pageSize)
      .subscribe({
        next: data => {
          this.links = data.links;
          this.totalLinksCount = data.totalLinksCount;

          this.totalPages = Math.ceil(this.totalLinksCount / this.pageSize);
        },
        error: (error: HttpErrorResponse) => {
          console.error(error);
        }
      });
  }

  editLink(id: string) {
    const link = this.links.find(l => l.id === id);

    if (link) {
      this.editingId = id;
      this.editUrl = link.originalUrl;
      this.originalUrl = link.originalUrl;
    }
  }

  cancelEdit() {
    this.editingId = null;
    this.editUrl = '';
  }

  saveEdit(id: string) {
    if (!this.editUrl.trim()) {
      alert('URL не может быть пустым!');
      return;
    }

    try {
      new URL(this.editUrl);
    } catch {
      alert('Введите корректный URL (http:// или https://)');
      return;
    }

    if (this.editUrl === this.originalUrl) {
      this.cancelEdit();
      return;
    }

    const request: UpdateLongLinkRequestInterface = {
      newLongLink: this.editUrl,
    };

    this.linkShortenerService.updateLongLink(request, id)
      .subscribe({
        next: data => {
          const link = this.links.find(l => l.id === id);
          if (link) {
            link.originalUrl = data.newLongLink;
          }
          this.cancelEdit();
        },
        error: (error: HttpErrorResponse) => {
          console.error(error);
        }
      });
  }

  deleteLink(id: string) {
    if (confirm('Удалить эту ссылку?')) {
      this.linkShortenerService.deleteLink(id)
        .subscribe({
          next: () => {
            this.links = this.links.filter(link => link.id !== id);
          },
          error: (error: HttpErrorResponse) => {
            console.error(error);
          }
        });
    }
  }

  onShortLinkClick(link: GetShortLinkResponse) {
    link.clickCount++;
  }

  // ✅ Навигация по страницам
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPages && page !== this.page) {
      this.page = page;
      this.loadLinks();
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.goToPage(this.page - 1);
    }
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.goToPage(this.page + 1);
    }
  }

  // ✅ Генерация видимых номеров страниц (5 штук)
  getVisiblePages(): number[] {
    const pages: number[] = [];
    const maxVisible = 5;
    const halfVisible = Math.floor(maxVisible / 2);

    let startPage = Math.max(1, this.page - halfVisible);
    let endPage = Math.min(this.totalPages, startPage + maxVisible - 1);

    // Корректируем начало, если конец усекается
    if (endPage - startPage + 1 < maxVisible) {
      startPage = Math.max(1, endPage - maxVisible + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }
    return pages;
  }

  protected readonly Math = Math;
}
