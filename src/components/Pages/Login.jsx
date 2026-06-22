import { useState, useEffect } from 'react';
import api from '../../services/api';

function Login() {
  const [colaboradores, setColaboradores] = useState([]);
  const [selectedId, setSelectedId] = useState('');
  const [senha, setSenha] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const carregarColaboradores = async () => {
      try {
        const res = await api.get('/usuarios/colaboradores');
        setColaboradores(res.data);
      } catch (err) {
        console.error('Erro ao carregar colaboradores:', err);
        setError('Erro ao carregar lista de colaboradores');
      } finally {
        setLoading(false);
      }
    };

    carregarColaboradores();
  }, []);

  const handleLogin = (e) => {
    e.preventDefault();

    if (!selectedId) {
      alert('Selecione uma pessoa!');
      return;
    }

    console.log('Tentativa de Login');
    console.log('ID:', selectedId);
    console.log('Senha:', senha);

    // Implementar autenticação JWT
    alert('Login simulado com sucesso!');
  };

  if (loading) {
    return (
      <div className="flex items-center justify-center min-h-[70vh]">
        <p className="text-slate-600 dark:text-slate-300">
          Carregando colaboradores...
        </p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex items-center justify-center min-h-[70vh]">
        <p className="text-red-500 font-medium">{error}</p>
      </div>
    );
  }

  return (
    <div className="flex items-center justify-center min-h-[70vh] px-4">
      <div className="w-full max-w-md bg-white dark:bg-slate-800 rounded-2xl shadow-lg p-8 border border-slate-200 dark:border-slate-700">

        <h2 className="text-2xl font-bold text-center mb-6 text-slate-800 dark:text-white">
          Login - Dashboard de Cobrança
        </h2>
        {/* Formulário */}
        <form onSubmit={handleLogin} className="space-y-4">

          <div>
            <label className="block text-sm font-medium mb-2 text-slate-700 dark:text-slate-300">
              Colaborador
            </label>

            <select
              value={selectedId}
              onChange={(e) => setSelectedId(e.target.value)}
              required
              className="w-full p-3 rounded-lg border border-slate-300 dark:border-slate-600 bg-white dark:bg-slate-700 text-slate-800 dark:text-white focus:outline-none focus:ring-2 focus:ring-indigo-500"
            >
              <option value="">Selecione uma pessoa</option>

              {colaboradores.map((colab) => (
                <option key={colab.id} value={colab.id}>
                  {colab.nome}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block text-sm font-medium mb-2 text-slate-700 dark:text-slate-300">
              Senha
            </label>

            <input
              type="password"
              placeholder="Digite sua senha"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
              className="w-full p-3 rounded-lg border border-slate-300 dark:border-slate-600 bg-white dark:bg-slate-700 text-slate-800 dark:text-white placeholder-slate-400 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            />
          </div>

          <button
            type="submit"
            className="w-full py-3 bg-indigo-600 hover:bg-indigo-700 text-white font-medium rounded-lg transition-colors"
          >
            Entrar
          </button>

        </form>
      </div>
    </div>
  );
}

export default Login;