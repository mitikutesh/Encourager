# Getting Started

## Prerequisites

Before you begin, ensure you have the following tools installed:

| Tool | Version | Installation |
|------|---------|--------------|
| .NET SDK | 10.0+ | `brew install dotnet` (macOS) or [download](https://dotnet.microsoft.com/download) |
| Node.js | 22+ | `brew install node` (macOS) or [download](https://nodejs.org/) |
| AWS CLI | 2.x | `brew install awscli` (macOS) or [download](https://aws.amazon.com/cli/) |
| AWS SAM CLI | 1.x | `brew install aws-sam-cli` (macOS) or [download](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/install-sam-cli.html) |
| Docker Desktop | latest | [download](https://docker.com/products/docker-desktop) |

## Project Structure

```
Encourager/
├── backend/              # .NET 10 Minimal API
│   ├── Program.cs        # Kestrel entry point (local dev)
│   ├── LambdaEntryPoint.cs  # Lambda entry point (AWS)
│   ├── AppConfiguration.cs  # Shared DI & endpoints
│   └── Services/        # Business logic
├── frontend/             # React 19 + TypeScript
│   ├── src/
│   │   ├── pages/       # Route components
│   │   ├── components/  # Reusable components
│   │   ├── contexts/    # React contexts
│   │   └── i18n/        # Translations
│   └── vite.config.ts   # Vite configuration
├── infrastructure/       # AWS SAM template
│   └── template.yaml    # CloudFormation template
├── scripts/              # Deployment scripts
├── tests/               # Backend unit tests
├── e2e/                 # Playwright E2E tests
└── docs/                # Documentation
```

## Local Development Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd Encourager
```

### 2. Backend Setup

```bash
cd backend
dotnet restore
dotnet run
```

The backend will start on `http://localhost:5226`

**Key Files:**
- `Program.cs`: Kestrel web server for local development
- `AppConfiguration.cs`: Shared service registration and endpoint mapping
- `Services/VerseService.cs`: Verse data service

### 3. Frontend Setup

```bash
cd frontend
npm install
npm run dev
```

The frontend will start on `http://localhost:5173` and proxy `/api/*` requests to the backend.

**Key Files:**
- `src/main.tsx`: Application entry point
- `src/pages/Home.tsx`: Main verse display page
- `vite.config.ts`: Vite configuration with PWA plugin

### 4. Full Stack with Docker Compose

```bash
docker compose up
```

This starts:
- Backend on `:8080` (internal)
- Frontend on `:4000` (accessible)

**Note**: E2E tests expect the app running at `http://localhost:4000`

## Development Workflow

### Running Tests

**Backend Unit Tests:**
```bash
dotnet test                                    # Run all tests
dotnet test tests/Encourager.Api.Tests/       # Run specific project
dotnet test --filter "FullyQualifiedName~GetRandomVerse"  # Run single test
dotnet test --verbosity normal                 # Verbose output
```

**Frontend E2E Tests:**
```bash
cd e2e
npx playwright install --with-deps chromium   # First time setup
npx playwright test                           # Headless
npm run test:headed                           # With browser UI
```

**Linting:**
```bash
cd frontend
npm run lint
```

### Building for Production

**Backend:**
```bash
cd backend
dotnet build --configuration Release
```

**Frontend:**
```bash
cd frontend
npm run build  # Runs: tsc -b && vite build
```

Output: `frontend/dist/` directory with production build

## Environment Variables

### Backend

| Variable | Default | Description |
|----------|---------|-------------|
| `ALLOWED_ORIGIN` | `*` | CORS allowed origin (restrict in production) |
| `ASPNETCORE_ENVIRONMENT` | `Development` | Environment name |

**Local Development:**
- Defaults allow all origins (`*`)
- No additional configuration needed

**Production:**
- Set `ALLOWED_ORIGIN` to your CloudFront URL
- Set `ASPNETCORE_ENVIRONMENT` to `Production`

### Frontend

No environment variables required. API proxy configured in `vite.config.ts`:

```typescript
proxy: {
  '/api': 'http://localhost:5226'
}
```

## Common Development Tasks

### Adding a New Verse

1. Open `backend/Services/Data/EnglishVerses.cs` (or AmharicVerses/FinnishVerses)
2. Add new verse to the array:
```csharp
new Verse { Text = "Your verse text", Reference = "Book 1:1", Index = 50 }
```
3. Update index numbers if needed
4. Test with: `GET /api/verse/random?lang=en&index=50`

### Adding a New Language

1. Create new verse data class: `backend/Services/Data/NewLanguageVerses.cs`
2. Add language to `VerseService.cs`:
```csharp
case "newlang":
    return NewLanguageVerses.Verses;
```
3. Add translations: `frontend/src/i18n/translations.ts`
4. Update language selector in UI

### Modifying API Endpoints

1. Edit `backend/AppConfiguration.cs`
2. Add endpoint in `ConfigureEndpoints` method
3. Test locally: `dotnet run`
4. Update API documentation

### Changing UI Components

1. Edit components in `frontend/src/components/`
2. Hot reload available in dev mode
3. Check browser console for errors
4. Test on mobile viewport

## IDE Setup

### VS Code

**Recommended Extensions:**
- C# Dev Kit (for .NET)
- ESLint (for TypeScript/React)
- Tailwind CSS IntelliSense
- Prettier

**Settings:**
- Format on save enabled
- TypeScript strict mode
- ESLint auto-fix on save

### Visual Studio / Rider

- Full .NET support
- React/TypeScript support
- Integrated debugging

## Debugging

### Backend Debugging

**VS Code:**
1. Set breakpoints in C# files
2. Press F5 to start debugging
3. Use Debug Console for inspection

**Command Line:**
```bash
dotnet run --verbosity detailed
```

### Frontend Debugging

**Browser DevTools:**
1. Open Chrome DevTools (F12)
2. Set breakpoints in TypeScript files
3. Use React DevTools extension
4. Check Network tab for API calls

**Console Logging:**
```typescript
console.log('Debug info', data);
```

## Troubleshooting Development Issues

### Backend Won't Start

- Check .NET SDK version: `dotnet --version`
- Restore packages: `dotnet restore`
- Check port 5226 is available
- Review `Program.cs` for errors

### Frontend Won't Start

- Check Node.js version: `node --version`
- Clear node_modules: `rm -rf node_modules && npm install`
- Check port 5173 is available
- Review `vite.config.ts` for errors

### API Proxy Not Working

- Verify backend is running on :5226
- Check `vite.config.ts` proxy configuration
- Review browser Network tab for failed requests

### Tests Failing

- Run tests individually to isolate issues
- Check test data builders are correct
- Verify mocks are set up properly
- Review test output for specific errors

## Next Steps

- Read [API Reference](./api-reference.md) for endpoint details
- Review [Development Workflows](./development-workflows.md) for best practices
- Check [Testing Guidelines](./testing-guidelines.md) for test patterns
- See [Deployment](./deployment.md) for production setup
