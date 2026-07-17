import axios from 'axios';
import { ApiError } from '../types';

const client = axios.create({
  baseURL: 'http://localhost:5000/api',
  headers: { 'Content-Type': 'application/json' },
});

client.interceptors.response.use(
  (response) => response,
  (error) => {
    const data = error.response?.data as ApiError | undefined;
    const mensagem = data?.mensagem ?? 'Erro de conexão com o servidor';
    return Promise.reject(new Error(mensagem));
  }
);

export default client;