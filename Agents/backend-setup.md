# Step 1: .NET 10 Backend (Server)

- Create a `Verse` model: `string Text`, `string Reference`.
- Create a `VerseService` containing a hardcoded list of 50 encouraging verses (to avoid needing a DB for now).
- Implement a Minimal API endpoint:
  - `GET /api/verse/random` -> Returns a random verse.
- Enable **CORS** to allow the React frontend (running on a different port) to access the API.