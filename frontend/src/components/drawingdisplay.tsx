import React from 'react';

interface DrawingDisplayProps {
  drawingData: string;
}

const DrawingDisplay: React.FC<DrawingDisplayProps> = ({ drawingData }) => {
  return (
    <div>
      <img src={drawingData} alt="Drawing" />
    </div>
  );
};

export default DrawingDisplay;
