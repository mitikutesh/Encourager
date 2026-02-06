# Testing PWA Locally

## Step 1: Generate Required Icons

Before testing, you need to create the PWA icon files:

1. Open `public/generate-icons.html` in your browser
2. Click "Download 192x192" button
3. Click "Download 512x512" button
4. Move both downloaded PNG files to the `public/` folder
5. Verify both files are in `frontend/public/`:
   - `pwa-192x192.png`
   - `pwa-512x512.png`

## Step 2: Build and Run the App

### Option A: Development Mode (with PWA enabled)
```bash
cd frontend
npm run dev
```
The dev server runs with PWA enabled (configured in `vite.config.ts`).

### Option B: Production Build (Recommended for full PWA testing)
```bash
cd frontend
npm run build
npm run preview
```

**Note:** PWA features work best in production builds. The preview server will serve your built app.

## Step 3: Test PWA Features

### 3.1 Open in Browser
- Open `http://localhost:5173` (dev) or the preview URL
- Use **Chrome** or **Edge** for best PWA support

### 3.2 Check Service Worker Registration

1. Open **DevTools** (F12 or Cmd+Option+I)
2. Go to **Application** tab
3. Click **Service Workers** in the left sidebar
4. You should see:
   - ✅ Service worker registered and running
   - Status: "activated and is running"
   - Source: `sw.js` or similar

### 3.3 Check Web App Manifest

1. In DevTools **Application** tab
2. Click **Manifest** in the left sidebar
3. Verify:
   - ✅ Name: "Amen — ECCFIN Blessing"
   - ✅ Short name: "Amen"
   - ✅ Icons: Both 192x192 and 512x512 should be listed
   - ✅ Display: "standalone"
   - ✅ Theme color: "#1a374f"

### 3.4 Test Install Prompt

**Desktop (Chrome/Edge):**
- Look for **install icon** (➕) in the address bar
- Click it to install the app
- App should open in standalone window (no browser UI)

**Mobile (or Chrome DevTools Mobile Mode):**
1. Open DevTools (F12)
2. Click **Toggle Device Toolbar** (Cmd+Shift+M / Ctrl+Shift+M)
3. Select a mobile device
4. Look for "Add to Home Screen" prompt or install banner
5. Or manually: Click **⋮** menu → "Install Amen"

### 3.5 Test Offline Functionality

1. In DevTools, go to **Network** tab
2. Check **"Offline"** checkbox (or select "Offline" from throttling dropdown)
3. Refresh the page
4. ✅ App should still load (cached assets)
5. ✅ Service worker serves cached content

### 3.6 Test Install Prompt Component

1. If you haven't installed yet, you should see the **InstallPrompt** component at the bottom
2. Click "Install" button
3. Follow browser's install prompt
4. After installation, the prompt should disappear

### 3.7 Verify Standalone Mode

After installing:
1. Launch the installed app (from desktop/home screen)
2. ✅ Should open in standalone window (no address bar, no browser UI)
3. ✅ Should look like a native app

## Step 4: Common Issues & Solutions

### Icons Not Found
- **Error:** Manifest shows missing icons
- **Solution:** Make sure `pwa-192x192.png` and `pwa-512x512.png` are in `public/` folder

### Stale Service Worker / Cached Content

- **Symptom:** Changes don't appear after refresh, old content persists
- **Solution:**
  1. Open DevTools > **Application** > **Service Workers**
  2. Click **"Unregister"** on the active service worker
  3. Go to **Application** > **Storage** > click **"Clear site data"**
  4. Hard refresh: **Cmd+Shift+R** (macOS) or **Ctrl+Shift+R** (Windows/Linux)
- **Tip:** Enable **"Update on reload"** checkbox in Service Workers panel during development

### Service Worker Not Registering
- **Check:** DevTools Console for errors
- **Solution:** Ensure you're using HTTPS or localhost (PWA requires secure context)

### Install Button Not Showing
- **Check:** Manifest is valid (DevTools > Application > Manifest)
- **Check:** Service worker is registered
- **Check:** App meets PWA criteria (HTTPS, manifest, service worker)

### TypeScript Errors
- **If you see:** `Cannot find module 'virtual:pwa-register'`
- **Solution:** The type declaration file should be in `src/vite-env.d.ts`
- **Fix:** Restart TypeScript server in your IDE

## Step 5: Mobile Testing (Real Device)

1. **Find your local IP:**
   ```bash
   # macOS/Linux
   ifconfig | grep "inet " | grep -v 127.0.0.1
   
   # Windows
   ipconfig
   ```

2. **Start dev server with network access:**
   ```bash
   npm run dev -- --host
   ```

3. **On your phone:**
   - Connect to same WiFi network
   - Open `http://YOUR_IP:5173` in mobile browser
   - Test "Add to Home Screen"
   - Install and verify standalone mode

## Quick Test Checklist

- [ ] Icons generated and in `public/` folder
- [ ] App builds without errors (`npm run build`)
- [ ] Service worker registered (DevTools > Application > Service Workers)
- [ ] Manifest valid (DevTools > Application > Manifest)
- [ ] Install prompt appears (desktop or mobile)
- [ ] App installs successfully
- [ ] App opens in standalone mode
- [ ] Offline mode works (Network tab > Offline)
- [ ] InstallPrompt component shows/hides correctly

## Expected Console Messages

When the app loads, you should see:
```
App ready to work offline
```

When a new version is available:
```
New content available, please refresh
```

These come from the service worker registration in `main.tsx`.
