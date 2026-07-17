import client from './client';
import { TotaisResponse } from '../types';

export async function consultarTotais(): Promise<TotaisResponse> {
  const response = await client.get<TotaisResponse>('/totais');
  return response.data;
}