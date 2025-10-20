import { useEffect, useState, useRef } from "react";
import { createMatchHubConnection } from "../services/signalClient"; 

export default function useMatchEvents() {
  //Making Placeholders so the React is stable
  const [events, setEvents] = useState(() => {
    const placeholders = [];
    for (let gameId = 1; gameId <= 4; gameId++) {
      //Assigning two player per Court that can be filled
      placeholders.push(
        { gameId, player: "[player1]", action: null, points: 0, timestamp: null },
        { gameId, player: "[player2]", action: null, points: 0, timestamp: null }
      );
    }
    return placeholders;
  });

  //Persisting connection 
  const connectionRef = useRef(null);

  useEffect(() => {
    //setting handleGameGetEvent so it can be removed on closure
    const handleGameGetEvent = (newEvent) => {
      setEvents((prev) => {
        const updated = [...prev];

        //check matching player In GameID
        const exactIndex = updated.findIndex(
          (e) => e.gameId === newEvent.gameId && e.player === newEvent.player
        );

        if (exactIndex !== -1) {
          updated[exactIndex] = newEvent;
          return updated;
        }

        //check placeholder values to fill
        const placeholderIndex = updated.findIndex(
          (e) =>
            e.gameId === newEvent.gameId &&
            (e.player === "[player1]" || e.player === "[player2]")
        );

        if (placeholderIndex !== -1) {
          updated[placeholderIndex] = newEvent;
          return updated;
        }

        //give a log warning
        console.warn(
          `Event Not Found: player: '${newEvent.player}' game: ${newEvent.gameId}`
        );
        //don't use broken event data
        return prev;
      });
    };

    const connection = createMatchHubConnection(handleGameGetEvent);
    connectionRef.current = connection;

    connection
      .start()
      .then(() => console.log("SignalR connected"))
      .catch((err) => console.error("SignalR connection error:", err));

    //cleanup
    return () => {
      connection.off("ReceiveEvent", handleGameGetEvent);
      //avoid stopping if already disconnected
      if (connection.state !== "Disconnected") {
        connection.stop().catch((err) => console.warn("SignalR stop error:", err));
      }
    };
  }, []);

  return events;
}