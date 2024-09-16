import React, { useState, useEffect } from 'react';
import axios from '../axiosConfig'; 

const AdminPage = () => {
  const [reservations, setReservations] = useState([]);
  const [loading, setLoading] = useState(true);

  
  useEffect(() => {
    const fetchReservations = async () => {
      try {
        const response = await axios.get('/reservation'); 
        setReservations(response.data);
      } catch (error) {
        console.error('Failed to fetch reservations:', error);
        alert('Failed to load reservations. Please try again later.');
      } finally {
        setLoading(false);
      }
    };

    fetchReservations();
  }, []);

  const handleDeleteReservation = async (reservationId) => {
    if (!window.confirm('Are you sure you want to delete this reservation?')) return;

    try {
      await axios.delete(`/reservation/${reservationId}`); 
      setReservations((prev) => prev.filter((res) => res.id !== reservationId)); 
      alert('Reservation deleted successfully.');
    } catch (error) {
      console.error('Failed to delete reservation:', error);
      alert('Failed to delete reservation. Please try again.');
    }
  };

  if (loading) {
    return <p>Loading reservations...</p>;
  }

  if (reservations.length === 0) {
    return <p>No reservations found.</p>;
  }

  return (
    <div>
      <h2>Admin Page - Manage Reservations</h2>
      <table style={tableStyle}>
        <thead>
          <tr>
            <th>Reservation Date</th>
            <th>Court ID</th>
            <th>User ID</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {reservations.map((reservation) => (
            <tr key={reservation.id}>
              <td>{reservation.reservationDate}</td>
              <td>{reservation.courtId}</td>
              <td>{reservation.userId}</td>
              <td>
                <button
                  onClick={() => handleDeleteReservation(reservation.id)}
                  style={deleteButtonStyle}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

const tableStyle = {
  width: '100%',
  borderCollapse: 'collapse',
  marginTop: '20px',
};

const deleteButtonStyle = {
  backgroundColor: '#e74c3c',
  color: '#fff',
  border: 'none',
  padding: '5px 10px',
  borderRadius: '4px',
  cursor: 'pointer',
};

export default AdminPage;
