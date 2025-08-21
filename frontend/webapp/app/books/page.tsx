import BooksList from "./bookList";
import { Book } from "./interfaces";
import { fetchBooks } from "./page.server";

export default async function BooksPage() {
  const books: Book[] = await fetchBooks();

  return <BooksList books={books} />;
}
