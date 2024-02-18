import React, { useState } from 'react';
import DrawingScreen from './components/drawingscreen';

const App: React.FC = () => {
  const [playerName, setPlayerName] = useState('');

  return (
    <div>
      <DrawingScreen playerName={playerName} setPlayerName={setPlayerName} />
    </div>
  );
};

export default App;
