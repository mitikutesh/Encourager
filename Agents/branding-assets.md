# ECCFIN Branding & Identity Instructions

## Official Palette
- **Primary (Deep Navy):** `#1a374f` - Use for main headings, the 'Amen' button background, and logo fallback text.
- **Secondary (Earthy Green):** `#6f9078` - Use for borders or secondary icons.
- **Accent (Warm Terracotta):** `#d06450` - Use for high-emphasis highlights or the "Amen" click animation.
- **Background (Cream):** `#fdfbca` - Use as the main page background for a warm, paper-like feel.
- **Muted Elements (Slate/Grey):** `#424555` or `#cad4d7` - Use for the "Media Team" footer and verse reference text.
- **Action Highlight (Light Blue):** `#77c4f0` - Use for hover states or link text.

## UI Implementation Details
- **Logo:** Center `/public/logo.png` at the top. Max height 80px.
- **Card Style:** - Background: White or a very subtle tint of `#cad4d7`.
  - Border: 1px solid `#6f9078` (low opacity).
  - Corner Radius: 28px (Extra rounded for a friendly feel).
- **Typography:**
  - Verse Text: Use the Primary color (`#1a374f`) with a Serif font.
  - Footer: "Designed with ❤️ by ECCFIN Media Team" in `#424555`.

## Animation Vibe
- Use **Framer Motion** to animate the verse card color from a soft `#fdfbca` glow to a solid state on load.