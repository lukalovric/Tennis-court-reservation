import React from 'react';
import LoginForm from '../components/LoginForm';
import { useNavigate } from 'react-router-dom';
import axios from '../axiosConfig';

const Login = () => {
  const navigate = useNavigate();

  const handleLogin = async (credentials) => {
    try {
      const response = await axios.post('/user/login', credentials);

      const user = response.data; 

      if (!user || typeof user !== 'object' || !user.id || !user.token) {
        throw new Error('Invalid user data received.');
      }

      localStorage.setItem('token', user.token); 
      localStorage.setItem('user', JSON.stringify(user)); 

      navigate('/courts');
    } catch (error) {
      console.error('Login error:', error); 
      alert('Login failed. Please try again.');
    }
  };

  return <LoginForm onLogin={handleLogin} />;
};

export default Login;
