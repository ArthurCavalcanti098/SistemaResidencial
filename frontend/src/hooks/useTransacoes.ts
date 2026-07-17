import { useState, useEffect, useCallback } from 'react';
import { Transacao, CriarTransacaoDto } from '../types';
import { listarTransacoes, criarTransacao } from '../api/transacoesApi';

export function useTransacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState<string | null>(null);

  const carregar = useCallback(async () => {
    try {
      setCarregando(true);
      setErro(null);
      const data = await listarTransacoes();
      setTransacoes(data);
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Erro ao carregar transações');
    } finally {
      setCarregando(false);
    }
  }, []);

  useEffect(() => { carregar(); }, [carregar]);

  const cadastrar = async (dto: CriarTransacaoDto): Promise<boolean> => {
    try {
      setErro(null);
      await criarTransacao(dto);
      await carregar();
      return true;
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Erro ao cadastrar transação');
      return false;
    }
  };

  return { transacoes, carregando, erro, cadastrar, limparErro: () => setErro(null) };
}