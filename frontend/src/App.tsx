import React, { useState } from 'react';
import DrawingScreen from './components/drawingscreen';
import DrawingDisplay from './components/drawingdisplay';

const App: React.FC = () => {
  const [playerName, setPlayerName] = useState<string>('');
  const [isDrawer, setIsDrawer] = useState<boolean>(false);
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [drawingData, setDrawingData] = useState<string>('');

  const handleSetPlayerName = (name: string) => {
    if (!playerName && !isDrawer) { // Check if playerName is not set and isDrawer is false
      setPlayerName(name);
      setIsDrawer(true); // The first person to submit their name becomes the drawer
    }
  };

  return (
    <div>
      <h1>Pictionary</h1>
      {playerName && <p>Welcome, {playerName}!</p>}
      {!playerName ? (
        <DrawingScreen setPlayerName={handleSetPlayerName} />
      ) : (
        isDrawer ? (
          <DrawingScreen setPlayerName={() => {}} /> // Drawer sees the drawing screen
        ) : (
          <DrawingDisplay drawingData={drawingData} /> // Viewers see the drawing display
        )
      )}
    </div>
  );
};

export default App;
