# Project Vibe: "Amen" QR Blessing App

## Tech Stack
- **Backend:** .NET 10 Web API (Minimal APIs), C# 14.
- **Frontend:** React 19, TypeScript, Vite, Tailwind CSS, Lucide React (icons).
- **Communication:** Axios or Fetch API.

## Core Features
1. **Verse API:** A backend endpoint `/api/verse/random` that returns a JSON object with a verse text and reference.
2. **Mobile-First UI:** A clean, spiritual, and high-end mobile interface.
3. **The Interaction:** - Load verse on mount.
   - Large "Amen" button.
   - On click: Trigger a confetti effect and show a "Next QR" or "Blessing Accepted" screen.
4. **QR Generation:** The app should include a "Church Admin" route (`/admin`) that displays a permanent QR code pointing to the home page.

## Vibe Guidelines
- Use **soft gradients** (light blues/creams).
- Animations: Use **Framer Motion** for smooth verse transitions.
- The tone is encouraging, grounded, and modern.