import { useEffect, useMemo, useState } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import type { Pessoa, Transacao } from '../types';
import { pessoasService } from '../services/pessoasService';
import { transacoesService } from '../services/transacoesService';
import { Input } from '../components/common/Input';
import { Select } from '../components/common/Select';
import { toast } from 'react-toastify';
import { AxiosError } from 'axios';

const transacaoSchema = z.object({
    descricao: z.string().min(1, 'Descrição é obrigatória').max(200),
    valor: z.number({ error: 'Valor deve ser um número' }).min(0.01, 'Valor deve ser maior que zero'),
    tipo: z.number().min(1, 'Selecione o tipo').max(2, 'Tipo inválido'),
    pessoaId: z.number({ error: 'Selecione uma pessoa' }).min(1, 'Selecione uma pessoa'),
});

type TransacaoForm = z.infer<typeof transacaoSchema>;

export function TransacoesPage() {
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);

    const { register, handleSubmit, reset, watch, setValue, formState: { errors } } = useForm<TransacaoForm>({
        resolver: zodResolver(transacaoSchema),
        defaultValues: { tipo: 1 },
    });

    const pessoaIdSelecionada = watch('pessoaId');
    const pessoaSelecionada = useMemo(
        () => pessoas.find(p => p.id === Number(pessoaIdSelecionada)),
        [pessoas, pessoaIdSelecionada]
    );
    const pessoaMenorDeIdade = pessoaSelecionada !== undefined && pessoaSelecionada.idade < 18;

    useEffect(() => {
        carregarDados();
    }, []);

    useEffect(() => {
        if (pessoaMenorDeIdade) {
            setValue('tipo', 2);
        }
    }, [pessoaMenorDeIdade, setValue]);

    const carregarDados = async () => {
        setIsLoading(true);
        try {
            const [listaTransacoes, listaPessoas] = await Promise.all([
                transacoesService.getAll(),
                pessoasService.getAll()
            ]);
            setTransacoes(listaTransacoes);
            setPessoas(listaPessoas);
        } catch (error) {
            console.error('Erro ao carregar dados:', error);
            toast.error('Erro ao carregar dados do servidor.');
        } finally {
            setIsLoading(false);
        }
    };

    const onSubmit = async (data: TransacaoForm) => {
        setIsSubmitting(true);
        try {
            await transacoesService.create(data);
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
        } finally {
            setIsSubmitting(false);
        }
    };

    const formatCurrency = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(value);
    };

    const tipoOptions = pessoaMenorDeIdade
        ? [{ label: 'Despesa', value: 2 }]
        : [
            { label: 'Receita', value: 1 },
            { label: 'Despesa', value: 2 }
        ];

    return (
        <div>
            <h1>Gerenciar Transações</h1>

            <div className="card">
                <h2 className="card-title">Nova Transação</h2>
                {isLoading ? (
                    <p>Carregando pessoas...</p>
                ) : pessoas.length === 0 ? (
                    <div style={{ padding: '1rem', backgroundColor: '#fef3c7', color: '#92400e', borderRadius: '8px' }}>
                        Cadastre uma pessoa primeiro para poder registrar transações.
                    </div>
                ) : (
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <Input 
                            label="Descrição" 
                            register={register('descricao')} 
                            error={errors.descricao?.message} 
                            disabled={isSubmitting}
                        />
                        <Input 
                            label="Valor (R$)" 
                            type="number"
                            step="0.01"
                            register={register('valor', { valueAsNumber: true })} 
                            error={errors.valor?.message} 
                            disabled={isSubmitting}
                        />
                        <Select 
                            label="Pessoa"
                            register={register('pessoaId', { valueAsNumber: true })}
                            error={errors.pessoaId?.message}
                            disabled={isSubmitting}
                            options={pessoas.map(p => ({
                                label: `${p.nome} (${p.idade} anos)`,
                                value: p.id
                            }))}
                        />
                        <Select 
                            label="Tipo de Transação"
                            register={register('tipo', { valueAsNumber: true })}
                            error={errors.tipo?.message}
                            disabled={isSubmitting || pessoaMenorDeIdade}
                            options={tipoOptions}
                        />
                        {pessoaMenorDeIdade && (
                            <p style={{ fontSize: '0.875rem', color: '#92400e', marginTop: '-0.5rem' }}>
                                Menores de 18 anos só podem registrar despesas.
                            </p>
                        )}
                        <button type="submit" className="btn btn-primary" style={{ marginTop: '1rem' }} disabled={isSubmitting}>
                            {isSubmitting ? 'Registrando...' : 'Registrar Transação'}
                        </button>
                    </form>
                )}
            </div>

            <div className="card">
                <h2 className="card-title">Histórico de Transações</h2>
                {isLoading ? (
                    <p>Carregando transações...</p>
                ) : transacoes.length === 0 ? (
                    <p>Nenhuma transação registrada.</p>
                ) : (
                    <div className="table-container">
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Pessoa</th>
                                    <th>Descrição</th>
                                    <th>Valor</th>
                                    <th>Tipo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {transacoes.map(t => (
                                    <tr key={t.id}>
                                        <td>{t.id}</td>
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
