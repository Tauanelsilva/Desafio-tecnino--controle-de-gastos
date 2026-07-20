import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { api } from '../services/api';
import { Pessoa, Transacao } from '../types';
import { Input } from '../components/common/Input';
import { Select } from '../components/common/Select';
import { toast } from 'react-toastify';
import { AxiosError } from 'axios';

const transacaoSchema = z.object({
    descricao: z.string().min(1, 'Descrição é obrigatória').max(200),
    valor: z.number({ invalid_type_error: "Valor deve ser um número" }).min(0.01, "Valor deve ser maior que zero"),
    tipo: z.number().min(1).max(2),
    pessoaId: z.number().min(1, 'Selecione uma pessoa')
});

type TransacaoForm = z.infer<typeof transacaoSchema>;

export function TransacoesPage() {
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    const { register, handleSubmit, reset, formState: { errors } } = useForm<TransacaoForm>({
        resolver: zodResolver(transacaoSchema)
    });

    useEffect(() => {
        carregarDados();
    }, []);

    const carregarDados = async () => {
        try {
            const [resTransacoes, resPessoas] = await Promise.all([
                api.get('/transacoes'),
                api.get('/pessoas')
            ]);
            setTransacoes(resTransacoes.data);
            setPessoas(resPessoas.data);
        } catch (error) {
            console.error('Erro ao carregar dados:', error);
            toast.error('Erro ao carregar dados do servidor.');
        }
    };

    const onSubmit = async (data: TransacaoForm) => {
        try {
            await api.post('/transacoes', data);
            toast.success('Transação registrada com sucesso!');
            reset();
            carregarDados();
        } catch (error) {
            if (error instanceof AxiosError && error.response?.data?.message) {
                toast.error(error.response.data.message);
            } else {
                toast.error('Erro ao registrar transação.');
            }
            console.error('Erro ao salvar transação:', error);
        }
    };

    const formatCurrency = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(value);
    };

    return (
        <div>
            <h1>Gerenciar Transações</h1>

            <div className="card">
                <h2 className="card-title">Nova Transação</h2>
                {pessoas.length === 0 ? (
                    <div style={{ padding: '1rem', backgroundColor: '#fef3c7', color: '#92400e', borderRadius: '8px' }}>
                        Cadastre uma pessoa primeiro para poder registrar transações.
                    </div>
                ) : (
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <Input 
                            label="Descrição" 
                            register={register('descricao')} 
                            error={errors.descricao?.message} 
                        />
                        <Input 
                            label="Valor (R$)" 
                            type="number"
                            step="0.01"
                            register={register('valor', { valueAsNumber: true })} 
                            error={errors.valor?.message} 
                        />
                        <Select 
                            label="Tipo de Transação"
                            register={register('tipo', { valueAsNumber: true })}
                            error={errors.tipo?.message}
                            options={[
                                { label: 'Receita', value: 1 },
                                { label: 'Despesa', value: 2 }
                            ]}
                        />
                        <Select 
                            label="Pessoa"
                            register={register('pessoaId', { valueAsNumber: true })}
                            error={errors.pessoaId?.message}
                            options={pessoas.map(p => ({ label: p.nome, value: p.id }))}
                        />
                        <button type="submit" className="btn btn-primary" style={{ marginTop: '1rem' }}>
                            Registrar Transação
                        </button>
                    </form>
                )}
            </div>

            <div className="card">
                <h2 className="card-title">Histórico de Transações</h2>
                {transacoes.length === 0 ? (
                    <p>Nenhuma transação registrada.</p>
                ) : (
                    <div className="table-container">
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>Pessoa</th>
                                    <th>Descrição</th>
                                    <th>Valor</th>
                                    <th>Tipo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {transacoes.map(t => (
                                    <tr key={t.id}>
                                        <td>{t.pessoaNome}</td>
                                        <td>{t.descricao}</td>
                                        <td className={t.tipo === 'Receita' ? 'positive' : 'negative'} style={{ fontWeight: 500 }}>
                                            {formatCurrency(t.valor)}
                                        </td>
                                        <td>
                                            <span className={`badge ${t.tipo === 'Receita' ? 'badge-receita' : 'badge-despesa'}`}>
                                                {t.tipo}
                                            </span>
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
