import "../styles/GameCardStyle.css";

function GameCard({ gameId, events }) {
  return (
    <div className="game-card-container">
      <h2>Game {gameId}</h2>
      {events.map((event) => (
        <div key={event.player} className="player-card">
          <h3>{event.player}</h3>
          <p>Last Action: {event.action ?? "----"}</p>
          <p>Current Points: {event.points}</p>
          <p>
            Last Updated:{" "}
            {event.timestamp
              ? new Date(event.timestamp).toLocaleTimeString()
              : "----"}
          </p>

        </div>
      ))}
    </div>
  );
}

export default GameCard;