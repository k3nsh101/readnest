"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { NewBookProps } from "./interfaces";

export default function NewBookForm({ genres }: NewBookProps) {
  const router = useRouter();
  const [form, setForm] = useState({
    title: "",
    author: "",
    genreId: "",
    totalPages: "",
    burrowed: false,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>,
  ) => {
    const { name, type } = e.target;

    let value: string | boolean;

    if (type === "checkbox") {
      value = (e.target as HTMLInputElement).checked;
    } else {
      value = e.target.value;
    }

    setForm((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    if (!form.title || !form.author || !form.genreId) {
      setError("Please fill all required fields");
      setLoading(false);
      return;
    }

    try {
      console.log(process.env);
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            name: form.title,
            author: form.author,
            genreId: form.genreId,
            burrowed: form.burrowed,
            totalPages: form.totalPages ? Number(form.totalPages) : null,
          }),
        },
      );

      if (!res.ok) throw new Error("Failed to add book");

      router.push("/books");
    } catch (err: any) {
      setError(err.message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-xl mx-auto mt-10 p-6 bg-white shadow-md rounded-md">
      <h1 className="text-2xl font-bold mb-6">Add a New Book</h1>

      {error && <p className="text-red-500 mb-4">{error}</p>}

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block font-medium mb-1">Title</label>
          <input
            type="text"
            name="title"
            value={form.title}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <div>
          <label className="block font-medium mb-1">Author</label>
          <input
            type="text"
            name="author"
            value={form.author}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <div>
          <label className="block font-medium mb-1">Genre</label>
          <select
            name="genreId"
            value={form.genreId}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          >
            <option value="">Select a genre</option>
            {genres?.map((genre) => (
              <option key={genre.genreId} value={genre.genreId}>
                {genre.name}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block font-medium mb-1">Total Pages</label>
          <input
            type="number"
            name="totalPages"
            value={form.totalPages}
            onChange={handleChange}
            className="w-full border rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            min={1}
          />
        </div>

        <div className="flex items-center space-x-2">
          <input
            type="checkbox"
            name="burrowed"
            checked={form.burrowed}
            onChange={handleChange}
            className="h-4 w-4"
          />
          <label>Burrowed</label>
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded"
        >
          {loading ? "Saving..." : "Add Book"}
        </button>
      </form>
    </div>
  );
}
