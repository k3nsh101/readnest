import { BookSummary } from "../books/interfaces";
import { ReadingLog } from "./interfaces";
import NewLogForm from "./newLogForm";
import ReadingLogList from "./readingLogList";

// Disable prerendering for dynamic fetch
export const dynamic = "force-dynamic";

export default async function ReadingLogsPage() {
  const books: BookSummary[] = await fetchBooks();
  const readingLogs: ReadingLog[] = await fetchReadingLogs();
  return (
    <div className="p-6 w-full bg-white shadow-md rounded-md">
      <ReadingLogList logs={readingLogs} />
      <NewLogForm books={books} />
    </div>
  );
}

async function fetchBooks(): Promise<BookSummary[]> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/books`);
  if (!res.ok) throw new Error("Failed to fetch books");
  return res.json();
}

async function fetchReadingLogs(): Promise<ReadingLog[]> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/reading-logs`);
  if (!res.ok) throw new Error("Failed to fetch books");
  return res.json();
}
