import React, { useState } from 'react';

interface GuessAndDrawProps {
  playerName: string;
  isDrawer: boolean;
}

const GuessAndDraw: React.FC<GuessAndDrawProps> = ({ isDrawer }) => {
  const [wordToDraw, setWordToDraw] = useState('');
  const [guess, setGuess] = useState('');
  const [winner, ] = useState('');
  const [nameSubmitted, setNameSubmitted] = useState(false);

  const submitGuess = () => {
    // Guess submission logic
  };

  const handleWordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setWordToDraw(e.target.value);
  };

  const validateName = (name: string) => {
    // Validate the name to ensure it is not empty
    return name.trim() !== '';
  };

  const handleNameSubmit = () => {
    if (validateName(guess)) {
      setNameSubmitted(true);
    } else {
      // Provide feedback to the user that a valid name is required
      alert('Please enter a valid name.');
    }
  };

  return (
    <div>
      {nameSubmitted ? (
        <div>
          <h2>Welcome, {guess}!</h2>
          {isDrawer ? (
            <div>
              <input
                type="text"
                value={wordToDraw}
                onChange={handleWordChange}
                placeholder="Enter word to draw"
              />
              <canvas /* Your canvas drawing component */ />
            </div>
          ) : (
            <div>
              <p>Guess the word:</p>
              <input
                type="text"
                value={guess}
                onChange={(e) => setGuess(e.target.value)}
                placeholder="Your guess"
              />
              <button onClick={submitGuess}>Submit Guess</button>
            </div>
          )}
        </div>
      ) : (
        <div>
          <p>Enter your name:</p>
          <input
            type="text"
            value={guess}
            onChange={(e) => setGuess(e.target.value)}
            placeholder="Your name"
          />
          <button onClick={handleNameSubmit}>Submit</button>
        </div>
      )}
      {winner && <p>{winner} won!</p>}
    </div>
  );
};

export default GuessAndDraw;
