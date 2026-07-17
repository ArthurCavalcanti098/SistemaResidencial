import { useState, useEffect } from 'react';
import { TotaisResponse } from '../types';
import { consultarTotais } from '../api/totaisApi';

function formatarMoeda(valor: number): string {
  return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor);
}

export default function TotaisPage() {
  const [totais, setTotais] = useState<TotaisResponse | null>(null);
  const [carregando, setCarregando] = useState(true);
  const [erro, setErro] = useState<string | null>(null);

  useEffect(() => {
    consultarTotais()
      .then((data) => setTotais(data))
      .catch((e) => setErro(e instanceof Error ? e.message : 'Erro ao carregar totais'))
      .finally(() => setCarregando(false));
  }, []);

  if (carregando) return <p className="text-gray-500">Carregando...</p>;
  if (erro) return <p className="text-red-600">{erro}</p>;
  if (!totais) return null;

  return (
    <div>
      <h2 className="text-xl font-semibold text-gray-800 mb-4">Consulta de Totais</h2>

      <div className="bg-white rounded-lg shadow overflow-hidden mb-6">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Nome</th>
              <th className="px-4 py-3 text-right text-xs font-medium text-gray-500 uppercase">Total Receitas</th>
              <th className="px-4 py-3 text-right text-xs font-medium text-gray-500 uppercase">Total Despesas</th>
              <th className="px-4 py-3 text-right text-xs font-medium text-gray-500 uppercase">Saldo</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200">
            {totais.pessoas.map((p) => (
              <tr key={p.pessoaId} className="hover:bg-gray-50">
                <td className="px-4 py-3 text-sm text-gray-900">{p.nome}</td>
                <td className="px-4 py-3 text-sm text-right text-gray-600">{formatarMoeda(p.totalReceitas)}</td>
                <td className="px-4 py-3 text-sm text-right text-gray-600">{formatarMoeda(p.totalDespesas)}</td>
                <td className={`px-4 py-3 text-sm text-right font-semibold ${p.saldo < 0 ? 'text-red-600' : 'text-gray-900'}`}>
                  {formatarMoeda(p.saldo)}
                </td>
              </tr>
            ))}
            {totais.pessoas.length === 0 && (
              <tr>
                <td colSpan={4} className="px-4 py-6 text-center text-sm text-gray-500">
                  Nenhuma pessoa cadastrada
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      <div className="bg-white rounded-lg shadow p-6">
        <h3 className="text-lg font-semibold text-gray-800 mb-4">Totais Gerais</h3>
        <div className="grid grid-cols-3 gap-4">
          <div>
            <p className="text-xs text-gray-500 uppercase mb-1">Total Receitas</p>
            <p className="text-lg font-semibold text-green-600">
              {formatarMoeda(totais.totaisGerais.totalReceitas)}
            </p>
          </div>
          <div>
            <p className="text-xs text-gray-500 uppercase mb-1">Total Despesas</p>
            <p className="text-lg font-semibold text-red-600">
              {formatarMoeda(totais.totaisGerais.totalDespesas)}
            </p>
          </div>
          <div>
            <p className="text-xs text-gray-500 uppercase mb-1">Saldo Líquido</p>
            <p className={`text-lg font-semibold ${totais.totaisGerais.saldoLiquido < 0 ? 'text-red-600' : 'text-gray-900'}`}>
              {formatarMoeda(totais.totaisGerais.saldoLiquido)}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}