"use client";

import { useState } from "react";
import { BooksListProps } from "../books/interfaces";

export default function NewLogForm({ books }: BooksListProps) {
  const [form, setForm] = useState({
    bookId: "",
    pagesRead: "",
    date: "",
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>,
  ) => {
    const { name, value } = e.target;

    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    if (!form.bookId || !form.pagesRead || !form.date) {
      setError("Please fill all required fields");
      setLoading(false);
      return;
    }

    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/reading-logs`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            bookId: form.bookId,
            pagesRead: form.pagesRead,
            date: form.date,
          }),
        },
      );

      if (!res.ok) throw new Error("Failed to save log");
      setForm({
        bookId: "",
        pagesRead: "",
        date: "",
      });
    } catch (err: any) {
      setError(err.message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-6 max-w-150">
      <h1 className="text-2xl font-bold mb-6">Add Log</h1>

      {error && <p className="text-red-500 mb-4">{error}</p>}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="flex items-center">
          <label className="block font-medium min-w-30">Book</label>
          <select
            name="bookId"
            value={form.bookId}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          >
            <option value="">Select Book</option>
            {books?.map((book) => (
              <option key={book.bookId} value={book.bookId}>
                {book.title}
              </option>
            ))}
          </select>
        </div>

        <div className="flex items-center">
          <label className="block font-medium min-w-30">Date</label>
          <input
            type="date"
            name="date"
            value={form.date}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="flex items-center">
          <label className="block font-medium min-w-30">Pages Read</label>
          <input
            type="number"
            name="pagesRead"
            value={form.pagesRead}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded cursor-pointer"
        >
          {loading ? "Saving..." : "Save Log"}
        </button>
      </form>
    </div>
  );
}
