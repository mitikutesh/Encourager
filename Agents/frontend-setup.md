# Step 2: React 19 Frontend (Client)

- Use **Vite** to scaffold a React TS project.
- **Components:**
  - `VerseCard.tsx`: Displays the text and reference with a fade-in animation.
  - `AmenButton.tsx`: A stylized button that triggers the `onAccept` state.
  - `SuccessView.tsx`: Shows a "God Bless" message and a "New Blessing" button.
- **State Management:** Simple `useState` to track the current verse and whether the user has "accepted" it.
- **Styling:** Use Tailwind CSS for a card-based layout centered on the mobile screen.

# Instructions: React 19 Mobile UI
- Scaffold a Vite project with React and TypeScript.
- Use **Tailwind CSS** for styling.
- **Components to build:**
    - `VerseDisplay`: A card that uses Framer Motion to "fade and float" in when the verse loads.
    - `AmenButton`: A high-end button with a subtle "glow" effect. 
    - `Celebration`: A component using `canvas-confetti` that triggers when "Amen" is clicked.
- **State Logic:**
    - Fetch the random verse from the .NET 10 API on mount.
    - Track "accepted" status to toggle between the Verse card and the Success screen.
- **Visual Vibe:** Modern, clean, spiritual. Use a soft background gradient (e.g., `bg-slate-50` to `bg-blue-50`).