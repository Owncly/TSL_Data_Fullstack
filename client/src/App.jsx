import useMatchEvents from "./hooks/useMatchEvents";
import "./styles/index.css";

function App() {
  const events = useMatchEvents();

  return (
    <div className="centered-page">
      <h1>Badminton Matches</h1>
    
    </div>
  );
}

export default App;