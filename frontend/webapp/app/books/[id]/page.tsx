import Image from "next/image";
import { Book } from "../interfaces";

export default async function BookDetails({
  params,
}: {
  params: Promise<{ id: number }>;
}) {
  const bookId = (await params).id;

  const book = await fetchBookDetails(bookId);

  return (
    <div className="min-h-screen bg-gray-50j">
      <div className="flex flex-1 p-10 gap-16 justify-center">
        <div className="rounded-lg overflow-hidden">
          <Image
            src={`${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}${book.coverUrl}`}
            alt={book.name}
            width={200}
            height={400}
            className="object-cover"
          />
        </div>

        <div className="flex flex-col pt-4">
          <h1 className="text-3xl font-bold mb-2">{book.name}</h1>
          <p className="text-lg text-gray-600">{book.author}</p>
        </div>
      </div>
    </div>
  );
}

async function fetchBookDetails(id: number): Promise<Book> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/books/${id}`);
  if (!res.ok) throw new Error("Failed to fetch book details");
  return res.json();
}
