/* eslint-disable no-unused-vars */
import React, { useState } from 'react';
import logo from "../../assets/logo.jpg";
import {
  LayoutDashboard,
  Users,
  Settings,
  FileText,
  ChevronDown,
  Menu,
  Target
} from 'lucide-react';

{/* Menu items - declarando*/}
const menuItems = [
  {
    id: "dashboard",
    icon: LayoutDashboard,
    label: "Dashboard",
    active: true,
    badge: "New"
  },
  
  {
  id: "goals",
  icon: Target,
  label: "Metas"
  },

  {
    id: "settings",
    icon: Settings,
    label: "Settings",
    active: false
  },

  {
    id: "documents",
    icon: FileText,
    label: "Documents",
    active: false
  },
  
  {
  id: "login",
  icon: Users,
  label: "Login"
},
];

{/*Sidebar função */}
function Sidebar({collapsed, onToggle, currentPage, onPageChange}) 

{
  const [openMenu, setOpenMenu] = useState(null);

  const toggleMenu = (id) => {
    setOpenMenu(openMenu === id ? null : id);
  };

  return (
    <div className={` h-screen bg-white dark:bg-gray-800 shadow-md flex flex-col ${collapsed ? 'w-16' : 'w-64'}`}>

      {/* logo e texto */}
      <div className="p-4 border-none border-slate-200/50 dark:border-slate-600/40">
        <div className="flex items-center space-x-3">
          <img
            src={logo}
            alt="Logo"
            className="w-8 h-8 rounded-xl "
          />
          {!collapsed && (
            <div>
              <h1 className="text-lg font-semibold text-gray-800 dark:text-gray-200">
                Cobrança
              </h1>
              <p className="text-sm text-gray-500 dark:text-gray-400">
                Painel
              </p>
            </div>
          )}
        </div>
      </div>
      


      {/* Navigation */}
      <nav className="flex-1 p-4 space-y-2 overflow-y-auto">
          {menuItems.map((item) => {
          const Icon = item.icon;
          const isOpen = openMenu === item.id;

          const handleClick = () => {
            // toggle submenu if present
            if (item.submenu) toggleMenu(item.id);
            if (onPageChange) onPageChange(item.id);
          };

         
          return (
            
            // Menu de Itens
            <div key={item.id} className="relative">
              <button
                onClick={handleClick}
                className={`
                  flex items-center
                  ${collapsed ? "justify-center" : "justify-between"}
                  w-full
                  p-2
                  rounded-xl
                  hover:bg-slate-100
                  dark:hover:bg-slate-700
                  transition-all
                  duration-100
                  ${
                    currentPage === item.id 
                      ? "text-white"
                      : "text-gray-700 dark:text-gray-300"
                  }
                `}
  >
                <div className="flex items-center space-x-3">
                  <Icon className="w-5 h-5 text-gray-500 dark:text-gray-400" />
                  {/* Texto do item */}
                  {!collapsed && (
                    <span className="text-gray-700 dark:text-gray-300">
                      {item.label}
                    </span>
                  )}
                  {/* Badge (New)*/}
                  {!collapsed && item.badge && (
                    <span className="bg-blue-500 text-white text-xs font-bold px-2 py-1 rounded-full">
                      {item.badge}
                    </span>
                  )}
                  
                </div>

                  {/* Indicador de submenu - users */}
                {!collapsed && item.submenu && (
                  <ChevronDown
                    className={`w-4 h-4 text-gray-500 transition-transform duration-300 ${!collapsed && item.submenu &&
                      isOpen ? "rotate-180" : ""
                    }`}
                  />
                )}
              </button>
              
            {/* Submenu suspenso: user e adm - estado open*/}
            {!collapsed && item.submenu && isOpen && (
    <div
      className="
        mt-2
        top-0
        ml-4
        w-42
        bg-white
        dark:bg-gray-800
        rounded-xl
        shadow-xl
        border
        border-slate-200
        dark:border-slate-700
        p-2
        z-50
      "
    >
      {!collapsed && item.submenu.map((subItem) => (
        <button
          key={subItem.id}
          onClick={() => onPageChange && onPageChange(subItem.id)}
          className="
            w-full
            text-left
            px-3
            py-2
            rounded-lg
            text-sm
            text-gray-700
            dark:text-gray-300
            hover:bg-slate-100
            dark:hover:bg-slate-700
            transition-colors
          "
        >
          {subItem.label}
        </button>
      ))}
  </div>
)}
            </div>
          );
        })}
      </nav>

      {/* Footer */}
      <div className="p-4 border-t border-slate-200/50 dark:border-slate-600/40">
  {!collapsed && (
    <div className="flex items-center justify-center gap-2">
      <span className="relative flex h-3 w-3">
        <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
        <span className="relative inline-flex rounded-full h-3 w-3 bg-green-500"></span>
      </span>

      <p className="text-xs text-gray-700 dark:text-gray-300">
        Sistema Online
      </p>
    </div>
  )}
</div>
    </div>
  );
}

export default Sidebar;