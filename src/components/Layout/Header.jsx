/* eslint-disable no-unused-vars */
import React, { useState } from 'react';
import {
  Filter,
  Menu,
  Plus,
  Search,
  Settings,
  Sun,
  X,
  Moon
} from 'lucide-react';
// Recebendo prop
function Header({onToggleSidebar, darkMode, setDarkMode , user})

{
  const [openModal, setOpenModal] = useState(false);

  const [formData, setFormData] = useState({
    nome: '',
    data: '',
    valor: ''
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    console.log('Nova entrada:', formData);

    setFormData({
      nome: '',
      data: '',
      valor: ''
    });

    setOpenModal(false);
  };

  return (
    <>
      <div className='bg-white/80 dark:bg-gray-800/80 backdrop-blur-xl border-b border-slate-200/50 dark:border-slate-600/40 px-4 py-4'>

        <div className="flex items-center justify-between">

          <div className="flex items-center space-x-4">
            <button className="p-2 text-gray-500 dark:text-gray-400 focus:outline-none hover:bg-gray-500 dark:hover:bg-gray-700 transition-colors" onClick={onToggleSidebar}>
              <Menu className="h-6 w-6" />
            </button>
            {/* Título e Subtítulo */}
            <div className="hidden md:block">
              <h1 className="text-xl font-bold text-gray-800 dark:text-gray-200">
                Dashboard
              </h1>

              <p className="text-sm text-gray-500 dark:text-gray-400">
                Bem-vindo ao painel de controle
              </p>
            </div>
          </div>
          {/* Barra de pesquisa */}
          <div className="flex-1 max-w-md mx-8">
            <div className="relative">
              <Search className="w-4 h-4 absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-500 dark:text-gray-400" />

              <input
                type="text"
                placeholder="Search..."
                className="w-full pl-10 pr-4 py-2 rounded-xl bg-gray-100 dark:bg-gray-700 text-gray-800 dark:text-gray-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:bg-gray-300 dark:focus:bg-gray-600 focus:border-transparent transition-all"
              />
              {/* Filtro */}
              <button className="absolute right-2 top-1/2 transform -translate-y-1/2 py-1.5 bg-gray-700 dark:bg-gray-750 rounded-3xl hover:bg-gray-600 dark:hover:bg-gray-650 transition-colors px-3">
                <Filter className="w-4 h-4 text-gray-500 dark:text-gray-400" />
              </button>
            </div>
          </div>

           

          <div className="flex items-center space-x-4">

            {/* Botão novo - entrada */}
            <button
              onClick={() => setOpenModal(true)}
              className="hidden lg:flex w-8 h-8 rounded-full bg-blue-500 items-center justify-center text-white hover:bg-blue-600 transition-colors"
            >
              <Plus className="w-4 h-4" />
            </button>
            {/* Botão mudança de tema */}
            <button
              onClick={() => setDarkMode(!darkMode)}
              className="p-2 rounded-lg hover:bg-slate-100 dark:hover:bg-slate-700"
            >
              {darkMode ? (
                <Sun className="w-5 h-5 text-yellow-500" />
              ) : (
                <Moon className="w-5 h-5 text-slate-500" />
              )}
          </button>
          
          
            {/* Botão configuração */}
            <button className="p-2 text-gray-500 dark:text-gray-400 focus:outline-none hover:bg-gray-500 dark:hover:bg-gray-700 transition-colors rounded-xl">
              <Settings className="w-4 h-4" />
            </button>


          </div>
        </div>
      </div>

      {/* Modal - Entrada do usuário com os valores */}
      {openModal && (
        <div className="fixed inset-0 bg-black/40 flex items-center justify-center z-50">

          <div className="w-full max-w-md bg-white dark:bg-gray-800 rounded-2xl shadow-2xl p-6">

            <div className="flex items-center justify-between mb-6">
              <h2 className="text-xl font-bold text-gray-800 dark:text-gray-200">
                Nova Entrada
              </h2>

              <button
                onClick={() => setOpenModal(false)}
                className="text-gray-500 hover:text-red-500 transition-colors"
              >
                <X size={20} />
              </button>
            </div>
            {/* Formulário */}
            <form onSubmit={handleSubmit} className="space-y-4">

              <div>
                <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                  Nome
                </label>

                <select
                  name="nome"
                  value={formData.nome}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded-lg bg-white dark:bg-gray-700 dark:border-gray-600 dark:text-white dark:scheme-dark focus:outline-none focus:ring-2 focus:ring-blue-500 "
                >
                  <option value="">Selecione uma pessoa</option>
                  <option value="1">João Silva</option>
                  <option value="2">Maria Souza</option>
                  <option value="3">Pedro Santos</option>
                </select>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1 ">
                  Data
                </label>

                <input
                  type="date"
                  name="data"
                  value={formData.data}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded-lg bg-white dark:bg-gray-700 dark:border-gray-600 dark:text-white dark:scheme-dark focus:outline-none focus:ring-2 focus:ring-blue-500"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                  Valor
                </label>

                <input
                  type="number"
                  step="0.01"
                  name="valor"
                  value={formData.valor}
                  onChange={handleChange}
                  placeholder="0.00"
                  required
                  className="w-full px-3 py-2 border rounded-lg bg-white focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:text-white dark:scheme-dark focus:ring-2 focus:ring-blue-500"
                />
              </div>

              <div className="flex justify-end gap-3 pt-4">

                <button
                  type="button"
                  onClick={() => setOpenModal(false)}
                  className="px-4 py-2 rounded-lg border border-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 dark:text-white transition-colors"
                >
                  Cancelar
                </button>

                <button
                  type="submit"
                  className="px-4 py-2 rounded-lg bg-blue-500 text-white hover:bg-blue-600"
                >
                  Salvar
                </button>

              </div>

            </form>

          </div>

        </div>
      )}
    </>
  );
}

export default Header;