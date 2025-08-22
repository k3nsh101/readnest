import { Book } from "./interfaces";

export async function fetchBooks(): Promise<Book[]> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/books`);
  if (!res.ok) throw new Error("Failed to fetch books");
  return res.json();
}
