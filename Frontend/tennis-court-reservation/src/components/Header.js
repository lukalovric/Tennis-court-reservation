// src/components/Header.js
import React from 'react';
import { useNavigate } from 'react-router-dom';

const Header = () => {
  const navigate = useNavigate();

  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const isLoggedIn = !!user.id;
  const isAdmin = user.isAdmin;

  const handleLogout = () => {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    navigate('/login');
  };

  return (
    <header style={headerStyle}>
      {isAdmin && (
        <button onClick={() => navigate('/admin')} style={buttonStyle}>
          Admin
        </button>
      )}
      <h1 style={{ margin: '0 auto' }}>Rezervacija terena</h1>
      {isLoggedIn && (
        <button onClick={handleLogout} style={buttonStyle}>
          Logout
        </button>
      )}
    </header>
  );
};

const headerStyle = {
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  padding: '10px 20px',
  backgroundColor: '#3498db',
  color: '#fff',
};

const buttonStyle = {
  backgroundColor: '#e74c3c',
  color: '#fff',
  border: 'none',
  padding: '5px 10px',
  borderRadius: '4px',
  cursor: 'pointer',
};

export default Header;
