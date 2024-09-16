import React, { useEffect, useState } from 'react';
import CourtList from '../components/CourtList';
import { useNavigate } from 'react-router-dom';
import axios from '../axiosConfig';

const Courts = () => {
  const [courts, setCourts] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCourts = async () => {
      try {
        const response = await axios.get('/court');
        setCourts(response.data);
      } catch (error) {
        console.error('Failed to fetch courts:', error);
      }
    };

    fetchCourts();
  }, []);

  const handleSelectCourt = (court) => {
    navigate(`/reservation/${court.id}`);
  };

  return <CourtList courts={courts} onSelectCourt={handleSelectCourt} />;
};

export default Courts;
