"use client";
import { Book } from "./interfaces";
import Image from "next/image";

interface BooksListProps {
  books: Book[];
}

export default function BooksList({ books }: BooksListProps) {
  if (books.length === 0)
    return <p className="text-center mt-10">No books added yet.</p>;

  return (
    <div className="flex gap-10">
      {books.map((book) => (
        <div
          key={book.bookId}
          className="bg-transparent flex flex-col gap-1 w-50"
        >
          {book.coverUrl && (
            <Image
              src={`${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}${book.coverUrl}`}
              alt={book.name}
              width={160}
              height={300}
              className="rounded mb-2 w-full h-full object-cover"
            />
          )}
          <p className="font-bold">{book.name}</p>
        </div>
      ))}
    </div>
  );
}
