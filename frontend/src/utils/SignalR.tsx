import { useEffect, useState } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

const useSignalRConnection = (hubUrl: string) => {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    const startConnection = async () => {
      try {
        await newConnection.start();
        console.log("SignalR connection established");
      } catch (err) {
        console.error("Error establishing SignalR connection:", err);
      }
    };

    startConnection();

    return () => {
      newConnection.stop().then(() => console.log("SignalR connection stopped"));
    };
  }, [hubUrl]);

  return connection;
};

export default useSignalRConnection;