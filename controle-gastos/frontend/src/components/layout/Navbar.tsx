import React from 'react';
import { Link, useLocation } from 'react-router-dom';

const Navbar: React.FC = () => {
  const location = useLocation();

  const navStyle: React.CSSProperties = {
    display: 'flex',
    alignItems: 'center',
    gap: '24px',
    padding: '16px 24px',
    backgroundColor: '#1e293b',
    color: '#fff',
  };

  const linkStyle: React.CSSProperties = {
    color: '#94a3b8',
    textDecoration: 'none',
    fontSize: '14px',
    fontWeight: 500,
    padding: '8px 12px',
    borderRadius: '6px',
    transition: 'all 0.2s',
  };

  const activeLinkStyle: React.CSSProperties = {
    ...linkStyle,
    color: '#fff',
    backgroundColor: '#334155',
  };

  const logoStyle: React.CSSProperties = {
    fontSize: '18px',
    fontWeight: 700,
    marginRight: 'auto',
    color: '#60a5fa',
  };

  const links = [
    { path: '/', label: 'Início' },
    { path: '/pessoas', label: 'Pessoas' },
    { path: '/transacoes', label: 'Transações' },
    { path: '/totais', label: 'Totais' },
  ];

  return (
    <nav style={navStyle}>
      <span style={logoStyle}>💰 Controle de Gastos</span>
      {links.map((link) => (
        <Link
          key={link.path}
          to={link.path}
          style={location.pathname === link.path ? activeLinkStyle : linkStyle}
        >
          {link.label}
        </Link>
      ))}
    </nav>
  );
};

export default Navbar;
