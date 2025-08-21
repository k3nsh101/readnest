export interface Genre {
  genreId: number;
  name: string;
}

export interface NewBookProps {
  genres: Genre[];
}
