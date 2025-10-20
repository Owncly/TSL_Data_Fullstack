import useMatchEvents from "./hooks/useMatchEvents";
import GameCarousel from "./components/GameCarousel";
import "./styles/index.css";

function App() {
  const events = useMatchEvents();

  return (
    <div className="centered-page">
      <h1>Badminton Matches</h1>
      <GameCarousel events={events} />
    </div>
  );
}

export default App;