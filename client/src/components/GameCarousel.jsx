import useEmblaCarousel from "embla-carousel-react";
import GameCard from "./GameCard";
import Autoplay from "embla-carousel-autoplay";

import "../styles/GameCarouselStyle.css";

function GameCarousel({ events }) {
  const autoplayOptions = {
    delay: 5000, //5 seconds rotate
    stopOnInteraction: false,
    stopOnMouseEnter: true
  };

  //using autoplay import to cycle through matches
  const [carouselRef] = useEmblaCarousel(
    { loop: false },
    [Autoplay(autoplayOptions)]
  );


  const groupedGames = {};

  //Groups for gameID to send to each GameCard
  for (let i = 0; i < events.length; i++) {
    const event = events[i];
    if (!groupedGames[event.gameId]) {
      groupedGames[event.gameId] = [];
    }
    groupedGames[event.gameId].push(event);
  }

  const gameList = [];
  //Reformats to be {id, Events[] }
  for (const id in groupedGames) {
    gameList.push({
      gameId: id,
      events: groupedGames[id]
    });
  }


  return (
  <div className="carousel-container">
    <div ref={carouselRef}>
      <div className="carousel-track">
        {gameList.map((game) => (
          <div key={game.gameId} className="carousel-slide">
            <GameCard gameId={game.gameId} events={game.events} />
          </div>
        ))}
      </div>
    </div>
  </div>
);
}

export default GameCarousel;