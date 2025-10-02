"use client";

import Link from "next/link";
import { ReadingLogs } from "./interfaces";
import { Trash2 } from "lucide-react";
import { useState } from "react";
import { useRouter } from "next/navigation";

export default function ReadingListPage({ logs }: ReadingLogs) {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  const handleDelete = async (bookId: string, date: string) => {
    setLoading(true);
    setError(null);

    if (!bookId || !date) {
      setError("Please fill all required fields");
      setLoading(false);
      return;
    }

    try {
      const queryParams = new URLSearchParams({
        bookId,
        date,
      });

      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACKEND_API_BASE_URL}/reading-logs?${queryParams.toString()}`,
        {
          method: "DELETE",
        },
      );

      if (!res.ok) throw new Error("Failed to add delete reading log");
      router.refresh();
    } catch (err: any) {
      setError(err.message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-6">
      {error && <p className="text-red-500 mb-4">{error}</p>}

      <div className="max-w-4xl">
        <header className="mb-6">
          <h1 className="text-2xl font-semibold text-gray-900">Reading List</h1>
          <p className="text-sm text-gray-600">
            Click a title to open its page. Use delete to remove entries.
          </p>
        </header>

        <div className="overflow-x-auto bg-white shadow-sm rounded-lg">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-100">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Title
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Date
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Pages Read
                </th>
                <th className="px-6 py-3 text-center text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Actions
                </th>
              </tr>
            </thead>

            <tbody className="divide-y divide-gray-100">
              {logs.length === 0 && (
                <tr>
                  <td
                    colSpan={4}
                    className="px-6 py-8 text-center text-sm text-gray-500"
                  >
                    No books in the list.
                  </td>
                </tr>
              )}

              {logs.map((log) => (
                <tr
                  key={`${log.bookId}-${log.date}`}
                  className="hover:bg-gray-50"
                >
                  <td className="px-6 py-4 whitespace-nowrap">
                    <Link
                      href={`/books/${log.bookId}`}
                      className="text-blue-600 hover:underline font-medium"
                    >
                      {log.title}
                    </Link>
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-600">
                    {log.date}
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                    {log.pagesRead.toLocaleString()}
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-center">
                    <button
                      onClick={() => handleDelete(log.bookId, log.date)}
                      aria-label={`Delete ${log.title}`}
                      className="inline-flex items-center gap-2 px-3 py-1.5 text-sm font-medium rounded-md border border-gray-200 hover:bg-red-50 focus:outline-none focus:ring-2 focus:ring-red-300 cursor-pointer"
                      disabled={loading}
                    >
                      <Trash2 />
                      <span className="sr-only">Delete</span>
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
