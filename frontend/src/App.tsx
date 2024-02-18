import React, { useState } from 'react';
import DrawingScreen from './components/drawingscreen';

const App: React.FC = () => {
  const [, setPlayerName] = useState('');

  return (
    <div>
      <DrawingScreen setPlayerName={setPlayerName} />
    </div>
  );
};

export default App;
