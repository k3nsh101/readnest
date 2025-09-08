import Link from "next/link";
import Image from "next/image";
import { BooksListProps } from "./interfaces";

export default function BooksList({ books }: BooksListProps) {
  if (books.length === 0)
    return <p className="text-center mt-10">No books added yet.</p>;

  return (
    <div className="grid gap-6 grid-cols-[repeat(auto-fit,minmax(180px,1fr))]">
      {books.map((book) => (
        <div
          key={book.bookId}
          className="flex flex-col items-center max-w-[220px] mx-auto"
        >
          <Link href={`books/${book.bookId}`} className="w-full">
            {book.coverUrl && (
              <div className="w-full h-[300px] flex items-center justify-center bg-gray-100 rounded mb-2">
                <Image
                  src={`${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}${book.coverUrl}`}
                  alt={book.title}
                  width={200}
                  height={300}
                  className="max-h-full object-contain"
                />
              </div>
            )}
            <p className="font-bold text-center break-words line-clamp-2">
              {book.title}
            </p>
          </Link>
        </div>
      ))}
    </div>
  );
}
