import React from 'react';
import { Link } from 'react-router-dom';

const HomePage: React.FC = () => {
  const containerStyle: React.CSSProperties = {
    maxWidth: '800px',
    margin: '40px auto',
    padding: '0 20px',
    textAlign: 'center',
  };

  const titleStyle: React.CSSProperties = {
    fontSize: '32px',
    fontWeight: 700,
    color: '#1e293b',
    marginBottom: '16px',
  };

  const subtitleStyle: React.CSSProperties = {
    fontSize: '16px',
    color: '#64748b',
    marginBottom: '40px',
    lineHeight: 1.6,
  };

  const cardsContainerStyle: React.CSSProperties = {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
    gap: '20px',
    marginTop: '20px',
  };

  const cardStyle: React.CSSProperties = {
    padding: '24px',
    borderRadius: '12px',
    backgroundColor: '#fff',
    boxShadow: '0 1px 3px rgba(0,0,0,0.1)',
    textDecoration: 'none',
    color: 'inherit',
    transition: 'transform 0.2s, box-shadow 0.2s',
  };

  const cardTitleStyle: React.CSSProperties = {
    fontSize: '18px',
    fontWeight: 600,
    marginBottom: '8px',
    color: '#1e293b',
  };

  const cardDescStyle: React.CSSProperties = {
    fontSize: '14px',
    color: '#64748b',
  };

  return (
    <div style={containerStyle}>
      <h1 style={titleStyle}>Controle de Gastos Residenciais</h1>
      <p style={subtitleStyle}>
        Gerencie as finanças da sua residência de forma simples e eficiente.
        Cadastre pessoas, registre transações e acompanhe seus totais.
      </p>

      <div style={cardsContainerStyle}>
        <Link to="/pessoas" style={cardStyle}>
          <div style={cardTitleStyle}>👥 Pessoas</div>
          <div style={cardDescStyle}>Cadastre e gerencie as pessoas da residência.</div>
        </Link>

        <Link to="/transacoes" style={cardStyle}>
          <div style={cardTitleStyle}>💸 Transações</div>
          <div style={cardDescStyle}>Registre receitas e despesas.</div>
        </Link>

        <Link to="/totais" style={cardStyle}>
          <div style={cardTitleStyle}>📊 Totais</div>
          <div style={cardDescStyle}>Veja o resumo financeiro geral.</div>
        </Link>
      </div>
    </div>
  );
};

export default HomePage;
