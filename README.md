#Weather App & Custom Toast Package

This project shows a clean and modular Unity architecture.
It has two main parts:
- A reusable Toast / Snackbar package
- A Weather App that uses the Toast package

Toast Package (Reusable Library)
- Built as a separate, reusable package
- Just drag & drop Toast_Main prefab into any scene
- To show a message, simply call:
- ToastSpawner.Show("Your message")
All toast logic is handled inside the package
No hardcoded dependencies
Works consistently on all platforms (Android, iOS, etc.)

Why this design
Keeps UI logic separate from app/game logic
Easy to reuse in other projects
Cleaner and maintainable code

Weather App Architecture
The app is split into clear responsibilities:
Controller (Manager):
- Controls the app flow
- Does NOT handle GPS or API logic
- Tells services what to do
- Sends results to the Toast package

Services
Location Service
- Requests location permission
- Waits for GPS to get coordinates
- Returns Latitude & Longitude
API Service
- Builds the API URL
- Calls the weather API
- Parses JSON response into usable data

API Integration
Uses Open-Meteo API (free & simple)
Sends: Latitude & Longitude
Fetches:
- temperature_2m_max (daily max temperature)
- Displays temperature using the Toast system

Unit Testing (Bonus)
- Used NUnit (Unity standard)
- Focused on logic testing, not live internet
Tests include:
- JSON parsing correctness
- API URL generation with GPS values
Run tests via:
- Window > General > Test Runner
- Edit Mode → Run All
Still improving test setup (will update GitHub)

How to Build
Android
Enable permissions in Player Settings:
- INTERNET
- ACCESS_FINE_LOCATION
- ACCESS_COARSE_LOCATION

iOS
- Add to Info.plist:
- NSLocationWhenInUseUsageDescription
- Required for location permission popup

Final Result
-App asks for location
-Fetches weather data
- Shows temperature using Toast UI
