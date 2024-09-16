import React from 'react';

const CourtList = ({ courts, onSelectCourt }) => {
  return (
    <div>
      <h2>Select a Court</h2>
      <ul>
        {courts.map((court) => (
          <li key={court.id}>
            <button onClick={() => onSelectCourt(court)}>{court.name}</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CourtList;
