# Testing Guidelines

## Testing Philosophy

- **Test Behavior, Not Implementation**: Focus on what the code does, not how
- **Fast Feedback**: Tests should run quickly
- **Isolation**: Tests should be independent and isolated
- **Maintainability**: Tests should be easy to read and maintain
- **Coverage**: Aim for meaningful coverage, not just high percentages

## Backend Testing (.NET)

### Test Framework

- **Framework**: xUnit
- **Mocking**: NSubstitute
- **Builders**: Test Data Builders for domain objects

### Test Structure

Follow the **Arrange-Act-Assert** pattern:

```csharp
[Fact]
public void GetRandom_ShouldReturnVerse_WhenLanguageIsValid()
{
    var sut = new VerseService();
    
    var actual = sut.GetRandom("en");
    
    Assert.NotNull(actual);
    Assert.NotNull(actual.Verse);
    Assert.NotEmpty(actual.Verse.Text);
}
```

### Naming Convention

Format: `[MethodName]_Should[ExpectedBehavior]_When[Condition]`

**Examples:**
- `GetRandom_ShouldReturnVerse_WhenLanguageIsValid`
- `GetByIndex_ShouldReturnSpecificVerse_WhenIndexExists`
- `GetRandom_ShouldThrowException_WhenLanguageIsInvalid`

### Test Data Builders

**When to Use:**
- Domain objects with multiple properties
- DTOs with many fields
- Complex objects used in tests

**Example:**

```csharp
public class VerseBuilder
{
    private string _text = "Default text";
    private string _reference = "Default 1:1";
    private int _index = 0;

    public VerseBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public VerseBuilder WithReference(string reference)
    {
        _reference = reference;
        return this;
    }

    public Verse Build() => new Verse
    {
        Text = _text,
        Reference = _reference,
        Index = _index
    };
}

// Usage in test
var verse = new VerseBuilder()
    .WithText("Test verse")
    .WithReference("Test 1:1")
    .Build();
```

### Mocking with NSubstitute

**When to Use:**
- Interface dependencies
- External services
- Complex dependencies

**Example:**

```csharp
var mockLogger = Substitute.For<ILogger>();
mockLogger.Log(Arg.Any<string>()).Returns(true);

var sut = new MyService(mockLogger);
var actual = sut.DoSomething();

mockLogger.Received(1).Log(Arg.Any<string>());
```

### Test Organization

**File Structure:**
```
tests/
└── Encourager.Api.Tests/
    ├── Services/
    │   └── VerseServiceTests.cs
    └── Endpoints/
        └── VerseEndpointTests.cs
```

**Class Structure:**
```csharp
public class VerseServiceTests
{
    [Fact]
    public void GetRandom_ShouldReturnVerse_WhenLanguageIsValid()
    {
        // Test implementation
    }

    [Fact]
    public void GetRandom_ShouldThrowException_WhenLanguageIsInvalid()
    {
        // Test implementation
    }
}
```

### Code Style Rules

1. **No Comments**: Tests should be self-explanatory
2. **No Blank Lines**: No blank lines between Arrange statements
3. **Blank Lines**: Only between Arrange/Act/Assert sections
4. **Assign to `actual`**: Always assign result to `actual` variable

**Example:**

```csharp
[Fact]
public void GetRandom_ShouldReturnVerse_WhenLanguageIsValid()
{
    var sut = new VerseService();
    var language = "en";
    
    var actual = sut.GetRandom(language);
    
    Assert.NotNull(actual);
    Assert.Equal(language, actual.Language);
}
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific project
dotnet test tests/Encourager.Api.Tests/

# Run single test
dotnet test --filter "FullyQualifiedName~GetRandom"

# Verbose output
dotnet test --verbosity normal
```

## Frontend Testing (E2E)

### Test Framework

- **Framework**: Playwright
- **Language**: TypeScript
- **Browser**: Chromium (configurable)

### Test Structure

```typescript
import { test, expect } from '@playwright/test';

test('should display verse on page load', async ({ page }) => {
  await page.goto('http://localhost:4000');
  
  const verseText = await page.locator('[data-testid="verse-text"]');
  await expect(verseText).toBeVisible();
});
```

### Best Practices

1. **Use Data Attributes**: Prefer `data-testid` over CSS selectors
2. **Wait for Elements**: Use Playwright's auto-waiting
3. **Isolate Tests**: Each test should be independent
4. **Clean State**: Reset state between tests if needed

