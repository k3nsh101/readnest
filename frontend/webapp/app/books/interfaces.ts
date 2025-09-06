import { Genre } from "./new/interfaces";

export interface Book {
  bookId: string;
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
  bookId: string;
  title: string;
  genre: Genre;
  owned: boolean;
  coverUrl: string | null;
}

export interface BooksListProps {
  books: BookSummary[];
}
