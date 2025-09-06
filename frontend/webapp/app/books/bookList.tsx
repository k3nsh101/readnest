import Link from "next/link";
import Image from "next/image";
import { BooksListProps } from "./interfaces";

export default function BooksList({ books }: BooksListProps) {
  if (books.length === 0)
    return <p className="text-center mt-10">No books added yet.</p>;

  return (
    <div className="flex gap-10 flex-wrap">
      {books.map((book) => (
        <div key={book.bookId} className="bg-transparent flex flex-col gap-1">
          <Link href={`books/${book.bookId}`} className="h-full">
            {book.coverUrl && (
              <Image
                src={`${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}${book.coverUrl}`}
                alt={book.title}
                width={170}
                height={300}
                className="rounded mb-2 h-full object-fill"
              />
            )}
            <p className="font-bold">{book.title}</p>
          </Link>
        </div>
      ))}
    </div>
  );
}
