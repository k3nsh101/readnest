import { Genre } from "./new/interfaces";

export interface Book {
  bookId: number;
  title: string;
  author: string;
  genre: Genre;
  totalPages?: number;
  pagesRead?: number;
  owned: boolean;
  completed: boolean;
  rating?: number;
  remarks?: string;
  description?: string;
  coverUrl: string | null;
}

export interface BookSummary {
  bookId: number;
  title: string;
  genre: Genre;
  owned: boolean;
  coverUrl: string | null;
}
