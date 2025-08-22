import BooksList from "./bookList";
import { BookSummary } from "./interfaces";

export default async function BooksPage() {
  const books: BookSummary[] = await fetchBooks();

  return <BooksList books={books} />;
}

async function fetchBooks(): Promise<BookSummary[]> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/books`);
  if (!res.ok) throw new Error("Failed to fetch books");
  return res.json();
}
