import React, { useState, useRef, useEffect } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

interface DrawingScreenProps {
  setPlayerName: (name: string) => void;
}

const useSignalRConnection = (hubUrl: string): HubConnection | null => {
  const [connection, setConnection] = useState<HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    const startConnection = async () => {
      try {
        await newConnection.start();
        console.log('SignalR connection established');
      } catch (err) {
        console.error('Failed to connect with hub', err);
      }
    };

    startConnection();
    setConnection(newConnection);

    return () => {
      newConnection.stop();
    };
  }, [hubUrl]);

  return connection;
};

interface DrawingContentProps {
  connection: HubConnection | null;
}

const DrawingContent: React.FC<DrawingContentProps> = ({ connection }) => {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const [isDrawing, setIsDrawing] = useState<boolean>(false);

  connection?.on('ReceiveDataPoints', (x: number, y: number) => {
    console.log(" from the server" , x, y);
    const canvas = canvasRef.current;
    if (!canvas) return;

    const context = canvas.getContext('2d');
    if (!context) return;

    context.lineTo(x, y);
    context.stroke();
  })

  const startDrawing = (event: React.MouseEvent<HTMLCanvasElement>) => {
    const canvas = canvasRef.current;
    if (!canvas) return;

    const context = canvas.getContext('2d');
    if (!context) return;

    context.beginPath();
    context.moveTo(event.nativeEvent.offsetX, event.nativeEvent.offsetY);
    setIsDrawing(true);
  };

  const draw = (event: React.MouseEvent<HTMLCanvasElement>) => {
    if (!isDrawing) return;
    
    const canvas = canvasRef.current;
    if (!canvas) return;

    const context = canvas.getContext('2d');
    if (!context) return;

    // console.log(event.nativeEvent.offsetX, event.nativeEvent.offsetY)
    // send to signalR
    connection?.invoke('SendDataPoints', event.nativeEvent.offsetX, event.nativeEvent.offsetY).catch(err => console.error(err));

    context.lineTo(event.nativeEvent.offsetX, event.nativeEvent.offsetY);
    context.stroke();
  };

  const finishDrawing = () => {
    if (!isDrawing) return;

    const canvas = canvasRef.current;
    if (!canvas || !connection) return;

    const context = canvas.getContext('2d');
    if (!context) return;

    context.closePath();
    setIsDrawing(false);

    const dataURL = canvas.toDataURL();
    connection.invoke('SendDrawing', dataURL).catch(err => console.error(err));
  };

  useEffect(() => {
    if (!connection) return;

    const handleReceiveDrawing = (drawingData: string) => {
      const canvas = canvasRef.current;
      if (!canvas) return;

      const context = canvas.getContext('2d');
      if (!context) return;

      const image = new Image();
      image.onload = () => {
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.drawImage(image, 0, 0);
      };
      image.src = drawingData;
    };

    connection.on('ReceiveDrawing', handleReceiveDrawing);

    return () => {
      connection.off('ReceiveDrawing', handleReceiveDrawing);
    };
  }, [connection]);

  return (
    <canvas
      ref={canvasRef}
      onMouseDown={startDrawing}
      onMouseMove={draw}
      onMouseUp={finishDrawing}
      onMouseOut={finishDrawing}
      width={800}
      height={600}
      style={{ border: '1px solid black', backgroundColor: 'white'}}
    />
  );
};

const DrawingScreen: React.FC<DrawingScreenProps> = ({ setPlayerName }) => {
  const [name, setName] = useState<string>('');
  const [submitted, setSubmitted] = useState<boolean>(false);
  const connection = useSignalRConnection('/r/pictionaryhub');

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (name.trim() !== '') {
      setPlayerName(name);
      setSubmitted(true);
    } else {
      alert('Please enter your name to join the game.');
    }
  };

  return submitted ? (
    <DrawingContent connection={connection} />
  ) : (
    <div className="flex justify-center">
      <form onSubmit={handleSubmit}>
        <p>Please enter your name to join the game:</p>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Enter your name"
        />
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default DrawingScreen;