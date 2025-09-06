import DeleteGenre from "./deleteGenre";
import NewGenreForm from "./newGenreForm";
import { getGenres } from "./page.server";

export default async function GenrePage() {
  const genres = await getGenres();

  return (
    <>
      <NewGenreForm />
      <DeleteGenre genres={genres} />
    </>
  );
}
