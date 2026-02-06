# Troubleshooting Guide

## Common Issues and Solutions

### Verse Not Loading

**Symptoms:**
- Blank verse display
- Loading spinner never stops
- Error message appears

**Solutions:**
1. **Check Internet Connection**
   - Ensure you have an active internet connection
   - Try loading another website to verify connectivity

2. **Refresh the Page**
   - Pull down to refresh (mobile) or press F5 (desktop)
   - Hard refresh: Ctrl+Shift+R (Windows) or Cmd+Shift+R (Mac)

3. **Clear Browser Cache**
   - Go to browser settings
   - Clear browsing data/cache
   - Reload the app

4. **Check API Status**
   - Verify backend API is running
   - Contact administrator if issue persists

### Language Not Changing

**Symptoms:**
- Language selector doesn't work
- Verse stays in same language
- Language preference not saved

**Solutions:**
1. **First-Time Language Change**
   - Requires internet connection
   - Verse will fetch in new language from API

2. **Refresh After Change**
   - Change language
   - Wait for verse to load
   - Refresh page if needed

3. **Clear localStorage**
   - Open browser developer tools
   - Clear localStorage
   - Reload app and set language again

### Daily Blessing Not Saving

**Symptoms:**
- "Amen" click doesn't save blessing
- Reflection view doesn't appear
- Blessing resets on refresh

**Solutions:**
1. **Check Browser Storage**
   - Ensure localStorage is enabled
   - Check browser storage permissions
   - Verify storage quota not exceeded

2. **Try Different Browser**
   - Some browsers restrict localStorage
   - Try Chrome, Firefox, or Safari

3. **Private/Incognito Mode**
   - Private browsing may restrict storage
   - Use normal browsing mode

### App Not Working Offline

**Symptoms:**
- App doesn't load offline
- "No internet" error
- Service worker not active

**Solutions:**
1. **First Visit Required**
   - Must visit app online first
   - Service worker installs on first visit

2. **Check Service Worker**
   - Open browser developer tools
   - Check Application/Service Workers tab
   - Verify service worker is registered

3. **Reinstall Service Worker**
   - Unregister service worker
   - Clear cache
   - Reload app online
   - Service worker will reinstall

### PWA Installation Issues

**Symptoms:**
- Install prompt doesn't appear
- Can't install to home screen
- App doesn't work when installed

**Solutions:**
1. **Browser Compatibility**
   - Ensure browser supports PWA
   - Chrome, Edge, Safari (iOS) support installation

2. **Install Requirements**
   - Must be served over HTTPS
   - Valid manifest.json required
   - Service worker must be registered

3. **Manual Installation**
   - **Chrome/Edge**: Menu → "Install App"
   - **Safari iOS**: Share → "Add to Home Screen"

### Countdown Timer Issues

**Symptoms:**
- Timer not updating
- Wrong time displayed
- Timer stuck

**Solutions:**
1. **Refresh Page**
   - Timer updates on page refresh
   - Automatic updates every second

2. **Check System Time**
   - Ensure device time is correct
   - Timer uses local device time

3. **Timezone Issues**
   - Timer resets at local midnight
   - Verify device timezone settings

### Performance Issues

**Symptoms:**
- Slow loading
- Laggy animations
- High battery usage

**Solutions:**
1. **Close Other Tabs**
   - Multiple tabs can slow browser
   - Close unnecessary tabs

2. **Update Browser**
   - Use latest browser version
   - Older browsers may be slower

3. **Clear Cache**
   - Clear browser cache
   - Remove old cached files

### QR Code Not Working

**Symptoms:**
- QR code doesn't scan
- Wrong URL opens
- Page not found

**Solutions:**
1. **Check QR Code**
   - Ensure QR code is not damaged
   - Verify QR code points to correct URL

2. **Camera Permissions**
   - Grant camera permissions
   - Try different QR scanner app

3. **Manual Entry**
   - Type URL manually if scanning fails
   - Contact administrator for correct URL

## Browser-Specific Issues

### Chrome/Edge

**Common Issues:**
- Service worker registration
- PWA installation

**Solutions:**
- Check chrome://serviceworker-internals/
- Verify manifest.json validity
- Check HTTPS requirement

### Safari (iOS)

**Common Issues:**
- Limited PWA support
- Service worker restrictions

**Solutions:**
- Use "Add to Home Screen" feature
- Ensure iOS 11.3+ for full support
- Some features may be limited

### Firefox

**Common Issues:**
- PWA installation not available
- Service worker support varies

**Solutions:**
- Core features work without installation
- Use "Add to Home Screen" manually
- Check Firefox version (latest recommended)

## Advanced Troubleshooting

### Developer Tools

**Access Developer Tools:**
- **Chrome/Edge**: F12 or Right-click → Inspect
- **Safari**: Enable Developer menu in preferences
- **Firefox**: F12 or Right-click → Inspect Element

**Useful Checks:**
1. **Console Tab**: Check for JavaScript errors
2. **Network Tab**: Verify API requests/responses
3. **Application Tab**: Check localStorage, service worker status
4. **Storage Tab**: Verify cached files

### localStorage Debugging

**Check Stored Data:**
```javascript
// In browser console
console.log(localStorage.getItem('last_blessing_data'));
console.log(localStorage.getItem('lang'));
```

**Clear Stored Data:**
```javascript
// In browser console
localStorage.removeItem('last_blessing_data');
localStorage.removeItem('lang');
```

### Service Worker Debugging

**Check Service Worker Status:**
1. Open Developer Tools
2. Go to Application/Service Workers tab
3. Verify service worker is registered and active
4. Check for errors in console

**Unregister Service Worker:**
1. Open Developer Tools
2. Application/Service Workers tab
3. Click "Unregister" button
4. Reload page

## Getting Help

If none of these solutions work:

1. **Document the Issue**
   - Note what you were doing when issue occurred
   - Take screenshots if possible
   - Note browser and device information

2. **Contact Support**
   - Reach out to church administrator
   - Provide detailed information about the issue
   - Include browser version and device type

3. **Report Bug**
   - Use appropriate bug reporting channel
   - Include steps to reproduce
   - Provide error messages if any

## Prevention Tips

1. **Keep Browser Updated**: Latest versions have best support
2. **Regular Cache Clearing**: Prevents stale cache issues
3. **Stable Internet**: Use reliable connection for first visit
4. **Install PWA**: Better offline experience when installed
5. **Check Permissions**: Ensure browser has necessary permissions
