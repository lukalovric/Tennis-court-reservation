import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import Courts from './pages/Courts';
import Reservation from './pages/Reservation';
import './App.css';
import Header from './components/Header';
import AdminPage from './pages/AdminPage'

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/courts" element={<Courts />} />
        <Route path="/reservation/:courtId" element={<Reservation />} />
        <Route path="/admin" element={<AdminPage />} />
      </Routes>
    </Router>
  );
}

export default App;
