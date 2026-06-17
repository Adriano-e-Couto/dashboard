/* eslint-disable no-unused-vars */

import React, {useState, useEffect} from 'react';
import Sidebar from './components/Layout/Sidebar';
import Header from './components/Layout/Header';
import Dashboard from './components/Layout/Dashboard/Dashboard';
// import Usuarios from './components/Pages/Usuarios';
import Metas from  "./components/Pages/Metas";
import Login from "./components/Pages/Login";
import Documents from "./components/Pages/Documents";

function App() {
  const [sideBarCollapsed, setSideBarCollapsed] = React.useState(false);
  const [currentPage, setCurrentPage] = React.useState("dashboard");
  const [user, setUser] = useState(null);
  const [entries, setEntries] = React.useState([
  { nome: "João", valor: 100, data: "2026-06-01" },
  { nome: "Maria", valor: 200, data: "2026-06-02" },
  { nome: "João", valor: 150, data: "2026-06-03" },
]);
const [metaSemanal, setMetaSemanal] = React.useState(30000);
const [metaMensal, setMetaMensal] = React.useState(120000);

// Temas
// Alterna a classe "dark" no elemento <html>
// permitindo que o Tailwind aplique as classes dark:

// Recupera o tema salvo no navegador 
const [darkMode, setDarkMode] = useState(() => {
  return localStorage.getItem("theme") === "dark";
});
useEffect(() => {
  console.log("DarkMode:", darkMode);

  document.documentElement.classList.toggle(
    "dark",
    darkMode
  ); // Set - preferência
    localStorage.setItem(
    "theme",
    darkMode ? "dark" : "light"
  );
}, [darkMode]);

  return (
    
  
      

      <div className="min-h-screen bg-linear-to-br from-slate-50 via-blue-50 to-indigo-50 dark:from-slate-900 dark:via-slate-800 dark:to-slate-700 transition-all duration-500">
        {/* Sidebar - estado*/}
        <div className="flex h-screen overflow-hidden">
          
          <Sidebar
            collapsed={sideBarCollapsed}
            currentPage={currentPage}
            onPageChange={setCurrentPage}
            user={user}
          />
          <div className="flex-1 flex flex-col overflow-hidden">

            {/* Header */}
           <Header
              onToggleSidebar={() =>
                setSideBarCollapsed(!sideBarCollapsed)
              }
              darkMode={darkMode}
              setDarkMode={setDarkMode}
              user={user}
/>
  
            <main className="flex-1 p-6 overflow-y-auto">
              {/* Conteúdo principal do dashboard */}
              
               {currentPage === "dashboard" && (
                  <Dashboard entries={entries} />
  )}
  {currentPage === "login" && (
            <Login onLogin={setUser} />
          )}
              
              {/* Metas */}  
              {currentPage === "goals" && (
    <Metas />
              
  )}
              {currentPage === "documents" && (
              <Documents />
            )}
            </main>
          </div>
        </div>


      </div>
    
  );
}

export default App;