import { Book } from "../interfaces";
import BookDetailsClient from "./bookDetailsClient";

export default async function BookDetails({
  params,
}: {
  params: Promise<{ id: number }>;
}) {
  const bookId = (await params).id;

  const book = await fetchBookDetails(bookId);

  return <BookDetailsClient book={book} />;
}

async function fetchBookDetails(id: number): Promise<Book> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/books/${id}`);
  if (!res.ok) throw new Error("Failed to fetch book details");
  return res.json();
}
