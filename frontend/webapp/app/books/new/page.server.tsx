import { Genre } from "./interfaces";

async function getGenres(): Promise<Genre[]> {
  const res = await fetch(`${process.env.BACKEND_API_BASE_URL}/genres`);
  if (!res.ok) throw new Error("Failed to fetch genres");
  return res.json();
}

export { getGenres };
