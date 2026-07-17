import { useState, useEffect } from 'react';
import { useTransacoes } from '../hooks/useTransacoes';
import { usePessoas } from '../hooks/usePessoas';
import { CriarTransacaoDto, TipoTransacao, Pessoa } from '../types';
import FormMessage from '../components/FormMessage';

function formatarMoeda(valor: number): string {
  return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(valor);
}

export default function TransacoesPage() {
  const { transacoes, carregando, erro, cadastrar, limparErro } = useTransacoes();
  const { pessoas } = usePessoas();

  const [descricao, setDescricao] = useState('');
  const [valor, setValor] = useState('');
  const [tipo, setTipo] = useState<TipoTransacao>('Despesa');
  const [pessoaId, setPessoaId] = useState('');
  const [sucesso, setSucesso] = useState<string | null>(null);
  const [pessoaSelecionada, setPessoaSelecionada] = useState<Pessoa | null>(null);

  useEffect(() => {
    const encontrada = pessoas.find(p => p.id === pessoaId) ?? null;
    setPessoaSelecionada(encontrada);
    if (encontrada && encontrada.idade < 18 && tipo === 'Receita') {
      setTipo('Despesa');
    }
  }, [pessoaId, pessoas, tipo]);

  const menorSelecionado = pessoaSelecionada && pessoaSelecionada.idade < 18;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    limparErro();
    setSucesso(null);
    if (!descricao.trim() || !valor || !pessoaId) return;

    const dto: CriarTransacaoDto = {
      descricao: descricao.trim(),
      valor: parseFloat(valor),
      tipo,
      pessoaId,
    };

    const ok = await cadastrar(dto);
    if (ok) {
      setDescricao('');
      setValor('');
      setTipo('Despesa');
      setPessoaId('');
      setSucesso('Transação cadastrada com sucesso!');
    }
  };

  return (
    <div>
      <h2 className="text-xl font-semibold text-gray-800 mb-4">Cadastro de Transações</h2>

      <form onSubmit={handleSubmit} className="bg-white p-4 rounded-lg shadow mb-6 space-y-3">
        <div className="flex gap-3 flex-wrap">
          <input
            type="text"
            placeholder="Descrição"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            className="border border-gray-300 rounded-md px-3 py-2 flex-1 min-w-[200px] focus:ring-2 focus:ring-blue-500 focus:outline-none"
            required
          />
          <input
            type="number"
            placeholder="Valor"
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            className="border border-gray-300 rounded-md px-3 py-2 w-36 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            step="0.01"
            min="0.01"
            required
          />
          <select
            value={tipo}
            onChange={(e) => setTipo(e.target.value as TipoTransacao)}
            className="border border-gray-300 rounded-md px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white"
          >
            <option value="Despesa">Despesa</option>
            <option value="Receita" disabled={!!menorSelecionado}>Receita</option>
          </select>
          <select
            value={pessoaId}
            onChange={(e) => setPessoaId(e.target.value)}
            className="border border-gray-300 rounded-md px-3 py-2 focus:ring-2 focus:ring-blue-500 focus:outline-none bg-white min-w-[180px]"
            required
          >
            <option value="">Selecione uma pessoa</option>
            {pessoas.map((p) => (
              <option key={p.id} value={p.id}>
                {p.nome} ({p.idade} anos)
              </option>
            ))}
          </select>
          <button
            type="submit"
            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md font-medium"
          >
            Cadastrar
          </button>
        </div>
        {menorSelecionado && (
          <p className="text-amber-600 text-sm">
            Menores de 18 anos só podem registrar despesas
          </p>
        )}
        <FormMessage erro={erro} sucesso={sucesso} />
      </form>

      {carregando ? (
        <p className="text-gray-500">Carregando...</p>
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Descrição</th>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Valor</th>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Tipo</th>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Pessoa</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {transacoes.map((t) => (
                <tr key={t.id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 text-sm text-gray-900">{t.descricao}</td>
                  <td className="px-4 py-3 text-sm text-gray-600">{formatarMoeda(t.valor)}</td>
                  <td className="px-4 py-3 text-sm text-gray-600">{t.tipo}</td>
                  <td className="px-4 py-3 text-sm text-gray-600">{t.pessoaNome}</td>
                </tr>
              ))}
              {transacoes.length === 0 && (
                <tr>
                  <td colSpan={4} className="px-4 py-6 text-center text-sm text-gray-500">
                    Nenhuma transação cadastrada
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}