import { Genre } from "./interfaces";
import NewBookForm from "./newBookForm";
import { getGenres } from "./page.server";

export default async function NewBook() {
  const genres: Genre[] = await getGenres();

  return <NewBookForm genres={genres} />;
}
