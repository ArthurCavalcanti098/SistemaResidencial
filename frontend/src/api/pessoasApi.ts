import client from './client';
import { Pessoa, CriarPessoaDto } from '../types';

export async function listarPessoas(): Promise<Pessoa[]> {
  const response = await client.get<Pessoa[]>('/pessoas');
  return response.data;
}

export async function criarPessoa(dto: CriarPessoaDto): Promise<Pessoa> {
  const response = await client.post<Pessoa>('/pessoas', dto);
  return response.data;
}

export async function excluirPessoa(id: string): Promise<void> {
  await client.delete(`/pessoas/${id}`);
}