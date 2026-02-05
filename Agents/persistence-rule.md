# Persistence & Rate Limiting Rule: "One Blessing Per Day"

## Logic Overview
The app must prevent users from generating multiple verses within the same calendar day to encourage reflection. 

## Implementation Details
1. **Storage Key:** Use `localStorage` key `last_blessing_data`.
2. **Data Structure:** Store an object containing:
   - `timestamp`: The ISO string of when they clicked "Amen".
   - `verse`: The text/reference they received (so they can still see it if they return).

## Workflow (Frontend)
- **Initialization:** On `useEffect` (mount), check if `last_blessing_data` exists in `localStorage`.
- **Date Comparison:** - Compare the `timestamp` date with the current date.
    - If the date (Year-Month-Day) is the same as "Today", immediately state-shift to the **"Reflection Screen"**.
- **The Reflection Screen:** - Instead of a random verse, display: *"You have already received your Word for today. May it stay in your heart."*
    - Re-display the verse they saved in `localStorage` so they can read it again.
    - Hide the "Amen" button and show a countdown timer: *"Next blessing available at midnight."*
- **The "Amen" Trigger:** - Only save the timestamp to `localStorage` when the user clicks the "Amen" button. This ensures that if they scan but don't finish reading, they aren't locked out.

## Visual Feedback
- If a user is locked out, use a "Calm/Night" color palette (e.g., deep purples or soft greys) to distinguish the "Reflection" state from the "Active" state.