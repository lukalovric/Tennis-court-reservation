// src/axiosConfig.js
import axios from 'axios';

// Set up axios with a base URL
const axiosInstance = axios.create({
  baseURL: 'https://localhost:7156/api', // Base URL for all API requests
});

// Export axios instance to be used across your application
export default axiosInstance;
