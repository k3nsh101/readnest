export interface ReadingLog {
  bookId: string;
  title: string;
  date: string;
  pagesRead: number;
}

export interface ReadingLogs {
  logs: ReadingLog[];
}
