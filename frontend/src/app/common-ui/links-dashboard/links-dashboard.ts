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

  private page = 1;
  private readonly pageSize = 15;

  links: GetShortLinkResponse[] = [];
  editingId: string | null = null;
  editUrl: string = '';
  originalUrl: string = '';

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
}
