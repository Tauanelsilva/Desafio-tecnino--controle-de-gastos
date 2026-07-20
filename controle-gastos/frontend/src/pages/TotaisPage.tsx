import { useEffect, useState } from 'react';
import { api } from '../services/api';
import { Totais } from '../types';
import { toast } from 'react-toastify';

export function TotaisPage() {
    const [totais, setTotais] = useState<Totais | null>(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        carregarTotais();
    }, []);

    const carregarTotais = async () => {
        try {
            const response = await api.get('/transacoes/totais');
            setTotais(response.data);
        } catch (error) {
            console.error('Erro ao carregar totais:', error);
            toast.error('Erro ao carregar os totais financeiros.');
        } finally {
            setIsLoading(false);
        }
    };

    if (isLoading) return <div className="card">Carregando totais...</div>;
    if (!totais) return <div className="card">Falha ao carregar totais.</div>;

    const formatCurrency = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(value);
    };

    return (
        <div>
            <h1>Resumo Financeiro</h1>

            <div className="summary-grid">
                <div className="summary-card">
                    <div className="summary-title">Receitas (Geral)</div>
                    <div className="summary-value positive">{formatCurrency(totais.totalReceitasGeral)}</div>
                </div>
                <div className="summary-card">
                    <div className="summary-title">Despesas (Geral)</div>
                    <div className="summary-value negative">{formatCurrency(totais.totalDespesasGeral)}</div>
                </div>
                <div className="summary-card">
                    <div className="summary-title">Saldo Líquido (Geral)</div>
                    <div className={`summary-value ${totais.saldoGeral >= 0 ? 'positive' : 'negative'}`}>
                        {formatCurrency(totais.saldoGeral)}
                    </div>
                </div>
            </div>

            <div className="card">
                <h2 className="card-title">Totais por Pessoa</h2>
                {totais.pessoas.length === 0 ? (
                    <p>Nenhuma pessoa cadastrada.</p>
                ) : (
                    <div className="table-container">
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>Pessoa</th>
                                    <th>Idade</th>
                                    <th>Total de Receitas</th>
                                    <th>Total de Despesas</th>
                                    <th>Saldo Líquido</th>
                                </tr>
                            </thead>
                            <tbody>
                                {totais.pessoas.map((pessoa) => (
                                    <tr key={pessoa.pessoaId}>
                                        <td>{pessoa.nome}</td>
                                        <td>{pessoa.idade} anos</td>
                                        <td className="positive">{formatCurrency(pessoa.totalReceitas)}</td>
                                        <td className="negative">{formatCurrency(pessoa.totalDespesas)}</td>
                                        <td className={pessoa.saldo >= 0 ? 'positive' : 'negative'} style={{ fontWeight: 600 }}>
                                            {formatCurrency(pessoa.saldo)}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
}
