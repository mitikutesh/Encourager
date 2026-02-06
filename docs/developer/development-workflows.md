# Development Workflows

## Git Workflow

### Branch Strategy

- **main**: Production-ready code
- **develop**: Integration branch for features
- **feature/***: Feature branches
- **bugfix/***: Bug fix branches
- **hotfix/***: Critical production fixes

### Commit Messages

Follow conventional commits:

```
feat: add new language support
fix: correct daily blessing date comparison
docs: update API documentation
test: add unit tests for VerseService
refactor: simplify verse fetching logic
```

### Pull Request Process

1. **Create Feature Branch**
   ```bash
   git checkout -b feature/new-feature
   ```

2. **Make Changes**
   - Write code
   - Add tests
   - Update documentation

3. **Commit Changes**
   ```bash
   git add .
   git commit -m "feat: add new feature"
   ```

4. **Push and Create PR**
   ```bash
   git push origin feature/new-feature
   ```
   - Create PR on GitHub
   - Add description and link related issues
   - Request review

5. **Address Review Feedback**
   - Make requested changes
   - Push updates to PR branch

6. **Merge**
   - Squash and merge (preferred)
   - Delete branch after merge

## Code Review Checklist

### Backend (.NET)

- [ ] Code follows C# 14 conventions
- [ ] Nullable reference types handled correctly
- [ ] Unit tests added/updated
- [ ] Tests follow naming convention: `[Method]_Should[Behavior]_When[Condition]`
- [ ] No `any` types or unsafe code
- [ ] Error handling implemented
- [ ] Logging added where appropriate

### Frontend (React/TypeScript)

- [ ] TypeScript strict mode compliance
- [ ] No `any` types
- [ ] React 19 features used appropriately
- [ ] Components are functional with hooks
- [ ] No manual memoization (let React Compiler handle)
- [ ] Accessibility considerations
- [ ] Mobile-first responsive design
- [ ] E2E tests updated if UI changed

### Documentation

- [ ] Code comments for complex logic
- [ ] API documentation updated
- [ ] Architecture diagrams updated if needed
- [ ] User documentation updated for new features

## Testing Workflow

### Before Committing

1. **Run Unit Tests**
   ```bash
   dotnet test
   ```

2. **Run Linter**
   ```bash
   cd frontend && npm run lint
   ```

3. **Check TypeScript**
   ```bash
   cd frontend && npm run build
   ```

### Before Pushing

1. **Run All Tests**
   ```bash
   # Backend
   dotnet test
   
   # Frontend E2E (if app running)
   cd e2e && npx playwright test
   ```

2. **Manual Testing**
   - Test all three languages
   - Test daily blessing rule
   - Test offline functionality
   - Test on mobile viewport

### CI/CD Pipeline

GitHub Actions automatically runs:

1. **Build**
   - Backend: `dotnet build`
   - Frontend: `npm run build`

2. **Test**
   - Backend: `dotnet test`
   - Frontend: TypeScript check

3. **Lint**
   - Frontend: `npm run lint`

## Feature Development Workflow

### 1. Planning

- Create GitHub issue
- Define requirements
- Design API changes (if needed)
- Plan UI changes

### 2. Implementation

**Backend:**
1. Create feature branch
2. Add/update data models
3. Implement service logic
4. Add API endpoints
5. Write unit tests
6. Test locally

**Frontend:**
1. Update types/interfaces
2. Create/update components
3. Add translations
4. Update routing (if needed)
5. Test locally
6. Test all languages

### 3. Integration

1. Test full stack locally
2. Run E2E tests
3. Test offline functionality
4. Test on mobile devices

### 4. Documentation

1. Update API docs (if API changed)
2. Update user docs (if feature visible)
3. Update architecture docs (if structure changed)
4. Add code comments

### 5. Review & Merge

1. Create PR
2. Address review feedback
3. Merge to main
4. Deploy (if auto-deploy enabled)

## Bug Fix Workflow

### 1. Identify Bug

- Reproduce issue
- Document steps to reproduce
- Identify root cause

### 2. Fix

1. Create bugfix branch
2. Write failing test (if applicable)
3. Fix the bug
4. Verify test passes
5. Test manually

### 3. Verify

- Test fix in isolation
- Test related functionality
- Test all languages (if UI bug)
- Test edge cases

### 4. Document

- Update changelog
- Update docs if needed
- Close related issues

## Refactoring Workflow

### 1. Identify Need

- Code smells
- Performance issues
- Maintainability concerns

### 2. Plan Refactor

- Identify scope
- Ensure test coverage
- Plan incremental changes

### 3. Execute

1. Create refactor branch
2. Run existing tests (baseline)
3. Make incremental changes
4. Run tests after each change
5. Update documentation

### 4. Verify

- All tests pass
- No functionality changed
- Performance improved (if applicable)
- Code quality improved

## Deployment Workflow

### Development Environment

1. **Local Testing**
   ```bash
   docker compose up
   ```

2. **Manual Deployment** (if needed)
   ```bash
   ./scripts/deploy-backend.sh
   ./scripts/deploy-frontend.sh
   ```

### Staging Environment

1. **Merge to develop**
2. **CI/CD triggers**
3. **Auto-deploy to staging**
4. **Manual verification**

### Production Environment

1. **Merge to main**
2. **CI/CD triggers**
3. **Auto-deploy to production**
4. **Monitor health endpoint**
5. **Verify CloudFront distribution**

## Code Quality Standards

### Backend

- **Naming**: PascalCase for classes, camelCase for methods
- **Async/Await**: Use async/await, not `.Result` or `.Wait()`
- **Null Safety**: Use nullable reference types correctly
- **Dependency Injection**: Use constructor injection
- **Minimal APIs**: Keep endpoints simple and focused

### Frontend

- **Naming**: PascalCase for components, camelCase for functions
- **Hooks**: Use hooks, not class components
- **TypeScript**: Strict mode, no `any` types
- **Styling**: Tailwind utility classes, mobile-first
- **State**: Context API for global state, useState for local

### Testing

- **Coverage**: Aim for >80% code coverage
- **Naming**: `[Method]_Should[Behavior]_When[Condition]`
- **Structure**: Arrange-Act-Assert pattern
- **Mocks**: Use NSubstitute for .NET, minimal mocks
- **Builders**: Use Test Data Builders for domain objects

## Performance Considerations

### Backend

- **Singleton Services**: Use singleton for VerseService
- **In-Memory Data**: No database queries needed
- **Minimal Dependencies**: Keep dependencies minimal

### Frontend

- **Code Splitting**: Automatic with Vite
- **Lazy Loading**: Lazy load routes if needed
- **Image Optimization**: Optimize images before adding
- **Bundle Size**: Monitor bundle size, avoid large dependencies

## Security Considerations

### Backend

- **CORS**: Restrict `ALLOWED_ORIGIN` in production
- **Input Validation**: Validate query parameters
- **Error Messages**: Don't expose internal errors

### Frontend

- **XSS Prevention**: React escapes by default
- **localStorage**: No sensitive data in localStorage
- **HTTPS**: Always use HTTPS in production

## Monitoring & Debugging

### Local Development

- **Backend Logs**: Console output from `dotnet run`
- **Frontend Logs**: Browser console
- **Network Tab**: Check API requests/responses

### Production

- **CloudWatch Logs**: Lambda function logs
- **Health Endpoint**: Monitor `/api/health`
- **CloudFront Metrics**: Monitor distribution metrics

## Best Practices Summary

1. **Write Tests First**: TDD when possible
2. **Small Commits**: Commit often, small changes
3. **Document Changes**: Update docs with code changes
4. **Review Code**: Always get code review
5. **Test Locally**: Test before pushing
6. **Monitor Production**: Watch logs and metrics
7. **Incremental Changes**: Small, focused changes
8. **Follow Conventions**: Stick to project conventions
