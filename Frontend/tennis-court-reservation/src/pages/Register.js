import React from 'react';
import RegisterForm from '../components/RegisterForm';
import { useNavigate } from 'react-router-dom';
import axios from '../axiosConfig';

const Register = () => {
  const navigate = useNavigate();

  const handleRegister = async (data) => {
    try {
      await axios.post('/user/register', data);
      navigate('/login');
    } catch (error) {
      alert('Registration failed. Please try again.');
    }
  };

  return <RegisterForm onRegister={handleRegister} />;
};

export default Register;
