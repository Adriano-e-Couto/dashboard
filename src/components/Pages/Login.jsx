import { useState, useEffect } from 'react';
import api from '../../services/api';

function Login() {
    const [colaboradores, setColaboradores] = useState([]);
    const [selectedId, setSelectedId] = useState('');
    const [senha, setSenha] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Busca os colaboradores ao carregar a tela
    useEffect(() => {
        api.get('/usuarios/colaboradores')   // ← Mudei para o controller que você criou
            .then(res => {
                setColaboradores(res.data);
                setLoading(false);
            })
            .catch(err => {
                console.error("Erro ao carregar colaboradores", err);
                setError("Erro ao carregar lista de colaboradores");
                setLoading(false);
            });
    }, []);

    const handleLogin = (e) => {
        e.preventDefault();
        if (!selectedId) {
            alert("Selecione uma pessoa!");
            return;
        }
        console.log("Tentativa de Login - ID:", selectedId, "Senha:", senha);
        // TODO: Chamar endpoint de autenticação real
        alert("Login simulado com sucesso! (implementar JWT depois)");
    };

    if (loading) return <p>Carregando colaboradores...</p>;
    if (error) return <p style={{color: 'red'}}>{error}</p>;

    return (
        <div style={{ maxWidth: '400px', margin: '50px auto', padding: '20px' }}>
            <h2>Login - Dashboard de Cobrança</h2>
            <form onSubmit={handleLogin}>
                <select 
                    value={selectedId} 
                    onChange={(e) => setSelectedId(e.target.value)}
                    required
                    style={{ width: '100%', padding: '10px', marginBottom: '10px' }}
                >
                    <option value="">Selecione uma pessoa</option>
                    {colaboradores.map(colab => (
                        <option key={colab.id} value={colab.id}>
                            {colab.nome}
                        </option>
                    ))}
                </select>

                <input 
                    type="password" 
                    placeholder="Senha" 
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                    required 
                    style={{ width: '100%', padding: '10px', marginBottom: '10px' }}
                />

                <button 
                    type="submit"
                    style={{ width: '100%', padding: '12px', background: '#6366f1', color: 'white', border: 'none', borderRadius: '6px' }}
                >
                    Entrar
                </button>
            </form>
        </div>
    );
}

export default Login;   // ← ESSA LINHA ESTAVA FALTANDO!