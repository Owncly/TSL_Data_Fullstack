import * as signalR from "@microsoft/signalr";
const hubUrl = import.meta.env.VITE_SIGNALR_URL;

export function createMatchHubConnection(onReceiveEvent) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

  connection.on("ReceiveEvent", onReceiveEvent);

  return connection;
}