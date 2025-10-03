import DeleteGenre from "./deleteGenre";
import NewGenreForm from "./newGenreForm";
import { getGenres } from "./page.server";

// Disable prerendering for dynamic fetch
export const dynamic = "force-dynamic";

export default async function GenrePage() {
  const genres = await getGenres();

  return (
    <>
      <NewGenreForm />
      <DeleteGenre genres={genres} />
    </>
  );
}
