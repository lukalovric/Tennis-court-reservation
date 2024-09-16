import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import axios from '../axiosConfig';

const Reservation = () => {
  const { courtId } = useParams();
  const location = useLocation();
  const [reservations, setReservations] = useState([]);
  const [selectedDate, setSelectedDate] = useState('');
  const navigate = useNavigate();
  
  const courtName = location.state?.courtName || 'Court';

  useEffect(() => {
    const fetchReservations = async () => {
      try {
        const reservationsResponse = await axios.get(`/reservation?courtId=${courtId}`);
        setReservations(reservationsResponse.data);
      } catch (error) {
        console.error('Failed to fetch reservations:', error);
      }
    };

    fetchReservations();
  }, [courtId]);

  const days = Array.from({ length: 7 }, (_, i) => new Date(Date.now() + i * 86400000).toISOString().split('T')[0]);
  
  const hours = Array.from({ length: 12 }, (_, i) => `${8 + i}:00`);

  const isReserved = (date, time) => {
    return reservations.some(reservation => 
      reservation.reservationDate.startsWith(date) && 
      reservation.reservationDate.includes(time)
    );
  };

  const handleReserve = async (date, time) => {
    let user = null;
    try {
      user = JSON.parse(localStorage.getItem('user'));
    } catch (error) {
      console.error('Failed to parse user data:', error);
      alert('User data is not available. Please log in again.');
      return;
    }
  
    console.log('Date:', date);
    console.log('Time:', time); 
  
    const dateRegex = /^\d{4}-\d{2}-\d{2}$/; 
    const timeRegex = /^\d{2}:\d{2}$/; 
  
    if (!dateRegex.test(date) || !timeRegex.test(time)) {
      console.error('Invalid date or time format:', { date, time });
      alert('Invalid date or time format. Please try again.');
      return;
    }
  
    const dateTimeString = `${date}T${time}:00`; 
  
    console.log('Combined DateTime String:', dateTimeString);
  
    let reservationDate;
    try {
      reservationDate = new Date(dateTimeString);
  
      if (isNaN(reservationDate.getTime())) {
        throw new Error('Invalid date object created.');
      }
    } catch (error) {
      console.error('Error creating date object:', error);
      alert('Failed to create a valid date. Please try again.');
      return;
    }
  
    const formattedDate = reservationDate.toISOString(); 
  
    try {
      await axios.post('/reservation', {
        reservationDate: formattedDate, 
        courtId: courtId,
        userId: user.id 
      });
      alert('Reservation successful!');
      navigate('/courts');
    } catch (error) {
      console.error('Reservation error:', error);
      alert('Failed to make a reservation. Please try again.');
    }
  };
  
  
  

  return (
    <div>
      <h1>{courtName}</h1> {}
      <h2>Available Times for the Next 7 Days</h2>
      {days.map((day) => (
        <div key={day}>
          <h3>{day}</h3>
          <div style={{ display: 'flex', flexWrap: 'wrap' }}>
            {hours.map((hour) => (
              <button
                key={`${day}-${hour}`}
                onClick={() => handleReserve(day, hour)}
                disabled={isReserved(day, hour)} 
                style={{
                  margin: '5px',
                  padding: '10px',
                  borderRadius: '4px',
                  backgroundColor: isReserved(day, hour) ? '#e74c3c' : '#2ecc71',
                  color: '#fff',
                  border: 'none',
                  cursor: isReserved(day, hour) ? 'not-allowed' : 'pointer',
                }}
              >
                {hour}
              </button>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
};

export default Reservation;
