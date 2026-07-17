import { useState } from 'react';
import { usePessoas } from '../hooks/usePessoas';
import { CriarPessoaDto } from '../types';
import ConfirmModal from '../components/ConfirmModal';
import FormMessage from '../components/FormMessage';

export default function PessoasPage() {
  const { pessoas, carregando, erro, cadastrar, excluir, limparErro } = usePessoas();
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState('');
  const [sucesso, setSucesso] = useState<string | null>(null);
  const [modalAberto, setModalAberto] = useState(false);
  const [pessoaParaExcluir, setPessoaParaExcluir] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    limparErro();
    setSucesso(null);

    if (!nome.trim()) { setSucesso(null); return; }

    const dto: CriarPessoaDto = { nome: nome.trim(), idade: parseInt(idade, 10) || 0 };
    const ok = await cadastrar(dto);
    if (ok) {
      setNome('');
      setIdade('');
      setSucesso('Pessoa cadastrada com sucesso!');
    }
  };

  const confirmarExclusao = (id: string) => {
    setPessoaParaExcluir(id);
    setModalAberto(true);
  };

  const executarExclusao = async () => {
    if (!pessoaParaExcluir) return;
    limparErro();
    setSucesso(null);
    await excluir(pessoaParaExcluir);
    setModalAberto(false);
    setPessoaParaExcluir(null);
  };

  return (
    <div>
      <h2 className="text-xl font-semibold text-gray-800 mb-4">Cadastro de Pessoas</h2>

      <form onSubmit={handleSubmit} className="bg-white p-4 rounded-lg shadow mb-6 space-y-3">
        <div className="flex gap-3">
          <input
            type="text"
            placeholder="Nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            className="border border-gray-300 rounded-md px-3 py-2 flex-1 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            required
          />
          <input
            type="number"
            placeholder="Idade"
            value={idade}
            onChange={(e) => setIdade(e.target.value)}
            className="border border-gray-300 rounded-md px-3 py-2 w-28 focus:ring-2 focus:ring-blue-500 focus:outline-none"
            min="0"
            max="150"
            required
          />
          <button
            type="submit"
            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md font-medium"
          >
            Cadastrar
          </button>
        </div>
        <FormMessage erro={erro} sucesso={sucesso} />
      </form>

      {carregando ? (
        <p className="text-gray-500">Carregando...</p>
      ) : (
        <div className="bg-white rounded-lg shadow overflow-hidden">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Nome</th>
                <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase">Idade</th>
                <th className="px-4 py-3 text-right text-xs font-medium text-gray-500 uppercase">Ações</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {pessoas.map((p) => (
                <tr key={p.id} className="hover:bg-gray-50">
                  <td className="px-4 py-3 text-sm text-gray-900">{p.nome}</td>
                  <td className="px-4 py-3 text-sm text-gray-600">{p.idade}</td>
                  <td className="px-4 py-3 text-right">
                    <button
                      onClick={() => confirmarExclusao(p.id)}
                      className="bg-red-600 hover:bg-red-700 text-white px-3 py-1 rounded-md text-xs font-medium"
                    >
                      Excluir
                    </button>
                  </td>
                </tr>
              ))}
              {pessoas.length === 0 && (
                <tr>
                  <td colSpan={3} className="px-4 py-6 text-center text-sm text-gray-500">
                    Nenhuma pessoa cadastrada
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      )}

      <ConfirmModal
        open={modalAberto}
        title="Confirmar exclusão"
        message="Ao excluir esta pessoa, todas as transações vinculadas a ela serão removidas permanentemente. Deseja continuar?"
        onConfirm={executarExclusao}
        onCancel={() => { setModalAberto(false); setPessoaParaExcluir(null); }}
      />
    </div>
  );
}