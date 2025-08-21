"use client";
import { useState } from "react";
import { Book } from "./interfaces";

interface BooksListProps {
  books: Book[];
}

export default function BooksList({ books: initialBooks }: BooksListProps) {
  const [books, setBooks] = useState(initialBooks);

  const handleMarkComplete = async (id: number) => {
    try {
      await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books/${id}/complete`,
        {
          method: "POST",
        },
      );
      setBooks((prev) =>
        prev.map((b) => (b.bookId === id ? { ...b, completed: true } : b)),
      );
    } catch (err) {
      console.error(err);
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books/${id}`,
        {
          method: "DELETE",
        },
      );
      setBooks((prev) => prev.filter((b) => b.bookId !== id));
    } catch (err) {
      console.error(err);
    }
  };

  if (books.length === 0)
    return <p className="text-center mt-10">No books added yet.</p>;

  return (
    <div className="max-w-6xl mx-auto mt-10 grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
      {books.map((book) => (
        <div
          key={book.bookId}
          className="bg-white shadow-md rounded-lg p-4 flex flex-col justify-between"
        >
          <div>
            <h2 className="text-xl font-bold mb-1">{book.name}</h2>
            <p className="text-gray-600 mb-1">Author: {book.author}</p>
            <p className="text-gray-600 mb-1">Genre: {book.genre.name}</p>
            <p className="text-gray-600 mb-1">
              Progress: {book.pagesRead ?? 0} / {book.totalPages ?? "-"}
            </p>
            <p className="text-gray-600 mb-1">
              Owned: {book.owned ? "✅" : "❌"}
            </p>
            <p className="text-gray-600 mb-1">
              Completed: {book.completed ? "✅" : "❌"}
            </p>
            {book.rating && (
              <p className="text-gray-600 mb-1">Rating: {book.rating}/5</p>
            )}
            {book.remarks && (
              <p className="text-gray-600 mb-1">Remarks: {book.remarks}</p>
            )}
          </div>
          <div className="mt-3 flex justify-between space-x-2">
            {!book.completed && (
              <button
                onClick={() => handleMarkComplete(book.bookId)}
                className="flex-1 bg-green-500 text-white px-2 py-1 rounded hover:bg-green-600 transition"
              >
                Mark Complete
              </button>
            )}
            <button
              onClick={() => handleDelete(book.bookId)}
              className="flex-1 bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600 transition"
            >
              Delete
            </button>
          </div>
        </div>
      ))}
    </div>
  );
}
