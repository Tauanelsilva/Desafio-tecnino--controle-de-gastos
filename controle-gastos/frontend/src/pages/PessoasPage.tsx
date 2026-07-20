import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { api } from '../services/api';
import { Pessoa } from '../types';
import { Input } from '../components/common/Input';
import { toast } from 'react-toastify';

const pessoaSchema = z.object({
    nome: z.string().min(1, 'Nome é obrigatório').max(100),
    idade: z.number({ invalid_type_error: "Idade deve ser um número" }).min(1).max(150),
});

type PessoaForm = z.infer<typeof pessoaSchema>;

export function PessoasPage() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    
    const { register, handleSubmit, reset, formState: { errors } } = useForm<PessoaForm>({
        resolver: zodResolver(pessoaSchema)
    });

    useEffect(() => {
        carregarPessoas();
    }, []);

    const carregarPessoas = async () => {
        try {
            const response = await api.get('/pessoas');
            setPessoas(response.data);
        } catch (error) {
            toast.error('Erro ao carregar pessoas.');
            console.error('Erro ao carregar pessoas:', error);
        }
    };

    const onSubmit = async (data: PessoaForm) => {
        try {
            await api.post('/pessoas', data);
            toast.success('Pessoa cadastrada com sucesso!');
            reset();
            carregarPessoas();
        } catch (error) {
            toast.error('Erro ao cadastrar pessoa.');
            console.error('Erro ao cadastrar:', error);
        }
    };

    const deletarPessoa = async (id: number) => {
        if (!window.confirm('Tem certeza? Isso excluirá todas as transações desta pessoa.')) return;
        
        try {
            await api.delete(`/pessoas/${id}`);
            toast.success('Pessoa excluída com sucesso!');
            carregarPessoas();
        } catch (error) {
            toast.error('Erro ao excluir pessoa.');
            console.error('Erro ao excluir:', error);
        }
    };

    return (
        <div>
            <h1>Gerenciar Pessoas</h1>

            <div className="card">
                <h2 className="card-title">Nova Pessoa</h2>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <Input 
                        label="Nome Completo" 
                        register={register('nome')} 
                        error={errors.nome?.message} 
                    />
                    <Input 
                        label="Idade" 
                        type="number"
                        register={register('idade', { valueAsNumber: true })} 
                        error={errors.idade?.message} 
                    />
                    <button type="submit" className="btn btn-primary" style={{ marginTop: '1rem' }}>
                        Cadastrar Pessoa
                    </button>
                </form>
            </div>

            <div className="card">
                <h2 className="card-title">Pessoas Cadastradas</h2>
                {pessoas.length === 0 ? (
                    <p>Nenhuma pessoa cadastrada no sistema.</p>
                ) : (
                    <div className="table-container">
                        <table className="table">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Nome</th>
                                    <th>Idade</th>
                                    <th>Ações</th>
                                </tr>
                            </thead>
                            <tbody>
                                {pessoas.map(p => (
                                    <tr key={p.id}>
                                        <td>{p.id}</td>
                                        <td>{p.nome}</td>
                                        <td>{p.idade} anos</td>
                                        <td>
                                            <button 
                                                className="btn btn-danger" 
                                                onClick={() => deletarPessoa(p.id)}
                                            >
                                                Excluir
                                            </button>
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
