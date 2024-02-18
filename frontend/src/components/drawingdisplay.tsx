import React from 'react';

interface DrawingDisplayProps {
  imageData: string;
}

const DrawingDisplay: React.FC<DrawingDisplayProps> = ({ imageData }) => {
  return (
    <div>
      <img src={imageData} alt="Drawing" />
    </div>
  );
};

export default DrawingDisplay;
