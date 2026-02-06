# Component Relationships

## Backend Components

```mermaid
graph TB
    subgraph "Entry Points"
        Program[Program.cs<br/>Kestrel Server]
        LambdaEntry[LambdaEntryPoint.cs<br/>Lambda Handler]
    end

    subgraph "Configuration"
        AppConfig[AppConfiguration.cs<br/>Shared Config]
    end

    subgraph "Services"
        VerseService[VerseService<br/>Singleton]
    end

    subgraph "Data Layer"
        EnglishVerses[EnglishVerses<br/>~50 verses]
        AmharicVerses[AmharicVerses<br/>~50 verses]
        FinnishVerses[FinnishVerses<br/>~50 verses]
    end

    subgraph "Endpoints"
        VerseEndpoint[GET /api/verse/random]
        HealthEndpoint[GET /api/health]
    end

    Program --> AppConfig
    LambdaEntry --> AppConfig
    AppConfig --> VerseService
    AppConfig --> VerseEndpoint
    AppConfig --> HealthEndpoint
    VerseEndpoint --> VerseService
    VerseService --> EnglishVerses
    VerseService --> AmharicVerses
    VerseService --> FinnishVerses

    style VerseService fill:#1a374f,color:#fff
    style AppConfig fill:#6f9078,color:#fff
```

## Frontend Components

```mermaid
graph TB
    subgraph "Entry Point"
        Main[main.tsx<br/>App Bootstrap]
    end

    subgraph "Routing"
        Router[React Router v7]
        HomeRoute[/ Route]
        AdminRoute[/admin Route]
    end

    subgraph "Context"
        LanguageContext[LanguageContext<br/>Global Language State]
    end

    subgraph "Pages"
        Home[Home.tsx<br/>Main Verse Display]
        Admin[Admin.tsx<br/>QR Generator]
    end

    subgraph "Components"
        VerseDisplay[VerseDisplay.tsx]
        ReflectionView[ReflectionView.tsx]
        SuccessView[SuccessView.tsx]
        Celebration[Celebration.tsx<br/>Confetti]
        InstallPrompt[InstallPrompt.tsx]
    end

    subgraph "Hooks"
        UseLanguage[useLanguage<br/>Language Hook]
    end

    subgraph "i18n"
        Translations[translations.ts<br/>Multi-language Strings]
    end

    subgraph "PWA"
        ServiceWorker[Service Worker<br/>Workbox]
        Manifest[PWA Manifest]
    end

    Main --> Router
    Main --> LanguageContext
    Main --> ServiceWorker
    Router --> HomeRoute
    Router --> AdminRoute
    HomeRoute --> Home
    AdminRoute --> Admin
    Home --> VerseDisplay
    Home --> ReflectionView
    Home --> SuccessView
    Home --> Celebration
    Home --> InstallPrompt
    Home --> UseLanguage
    UseLanguage --> LanguageContext
    LanguageContext --> Translations
    ServiceWorker --> Manifest

    style LanguageContext fill:#1a374f,color:#fff
    style Home fill:#6f9078,color:#fff
    style ServiceWorker fill:#d06450,color:#fff
```

## Data Flow: Daily Blessing Rule

```mermaid
graph LR
    subgraph "User Actions"
        Load[Page Load]
        Amen[Amen Click]
        Refresh[Page Refresh]
    end

    subgraph "State Check"
        CheckStorage{Check localStorage<br/>last_blessing_data}
        CheckDate{Is Same Day?}
    end

    subgraph "States"
        NewBlessing[New Blessing<br/>Show Verse]
        Locked[Locked Today<br/>Show Reflection]
    end

    subgraph "Storage"
        Save[Save to localStorage<br/>timestamp + verse]
        Read[Read from localStorage]
    end

    Load --> CheckStorage
    CheckStorage -->|No Data| NewBlessing
    CheckStorage -->|Has Data| CheckDate
    CheckDate -->|Same Day| Locked
    CheckDate -->|Different Day| NewBlessing
    Amen --> Save
    Save --> Locked
    Refresh --> CheckStorage
    Locked --> Read

    style CheckDate fill:#1a374f,color:#fff
    style Save fill:#6f9078,color:#fff
```

## Component Interaction: Verse Fetching

```mermaid
sequenceDiagram
    participant User
    participant Home
    participant API
    participant VerseService
    participant Storage

    User->>Home: Load Page
    Home->>Storage: Check last_blessing_data
    alt Already Blessed Today
        Storage-->>Home: Return saved verse
        Home->>User: Show ReflectionView
    else New Blessing
        Home->>API: GET /api/verse/random?lang=en
        API->>VerseService: GetRandom(lang)
        VerseService-->>API: Return verse + index
        API-->>Home: JSON response
        Home->>User: Display verse
        User->>Home: Click "Amen"
        Home->>Storage: Save timestamp + verse
        Home->>User: Show SuccessView
    end
```
