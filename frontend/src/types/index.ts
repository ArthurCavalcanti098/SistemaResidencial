export type TipoTransacao = 'Despesa' | 'Receita';

export interface Pessoa {
  id: string;
  nome: string;
  idade: number;
}

export interface CriarPessoaDto {
  nome: string;
  idade: number;
}

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: string;
  pessoaNome: string;
}

export interface CriarTransacaoDto {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: string;
}

export interface TotalPorPessoa {
  pessoaId: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface TotaisResponse {
  pessoas: TotalPorPessoa[];
  totaisGerais: {
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
  };
}

export interface ApiError {
  mensagem: string;
  codigo?: string;
}