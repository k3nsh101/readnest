import { Genre } from "./new/interfaces";

export interface Book {
  bookId: number;
  name: string;
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
  name: string;
  genre: Genre;
  owned: boolean;
  coverUrl: string | null;
}
