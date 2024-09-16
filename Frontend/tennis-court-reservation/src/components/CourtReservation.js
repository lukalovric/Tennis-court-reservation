import React, { useState } from 'react';

const CourtReservation = ({ court, onReserve }) => {
  const [selectedDate, setSelectedDate] = useState('');
  const [selectedTime, setSelectedTime] = useState('');

  const days = Array.from({ length: 7 }, (_, i) => new Date(Date.now() + i * 86400000).toISOString().split('T')[0]);
  const hours = Array.from({ length: 12 }, (_, i) => `${8 + i}:00`);

  const handleSubmit = (e) => {
    e.preventDefault();
    onReserve({ courtId: court.id, date: selectedDate, time: selectedTime });
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Reserve {court.name}</h2>
      <select value={selectedDate} onChange={(e) => setSelectedDate(e.target.value)} required>
        <option value="">Select a date</option>
        {days.map((day) => (
          <option key={day} value={day}>
            {day}
          </option>
        ))}
      </select>

      <select value={selectedTime} onChange={(e) => setSelectedTime(e.target.value)} required>
        <option value="">Select a time</option>
        {hours.map((hour) => (
          <option key={hour} value={hour}>
            {hour}
          </option>
        ))}
      </select>

      <button type="submit">Reserve</button>
    </form>
  );
};

export default CourtReservation;
