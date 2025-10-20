# Overview
## Overall
Live track on ongoing badminton games that you can see each game individually, updated using current data for each person sent from backend, carousel thar cycles through ongoing games
### Backend
WebAPP C#. an event simulator to make this data that passes every couple of seconds, uses SignalR
### Middle
Using CORS to communicate between the two
### Frontend
React - Vite with embla libs and SignalR service to pass through. External CSS files for now, could use Tailwind after

# Setup
Clone or copy this into your directory
## .env
Create a .env in server for
CORS_ALLOWED_ORIGIN=http://localhost:5173

or whichever you host your frontend on

Create a .env in client for
VITE_SIGNALR_URL=http://localhost:5139/matchHub

or again whichever you wish to host on

## Install and run
### Server
dotnet restore
dotnet run
### Client
npm install
npm run dev


### Other fixes
Data coming in for matches is 1-4, similarly, generation is hardcoded for 4x courts, this should not be static either passed from something else or generated as each new id is loaded

### Next steps
-Docker implementation
-Originally was going to use RabbitMQ for the live data from external source but did not have a local installation unless I wanted to dockerise, so a separate push and pull to RabbitMQ to simulate an outside source better
-Login page with JWT to give uses a token to protect routes with "[Authorize]"
-Unit tests
-More detailed error logging and try at failure points
-Implementing tailwind to further design, pagelayout, UI functionality 