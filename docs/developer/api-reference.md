# API Reference

## Base URL

**Local Development:** `http://localhost:5226`  
**Production:** `https://<api-gateway-url>/Prod`

## Endpoints

### Get Random Verse

Returns a random Bible verse in the specified language.

**Endpoint:** `GET /api/verse/random`

**Query Parameters:**

| Parameter | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `lang` | string | No | `en` | Language code: `en`, `am`, or `fi` |
| `index` | integer | No | random | Specific verse index (0-49) |

**Request Examples:**

```bash
# Random verse in English
GET /api/verse/random?lang=en

# Random verse in Amharic
GET /api/verse/random?lang=am

# Specific verse by index
GET /api/verse/random?lang=en&index=23

# Random verse (defaults to English)
GET /api/verse/random
```

**Response:**

```json
{
  "text": "For I know the plans I have for you, declares the Lord, plans to prosper you and not to harm you, plans to give you hope and a future.",
  "reference": "Jeremiah 29:11",
  "index": 23
}
```

**Response Fields:**

| Field | Type | Description |
|-------|------|-------------|
| `text` | string | The verse text |
| `reference` | string | Bible reference (e.g., "Jeremiah 29:11") |
| `index` | integer | Verse index (0-49) for consistent retrieval |

**Status Codes:**

- `200 OK`: Success
- `400 Bad Request`: Invalid language or index parameter
- `500 Internal Server Error`: Server error

**Example Usage:**

```typescript
// Fetch random verse
const response = await fetch('/api/verse/random?lang=en');
const verse = await response.json();
console.log(verse.text);      // Verse text
console.log(verse.reference); // Bible reference
console.log(verse.index);     // Verse index

// Fetch specific verse by index
const specificVerse = await fetch('/api/verse/random?lang=en&index=23');
const data = await specificVerse.json();
```

### Health Check

Returns API health status and current timestamp.

**Endpoint:** `GET /api/health`

**Request:**

```bash
GET /api/health
```

**Response:**

```json
{
  "status": "healthy",
  "timestamp": "2026-02-06T12:34:56.789Z"
}
```

**Response Fields:**

| Field | Type | Description |
|-------|------|-------------|
| `status` | string | Always returns `"healthy"` |
| `timestamp` | string | Current UTC timestamp in ISO8601 format |

**Status Codes:**

- `200 OK`: Service is healthy

**Example Usage:**

```typescript
const response = await fetch('/api/health');
const health = await response.json();
console.log(health.status);    // "healthy"
console.log(health.timestamp); // ISO8601 timestamp
```

## Language Codes

| Code | Language | Script |
|------|----------|--------|
| `en` | English | Latin |
| `am` | Amharic | Ge'ez (አማርኛ) |
| `fi` | Finnish | Latin |

## Error Responses

### Invalid Language

**Request:** `GET /api/verse/random?lang=invalid`

**Response:** `400 Bad Request`

```json
{
  "error": "Invalid language code"
}
```

### Invalid Index

**Request:** `GET /api/verse/random?index=100`

**Response:** `400 Bad Request`

```json
{
  "error": "Index out of range"
}
```

### Server Error

**Response:** `500 Internal Server Error`

```json
{
  "error": "Internal server error"
}
```

## CORS Configuration

The API supports Cross-Origin Resource Sharing (CORS) for browser requests.

**Configuration:**
- Controlled by `ALLOWED_ORIGIN` environment variable
- Default: `*` (allows all origins)
- Production: Should be set to CloudFront URL

**Headers:**
- `Access-Control-Allow-Origin`: Configured origin
- `Access-Control-Allow-Methods`: `GET, OPTIONS`
- `Access-Control-Allow-Headers`: `Content-Type, Authorization`

## Rate Limiting

Currently, no rate limiting is implemented. Consider implementing rate limiting for production use.

## Caching

**CloudFront Caching:**
- API routes (`/api/*`) use network-first strategy
- Default TTL: 5 minutes (300 seconds)
- Max TTL: 1 hour (3600 seconds)
- Query strings are forwarded to origin

**Client-Side Caching:**
- Service worker caches API responses
- Network-first strategy with 5-minute cache
- Falls back to cache if network fails

## Versioning

Currently, no API versioning is implemented. All endpoints are under `/api/`.

Future versions may use:
- `/api/v1/verse/random`
- `/api/v2/verse/random`

## Testing Endpoints

### Using curl

```bash
# Random verse
curl http://localhost:5226/api/verse/random?lang=en

# Specific verse
curl http://localhost:5226/api/verse/random?lang=en&index=23

# Health check
curl http://localhost:5226/api/health
```

### Using Postman/Insomnia

1. Create new GET request
2. Set URL: `http://localhost:5226/api/verse/random`
3. Add query parameters:
   - `lang`: `en`
   - `index`: `23` (optional)
4. Send request

### Using Browser

```javascript
// In browser console
fetch('/api/verse/random?lang=en')
  .then(res => res.json())
  .then(data => console.log(data));
```

## Implementation Details

### Backend Implementation

**Service:** `VerseService` (singleton)

**Methods:**
- `GetRandom(string lang)`: Returns random verse
- `GetByIndex(string lang, int index)`: Returns specific verse

**Data Sources:**
- `EnglishVerses.Verses`: Array of ~50 English verses
- `AmharicVerses.Verses`: Array of ~50 Amharic verses
- `FinnishVerses.Verses`: Array of ~50 Finnish verses

**Random Selection:**
- Uses `Random.Shared.Next()` for random index
- Ensures uniform distribution

### Frontend Integration

**Fetch Pattern:**
```typescript
const fetchVerse = async (lang: string, index?: number) => {
  const url = index !== undefined
    ? `/api/verse/random?lang=${lang}&index=${index}`
    : `/api/verse/random?lang=${lang}`;
  
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error('Failed to fetch verse');
  }
  return response.json();
};
```

**Error Handling:**
```typescript
try {
  const verse = await fetchVerse('en');
  setVerse(verse);
} catch (error) {
  // Fallback to default verse
  setVerse({
    text: 'Fallback verse text',
    reference: 'Reference',
    index: -1
  });
}
```

## Best Practices

1. **Always Handle Errors**: API calls can fail, implement error handling
2. **Use Index for Consistency**: When switching languages, use index to get same verse
3. **Cache Responses**: Use service worker caching for offline support
4. **Respect Daily Limit**: Don't fetch new verses if user already blessed today
5. **Language Persistence**: Save language preference in localStorage

## Future Enhancements

Potential API improvements:

- Pagination for verse lists
- Verse search/filtering
- Verse categories/themes
- User favorites
- Verse sharing endpoints
- Analytics endpoints
