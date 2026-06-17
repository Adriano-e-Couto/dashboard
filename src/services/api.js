import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5292/api',   // ← URL do seu backend
  headers: {
    'Content-Type': 'application/json'
  }
});

// Interceptor para JWT (futuro)
api.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;     // ← Isso é o mais importante!