### Selectors

**Preferred:**
```typescript
// Data test ID
page.locator('[data-testid="verse-text"]')

// Role-based
page.getByRole('button', { name: 'Amen' })

// Text content
page.getByText('Jeremiah 29:11')
```

**Avoid:**
```typescript
// CSS selectors (fragile)
page.locator('.verse-text')

// XPath (complex)
page.locator('//div[@class="verse"]')
```

### Test Organization

**File Structure:**
```
e2e/
├── tests/
│   ├── home.spec.ts
│   ├── daily-blessing.spec.ts
│   └── language.spec.ts
└── playwright.config.ts
```

### Running E2E Tests

```bash
# Install browsers (first time)
npx playwright install --with-deps chromium

# Run tests (headless)
npx playwright test

# Run with browser UI
npm run test:headed

# Run specific test
npx playwright test home.spec.ts

# Debug mode
npx playwright test --debug
```

### Test Scenarios

**Critical Paths to Test:**

1. **Verse Display**
   - Verse loads on page load
   - Verse displays correctly in all languages
   - Verse reference displays

2. **Daily Blessing Rule**
   - New verse on first visit
   - Reflection view after "Amen"
   - Countdown timer works
   - Resets at midnight

3. **Language Switching**
   - Language selector works
   - Verse updates in new language
   - Preference persists

4. **Offline Functionality**
   - App works offline
   - Cached verses display
   - Service worker active

## Test Coverage Goals

### Backend

- **Unit Tests**: >80% code coverage
- **Critical Paths**: 100% coverage
- **Edge Cases**: All handled

### Frontend

- **E2E Tests**: All user flows covered
- **Critical Paths**: 100% coverage
- **Cross-browser**: Test on major browsers

## Writing Good Tests

### Characteristics of Good Tests

1. **Fast**: Run quickly (< 1 second per test)
2. **Independent**: Don't depend on other tests
3. **Repeatable**: Same result every time
4. **Self-Validating**: Pass or fail clearly
5. **Timely**: Written before or with code

### Test Pyramid

```
        /\
       /  \      E2E Tests (Few)
      /____\
     /      \    Integration Tests (Some)
    /________\
   /          \  Unit Tests (Many)
  /____________\
```

**Strategy:**
- **Many Unit Tests**: Fast, isolated, test individual components
- **Some Integration Tests**: Test component interactions
- **Few E2E Tests**: Test complete user flows

### Common Pitfalls

1. **Testing Implementation**: Don't test internal details
2. **Over-Mocking**: Mock only what's necessary
3. **Brittle Tests**: Avoid fragile selectors
4. **Slow Tests**: Optimize test performance
5. **Missing Edge Cases**: Test boundary conditions

## Continuous Integration

### GitHub Actions

Tests run automatically on:
- Pull requests
- Pushes to main/develop
- Scheduled runs (optional)

### Test Reports

- Test results displayed in PR
- Coverage reports (if configured)
- Failed tests block merge

## Debugging Tests

### Backend

```bash
# Run with debugger
dotnet test --logger "console;verbosity=detailed"

# Attach debugger in VS Code
# Set breakpoints and run test
```

### Frontend

```bash
# Debug mode
npx playwright test --debug

# Show browser
npx playwright test --headed

# Slow down execution
npx playwright test --slow-mo=1000
```

## Test Data Management

### Backend

- **Static Data**: Use in-memory verse data
- **Builders**: Create test objects with builders
- **Fixtures**: Reusable test data

### Frontend

- **Mock API**: Use MSW (Mock Service Worker) if needed
- **Test Data**: Use consistent test data
- **Cleanup**: Clear localStorage between tests

## Maintenance

### Keeping Tests Updated

1. **Update with Code**: Change tests when code changes
2. **Remove Obsolete**: Delete tests for removed features
3. **Refactor Tests**: Keep tests DRY and maintainable
4. **Review Coverage**: Regularly review coverage reports

### Test Documentation

- **Test Names**: Should describe what's being tested
- **Comments**: Only if test logic is complex
- **README**: Document test setup and running

## Resources

- [xUnit Documentation](https://xunit.net/)
- [NSubstitute Documentation](https://nsubstitute.github.io/)
- [Playwright Documentation](https://playwright.dev/)
- [Testing Best Practices](https://testingjavascript.com/)
