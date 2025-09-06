"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { NewBookProps } from "./interfaces";
import { ImageUp, X } from "lucide-react";

export default function NewBookForm({ genres }: NewBookProps) {
  const router = useRouter();
  const [form, setForm] = useState({
    title: "",
    author: "",
    genreId: "",
    totalPages: "",
    borrowed: false,
  });
  const [file, setFile] = useState<File | null>();
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

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selected = e.target.files?.[0];
    if (selected) {
      setFile(selected);
    }
  };

  const handleRemoveFile = () => {
    setFile(null);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    let coverUrl = null;

    if (!form.title || !form.author || !form.genreId) {
      setError("Please fill all required fields");
      setLoading(false);
      return;
    }

    try {
      if (file) {
        const formData = new FormData();
        formData.append("coverUrl", file);

        const uploadRes = await fetch(
          `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books/cover`,
          {
            method: "POST",
            body: formData,
          },
        );

        if (!uploadRes.ok) throw new Error("Failed to add book");

        const uploadData = await uploadRes.json();
        coverUrl = uploadData.url;
      }

      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            title: form.title,
            author: form.author,
            genreId: form.genreId,
            borrowed: form.borrowed,
            totalPages: form.totalPages ? Number(form.totalPages) : null,
            coverUrl,
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
            name="borrowed"
            checked={form.borrowed}
            onChange={handleChange}
            className="h-4 w-4"
          />
          <label>Borrowed</label>
        </div>

        <div>
          <label className="block font-medium mb-1">Book Cover</label>
          <div className="relative border-2 border-gray-300 border-dashed rounded-lg p-6">
            {/* Close Button in top-right of the border box */}
            {file && (
              <button
                type="button"
                onClick={handleRemoveFile}
                className="absolute -top-3 -right-3 bg-gray-200 rounded-full p-2 hover:bg-gray-300 focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 shadow z-20"
              >
                <X className="w-5 h-5 text-gray-700" />
              </button>
            )}

            <div className="text-center relative">
              <input
                type="file"
                accept="image/*"
                onChange={handleFileChange}
                className="absolute inset-0 h-full opacity-0 z-10 cursor-pointer"
              />
              <ImageUp className="mx-auto h-12 w-12" />

              {!file ? (
                <>
                  <h3 className="mt-2 text-sm font-medium text-gray-900">
                    <label htmlFor="file-upload" className="relative">
                      <span>Drag and drop</span>
                      <span className="text-indigo-600"> or browse </span>
                      <span>to upload</span>
                      <input
                        id="file-upload"
                        name="file-upload"
                        type="file"
                        className="sr-only"
                      />
                    </label>
                  </h3>
                  <p className="mt-1 text-xs text-gray-500">
                    PNG, JPG, GIF up to 10MB
                  </p>
                </>
              ) : (
                <div className="mt-3">
                  <p className="text-sm text-gray-800 font-medium">
                    {file.name}
                  </p>
                  <p className="text-xs text-gray-500">
                    {(file.size / 1024).toFixed(1)} KB
                  </p>
                </div>
              )}
            </div>
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded cursor-pointer"
        >
          {loading ? "Saving..." : "Add Book"}
        </button>
      </form>
    </div>
  );
}
