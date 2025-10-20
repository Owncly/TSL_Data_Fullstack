# Plan
## Overall
Live track on ongoing badminton games that you can see each game individually, updated using current data for each person sent from backend, just in boxes nothing fancy given constraints
## Backend
WebAPP C#. an event simulator to make this data that passes every couple of seconds, should use SignalR
## Middle
Using CORS to communicate between the two
## Frontend
React - Vite with embla libs and SignalR service to pass through. External CSS files for now, could use Tailwind after


### Other fixes
Data coming in for matches is 1-4, similarly, generation is hardcoded for 4x courts, this should not be static either passed from something else or generated as each new id is loaded