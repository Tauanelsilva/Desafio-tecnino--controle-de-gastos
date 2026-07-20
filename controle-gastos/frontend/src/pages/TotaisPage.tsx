import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import { transacoesService } from '../services/transacoesService';
import { Totais } from '../types';

const TotaisPage: React.FC = () => {
  const [totais, setTotais] = useState<Totais | null>(null);

  const fetchTotais = async () => {
    try {
      const data = await transacoesService.getTotais();
      setTotais(data);
    } catch (error) {
      toast.error('Erro ao carregar totais.');
    }
  };

  useEffect(() => {
    fetchTotais();
  }, []);

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  const containerStyle: React.CSSProperties = {
    maxWidth: '800px',
    margin: '40px auto',
    padding: '0 20px',
  };

  const cardsStyle: React.CSSProperties = {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
    gap: '20px',
    marginTop: '20px',
  };

  const cardBaseStyle: React.CSSProperties = {
    backgroundColor: '#fff',
    borderRadius: '12px',
    padding: '24px',
    boxShadow: '0 1px 3px rgba(0,0,0,0.1)',
    textAlign: 'center',
  };

  const getCardColor = (type: 'receita' | 'despesa' | 'saldo') => {
    const colors = {
      receita: { bg: '#dcfce7', text: '#166534', border: '#86efac' },
      despesa: { bg: '#fee2e2', text: '#991b1b', border: '#fca5a5' },
      saldo: { bg: '#dbeafe', text: '#1e40af', border: '#93c5fd' },
    };
    return colors[type];
  };

  if (!totais) {
    return (
      <div style={containerStyle}>
        <h1 style={{ fontSize: '24px', fontWeight: 700, marginBottom: '24px', color: '#1e293b' }}>
          Totais Financeiros
        </h1>
        <p style={{ color: '#9ca3af' }}>Carregando...</p>
      </div>
    );
  }

  return (
    <div style={containerStyle}>
      <h1 style={{ fontSize: '24px', fontWeight: 700, marginBottom: '8px', color: '#1e293b' }}>
        Totais Financeiros
      </h1>
      <p style={{ color: '#64748b', marginBottom: '24px' }}>
        Resumo geral de receitas e despesas.
      </p>

      <div style={cardsStyle}>
        {/* Total Receitas */}
        <div style={{ ...cardBaseStyle, border: `2px solid ${getCardColor('receita').border}` }}>
          <div style={{ fontSize: '14px', fontWeight: 600, color: getCardColor('receita').text, marginBottom: '8px' }}>
            Total Receitas
          </div>
          <div style={{ fontSize: '28px', fontWeight: 700, color: getCardColor('receita').text }}>
            {formatCurrency(totais.totalReceitas)}
          </div>
        </div>

        {/* Total Despesas */}
        <div style={{ ...cardBaseStyle, border: `2px solid ${getCardColor('despesa').border}` }}>
          <div style={{ fontSize: '14px', fontWeight: 600, color: getCardColor('despesa').text, marginBottom: '8px' }}>
            Total Despesas
          </div>
          <div style={{ fontSize: '28px', fontWeight: 700, color: getCardColor('despesa').text }}>
            {formatCurrency(totais.totalDespesas)}
          </div>
        </div>

        {/* Saldo */}
        <div style={{ ...cardBaseStyle, border: `2px solid ${getCardColor('saldo').border}` }}>
          <div style={{ fontSize: '14px', fontWeight: 600, color: getCardColor('saldo').text, marginBottom: '8px' }}>
            Saldo
          </div>
          <div style={{
            fontSize: '28px',
            fontWeight: 700,
            color: totais.saldo >= 0 ? getCardColor('receita').text : getCardColor('despesa').text,
          }}>
            {formatCurrency(totais.saldo)}
          </div>
        </div>
      </div>
    </div>
  );
};

export default TotaisPage;
