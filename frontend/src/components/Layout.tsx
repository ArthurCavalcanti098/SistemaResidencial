import { NavLink, Outlet } from 'react-router-dom';

export default function Layout() {
  const linkClasses = ({ isActive }: { isActive: boolean }) =>
    `px-4 py-2 text-sm font-medium rounded-md transition-colors ${
      isActive
        ? 'bg-blue-100 text-blue-700'
        : 'text-gray-600 hover:text-gray-900 hover:bg-gray-100'
    }`;

  return (
    <div className="min-h-screen bg-gray-50">
      <nav className="bg-white shadow border-b border-gray-200">
        <div className="max-w-4xl mx-auto px-4 py-3 flex gap-2 items-center">
          <span className="font-semibold text-gray-800 mr-4">Controle de Gastos</span>
          <NavLink to="/pessoas" className={linkClasses}>Pessoas</NavLink>
          <NavLink to="/transacoes" className={linkClasses}>Transações</NavLink>
          <NavLink to="/totais" className={linkClasses}>Totais</NavLink>
        </div>
      </nav>
      <main className="max-w-4xl mx-auto px-4 py-6">
        <Outlet />
      </main>
    </div>
  );
}