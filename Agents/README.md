# Agent Instructions & Documentation

This directory contains organized instructions, rules, and guides for AI agents (Cursor, Claude, etc.) working on the Encourager project.

## Structure

```
Agents/
├── README.md              # This file - main index
├── rules/                 # Coding standards and rules
│   ├── auto-documentation.md  ⭐ MANDATORY
│   ├── coding-standards.md
│   └── unit-testing.md
├── guides/                # Implementation guides
│   ├── admin-qr-generator.md
│   ├── backend-setup.md
│   ├── daily-blessing-rule.md
│   ├── frontend-setup.md
│   └── pwa-implementation.md
└── context/               # Project context and domain knowledge
    └── branding.md
```

## Quick Reference

### For Coding Standards
- **[Coding Standards](rules/coding-standards.md)** - General coding practices for .NET and React
- **[Unit Testing Rules](rules/unit-testing.md)** - Test structure, naming, and patterns
- **[Auto Documentation](rules/auto-documentation.md)** ⭐ **MANDATORY** - Documentation standards and automatic updates with code changes

### For Implementation Guides
- **[Backend Setup](guides/backend-setup.md)** - .NET 10 API setup and structure
- **[Frontend Setup](guides/frontend-setup.md)** - React 19 setup and component structure
- **[PWA Implementation](guides/pwa-implementation.md)** - Progressive Web App setup
- **[Daily Blessing Rule](guides/daily-blessing-rule.md)** - One blessing per day logic
- **[Admin QR Generator](guides/admin-qr-generator.md)** - QR code generation for admin

### For Project Context
- **[Branding](context/branding.md)** - ECCFIN branding guidelines and color palette

## Usage

### For Cursor
Cursor will automatically read the `.cursorrules` file in the project root, which references this directory. All files in this directory are available for context.

### For Claude
Claude can read these markdown files directly. Reference specific files when needed:
- Use `context/` files for project understanding
- Use `rules/` files for coding standards
- Use `guides/` files for implementation steps

## File Categories

### Rules (`rules/`)
Standards and conventions that should be followed:
- Coding standards and best practices
- Testing requirements
- Documentation requirements

### Guides (`guides/`)
Step-by-step implementation instructions:
- Setup procedures
- Feature implementation
- Configuration details

### Context (`context/`)
Domain knowledge and project information:
- Branding guidelines
- Business rules

## Contributing

When adding new instructions:
1. **Rules** -> Add to `rules/` if it's a standard or convention
2. **Guides** -> Add to `guides/` if it's an implementation guide
3. **Context** -> Add to `context/` if it's domain knowledge or project info
4. Update this README with links to new files
