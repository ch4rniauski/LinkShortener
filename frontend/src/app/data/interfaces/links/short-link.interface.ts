interface ShortLink {
  id: number;
  originalUrl: string;
  shortToken: string;
  createdAt: Date;
  clicks: number;
}
