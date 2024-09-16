import React, { useState } from 'react';

const RegisterForm = ({ onRegister }) => {
  const [formData, setFormData] = useState({
    fullName: '',
    username: '',
    email: '',
    password: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onRegister(formData);
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Register</h2>
      <input
        type="text"
        placeholder="Full Name"
        name="fullName"
        value={formData.fullName}
        onChange={handleChange}
        required
      />
      <input
        type="text"
        placeholder="Username"
        name="username"
        value={formData.username}
        onChange={handleChange}
        required
      />
      <input
        type="email"
        placeholder="Email"
        name="email"
        value={formData.email}
        onChange={handleChange}
        required
      />
      <input
        type="password"
        placeholder="Password"
        name="password"
        value={formData.password}
        onChange={handleChange}
        required
      />
      <button type="submit">Register</button>
    </form>
  );
};

export default RegisterForm;
