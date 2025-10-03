import { Genre } from "./interfaces";
import NewBookForm from "./newBookForm";
import { getGenres } from "./page.server";

// Disable prerendering for dynamic fetch
export const dynamic = "force-dynamic";

export default async function NewBook() {
  const genres: Genre[] = await getGenres();

  return <NewBookForm genres={genres} />;
}
