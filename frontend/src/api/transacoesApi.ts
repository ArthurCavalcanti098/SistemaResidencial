import client from './client';
import { Transacao, CriarTransacaoDto } from '../types';

export async function listarTransacoes(): Promise<Transacao[]> {
  const response = await client.get<Transacao[]>('/transacoes');
  return response.data;
}

export async function criarTransacao(dto: CriarTransacaoDto): Promise<Transacao> {
  const response = await client.post<Transacao>('/transacoes', dto);
  return response.data;
}