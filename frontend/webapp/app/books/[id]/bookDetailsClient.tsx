"use client";

import Image from "next/image";
import { SquarePen } from "lucide-react";
import { Book } from "../interfaces";
import { useState } from "react";
import { Genre } from "../new/interfaces";

enum ReadingStatus {
  NotStarted = 0,
  Reading = 1,
  Completed = 2,
}

const ReadingStatusLabels: Record<ReadingStatus, string> = {
  [ReadingStatus.NotStarted]: "Not Started",
  [ReadingStatus.Reading]: "Reading",
  [ReadingStatus.Completed]: "Completed",
};

export default function BookDetailsClient({ book }: { book: Book }) {
  const [isEditing, setIsEditing] = useState(false);
  const [form, setForm] = useState({
    title: book.title,
    author: book.author,
    genre: book.genre,
    pageCount: book.totalPages,
    readCount: book.pagesRead,
    readStatus: book.status,
    rating: book.rating,
    remarks: book.remarks || "",
  });
  const [genres, setGenres] = useState<Genre[]>([]);
  const readStatusLabel = ReadingStatusLabels[form.readStatus as ReadingStatus];

  const handleEditFields = async () => {
    async function getGenres(): Promise<Genre[]> {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/genres`,
      );
      if (!res.ok) throw new Error("Failed to fetch genres");
      return res.json();
    }

    setIsEditing(true);
    const genres = await getGenres();
    setGenres(genres);
  };

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >,
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSave = async () => {
    const updatedData = {
      title: form.title,
      author: form.author,
      totalPages: form.pageCount,
      pagesRead: form.readCount,
      status: Number(form.readStatus),
      rating: form.rating,
      remarks: form.remarks,
      genreId: form.genre.genreId,
    };

    if (
      updatedData.status == 2 &&
      updatedData.pagesRead != updatedData.totalPages
    ) {
      alert("Incompatible status and read pages");
      return;
    }

    await fetch(
      `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/books/${book.bookId}`,
      {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedData),
      },
    );
    setIsEditing(false);
  };

  const handleCancel = () => {
    setForm({
      title: book.title,
      author: book.author,
      genre: book.genre,
      pageCount: book.totalPages,
      readCount: book.pagesRead,
      readStatus: book.status,
      rating: book.rating,
      remarks: book.remarks || "",
    });
    setIsEditing(false);
  };

  return (
    <div>
      <div className="flex flex-1 p-10 gap-16 justify-center">
        <div className="rounded-lg min-w-[250px]">
          <Image
            src={`${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}${book.coverUrl}`}
            alt={book.title}
            width={250}
            height={400}
            className="object-cover"
          />
        </div>

        <div className="relative flex flex-col gap-2 pt-3">
          <SquarePen
            className={`absolute top-0 -right-10 ${!isEditing && "cursor-pointer hover:bg-gray-300"}`}
            onClick={handleEditFields}
          />

          {isEditing ? (
            <textarea
              name="title"
              value={form.title}
              onChange={handleChange}
              className="w-full text-3xl font-bold mb-2 max-w-[350px] p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
            />
          ) : (
            <h1 className="text-3xl font-bold mb-2 max-w-[350px]">
              {form.title}
            </h1>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Author</label>
              <input
                type="text"
                name="author"
                value={form.author}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
              />
            </div>
          ) : (
            <p className="text-lg text-gray-600">{form.author}</p>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Genre</label>
              <select
                name="genre"
                value={form.genre.genreId}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
                required
              >
                {genres?.map((genre) => (
                  <option key={genre.genreId} value={genre.genreId}>
                    {genre.name}
                  </option>
                ))}
              </select>
            </div>
          ) : (
            <p className="text-lg text-gray-600">{form.genre.name}</p>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Total Pages</label>
              <input
                type="number"
                name="pageCount"
                value={form.pageCount}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
              />
            </div>
          ) : (
            <p className="text-lg text-gray-600">{form.pageCount}</p>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Pages Read</label>
              <input
                type="number"
                name="readCount"
                value={form.readCount}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
              />
            </div>
          ) : (
            <p className="text-lg text-gray-600">{form.readCount}</p>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Read Status</label>
              <select
                name="readStatus"
                value={form.readStatus}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
                required
              >
                {Object.entries(ReadingStatusLabels)?.map(([value, label]) => (
                  <option key={value} value={value}>
                    {label}
                  </option>
                ))}
              </select>
            </div>
          ) : (
            <p className="text-lg text-gray-600">{readStatusLabel}</p>
          )}

          {isEditing ? (
            <div>
              <label className="block font-medium mb-1">Rating</label>
              <input
                type="text"
                name="author"
                value={form.rating}
                onChange={handleChange}
                className="w-full text-lg text-gray-600 block p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
              />
            </div>
          ) : (
            <p className="text-lg text-gray-600">{form.rating}</p>
          )}
        </div>
      </div>

      <div className="mt-8">
        <h1 className="text-3xl font-bold mb-2">Description</h1>
        {isEditing ? (
          <textarea
            name="remarks"
            value={form.remarks}
            onChange={handleChange}
            className="text-lg text-gray-600 block w-full p-2 border-2 border-dotted border-emerald-300 rounded-lg focus:border-dotted focus:border-emerald-300 focus:outline-none transition-shadow duration-200 focus:shadow-md"
            rows={5}
          />
        ) : (
          <p className="text-lg text-gray-600">{form.remarks}</p>
        )}
      </div>
      {isEditing && (
        <div className="mt-4 space-x-2">
          <button
            onClick={handleSave}
            className="px-4 py-2 bg-blue-500 text-white rounded"
          >
            Save
          </button>
          <button
            onClick={handleCancel}
            className="px-4 py-2 bg-gray-300 rounded"
          >
            Cancel
          </button>
        </div>
      )}
    </div>
  );
}
