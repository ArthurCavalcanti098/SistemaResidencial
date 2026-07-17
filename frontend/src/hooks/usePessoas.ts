import { useState, useEffect, useCallback } from 'react';
import { Pessoa, CriarPessoaDto } from '../types';
import { listarPessoas, criarPessoa, excluirPessoa } from '../api/pessoasApi';

export function usePessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState<string | null>(null);

  const carregar = useCallback(async () => {
    try {
      setCarregando(true);
      setErro(null);
      const data = await listarPessoas();
      setPessoas(data);
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Erro ao carregar pessoas');
    } finally {
      setCarregando(false);
    }
  }, []);

  useEffect(() => { carregar(); }, [carregar]);

  const cadastrar = async (dto: CriarPessoaDto): Promise<boolean> => {
    try {
      setErro(null);
      await criarPessoa(dto);
      await carregar();
      return true;
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Erro ao cadastrar pessoa');
      return false;
    }
  };

  const excluir = async (id: string): Promise<boolean> => {
    try {
      setErro(null);
      await excluirPessoa(id);
      await carregar();
      return true;
    } catch (e) {
      setErro(e instanceof Error ? e.message : 'Erro ao excluir pessoa');
      return false;
    }
  };

  return { pessoas, carregando, erro, cadastrar, excluir, limparErro: () => setErro(null) };
}