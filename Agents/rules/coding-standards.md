# Coding Standards & Best Practices

> For detailed test patterns, see `rules/unit-testing.md`.
> For documentation rules, see `rules/auto-documentation.md`.
> For branding colors, see `context/branding.md`.
> For architecture and API details, see `CLAUDE.md`.

## General Principles
- Write clean, maintainable code
- Follow language-specific conventions and best practices
- Use modern language features appropriately
- Prioritize readability over cleverness

## .NET / C# Standards

### API Design
- Use RESTful conventions
- Return appropriate HTTP status codes
- Implement proper error handling
- Enable CORS for frontend access

## React / TypeScript Standards

### Component Structure
- Keep components small and focused
- Use composition over inheritance
- Extract reusable logic into custom hooks
- Follow single responsibility principle

### Performance
- Let React Compiler handle optimizations
- Use code splitting for large features
- Optimize images and assets
- Implement proper loading states

## Git & Version Control

### Commit Messages
- Use clear, descriptive commit messages
- Follow conventional commit format when possible
- Reference issue numbers when applicable

### Branch Strategy
- Use feature branches for new work
- Keep main branch deployable
- Use pull requests for code review
