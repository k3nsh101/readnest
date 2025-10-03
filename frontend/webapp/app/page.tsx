import BooksPage from "./books/page";

// Disable prerendering for dynamic fetch
export const dynamic = "force-dynamic";

export default function Home() {
  return <BooksPage />;
}
