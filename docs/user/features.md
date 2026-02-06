# Features

## Core Features

### 1. Random Verse Delivery

- **Purpose**: Provides daily encouragement through scripture
- **Functionality**: 
  - Serves random Bible verses from a curated collection
  - ~50 verses per language (English, Amharic, Finnish)
  - Each verse includes text and Bible reference
- **User Experience**: Verse appears with smooth fade-in animation

### 2. Multi-Language Support

- **Languages**: 
  - English (en)
  - Amharic (አማርኛ) (am)
  - Finnish (fi)
- **Features**:
  - Language selector in UI
  - Preference saved to localStorage
  - Verses automatically translated/displayed in selected language
  - Same verse index shown in different languages when switching

### 3. One Blessing Per Day

- **Purpose**: Encourages daily reflection and intentional engagement
- **Mechanics**:
  - Verse locks only after clicking "Amen" (not on page load)
  - Allows reading without commitment
  - Stored in browser localStorage with timestamp
  - Date comparison checks year-month-day equality
  - Resets automatically at midnight (local time)
- **Reflection View**:
  - Shows saved verse if already blessed today
  - Displays countdown timer to midnight
  - Encourages reflection on the day's verse

### 4. Progressive Web App (PWA)

- **Installation**: Can be installed to device home screen
- **Offline Support**:
  - Service worker caches static assets
  - Network-first strategy for API calls (5min cache)
  - Cache-first for static assets
  - App shell loads immediately offline
- **Features**:
  - Install prompt on supported browsers
  - Works like native app when installed
  - Offline access to previously viewed content

### 5. QR Code Integration

- **Admin Route**: `/admin` page generates permanent QR code
- **Purpose**: Easy sharing and access
- **Usage**: QR codes can be printed, displayed, or shared digitally

### 6. Celebration & Animation

- **Confetti Effect**: Canvas-confetti animation on "Amen" click
- **Smooth Transitions**: Framer Motion animations for verse display
- **Visual Feedback**: Success view confirms blessing is saved

## User Interface Features

### Design Elements

- **Color Scheme**: 
  - Primary Navy (`#1a374f`)
  - Secondary Green (`#6f9078`)
  - Accent Terracotta (`#d06450`)
  - Background Cream (`#fdfbca`)
- **Typography**: Clean, readable fonts optimized for mobile
- **Layout**: Mobile-first responsive design

### Components

1. **Verse Display**: Large, readable verse text with reference
2. **Amen Button**: Prominent call-to-action button
3. **Language Selector**: Easy language switching
4. **Reflection View**: Peaceful screen for daily reflection
5. **Success View**: Celebration confirmation screen
6. **Countdown Timer**: Visual countdown to next blessing

## Technical Features

### Performance

- **Fast Loading**: Optimized React build with code splitting
- **Efficient Caching**: Smart caching strategies for offline support
- **Lightweight**: Minimal dependencies, fast bundle size

### Accessibility

- **Semantic HTML**: Proper HTML structure for screen readers
- **Keyboard Navigation**: Full keyboard support
- **Color Contrast**: WCAG-compliant color choices

### Browser Support

- **Modern Browsers**: Chrome, Firefox, Safari, Edge (latest versions)
- **Mobile Browsers**: iOS Safari, Chrome Mobile
- **Graceful Degradation**: Core features work in older browsers

## Future Enhancements

Potential features for future versions:

- Additional languages
- Verse sharing capabilities
- Reading plans
- Personal notes/journaling
- Verse categories/themes
- Audio narration

## Feature Comparison

| Feature | Web App | PWA Installed | Offline |
|---------|---------|---------------|---------|
| View Verses | ✅ | ✅ | ✅ (cached) |
| Daily Blessing | ✅ | ✅ | ✅ |
| Language Selection | ✅ | ✅ | ✅ |
| Offline Access | ⚠️ Limited | ✅ Full | ✅ |
| Install Prompt | ❌ | N/A | N/A |

## Usage Scenarios

### Scenario 1: First-Time User
1. Scans QR code
2. Sees random verse
3. Reads verse
4. Clicks "Amen"
5. Sees celebration
6. Blessing locked for the day

### Scenario 2: Returning User (Same Day)
1. Opens app
2. Sees reflection view
3. Views saved verse
4. Sees countdown timer
5. Reflects on verse

### Scenario 3: Language Switch
1. User changes language
2. Same verse index loads in new language
3. Language preference saved
4. Future visits use new language

### Scenario 4: Offline Usage
1. User installs PWA
2. Visits app while online
3. Goes offline
4. Can still view cached verses
5. New verses require connection
