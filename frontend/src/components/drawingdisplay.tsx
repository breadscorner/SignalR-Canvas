import React from 'react';

interface DrawingDisplayProps {
  drawingData: string;
}

const DrawingDisplay: React.FC<DrawingDisplayProps> = ({ drawingData }) => {
  return (
    <div>
      <img src={drawingData} alt="Drawing" />
      {/* Create a form to input a word that will connect to see if it matches a word in the db */}
      <form>
        <label htmlFor="guess">Guess the word:</label>
        <input type="text" id="guess" name="guess" />
        <input type="submit" value="Submit" />
      </form>
    </div>
  );
};

export default DrawingDisplay;
