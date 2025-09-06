export interface Genre {
  genreId: string;
  name: string;
}

export interface NewBookProps {
  genres: Genre[];
}
