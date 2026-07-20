import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { toast } from 'react-toastify';
import { pessoasService } from '../services/pessoasService';
import { Pessoa, CreatePessoaDto } from '../types';
import Input from '../components/common/Input';
import Button from '../components/common/Button';

const pessoaSchema = z.object({
  nome: z.string().min(1, 'O nome é obrigatório').max(100, 'O nome deve ter no máximo 100 caracteres'),
  idade: z.number().min(1, 'A idade deve ser no mínimo 1').max(150, 'A idade deve ser no máximo 150'),
});

type PessoaForm = z.infer<typeof pessoaSchema>;

const PessoasPage: React.FC = () => {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<PessoaForm>({
    resolver: zodResolver(pessoaSchema),
  });

  const fetchData = async () => {
    try {
      const data = await pessoasService.getAll();
      setPessoas(data);
    } catch (error) {
      toast.error('Erro ao carregar pessoas.');
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const onSubmit = async (data: PessoaForm) => {
    try {
      const dto: CreatePessoaDto = {
        nome: data.nome,
        idade: data.idade,
      };
      await pessoasService.create(dto);
      toast.success('Pessoa cadastrada com sucesso!');
      reset();
      fetchData();
    } catch (error: any) {
      toast.error(error.response?.data?.message || 'Erro ao cadastrar pessoa.');
    }
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm('Tem certeza que deseja excluir esta pessoa? Todas as suas transações serão removidas.')) {
      return;
    }
    try {
      await pessoasService.delete(id);
      toast.success('Pessoa excluída com sucesso!');
      fetchData();
    } catch (error: any) {
      toast.error(error.response?.data?.message || 'Erro ao excluir pessoa.');
    }
  };

  const containerStyle: React.CSSProperties = {
    maxWidth: '800px',
    margin: '40px auto',
    padding: '0 20px',
  };

  const cardStyle: React.CSSProperties = {
    backgroundColor: '#fff',
    borderRadius: '12px',
    padding: '24px',
    boxShadow: '0 1px 3px rgba(0,0,0,0.1)',
    marginBottom: '24px',
  };

  const tableStyle: React.CSSProperties = {
    width: '100%',
    borderCollapse: 'collapse',
    marginTop: '16px',
  };

  const thStyle: React.CSSProperties = {
    textAlign: 'left',
    padding: '12px',
    borderBottom: '2px solid #e5e7eb',
    fontSize: '14px',
    fontWeight: 600,
    color: '#374151',
  };

  const tdStyle: React.CSSProperties = {
    padding: '12px',
    borderBottom: '1px solid #f3f4f6',
    fontSize: '14px',
    color: '#4b5563',
  };

  return (
    <div style={containerStyle}>
      <h1 style={{ fontSize: '24px', fontWeight: 700, marginBottom: '24px', color: '#1e293b' }}>
        Cadastro de Pessoas
      </h1>

      {/* Formulário */}
      <div style={cardStyle}>
        <h2 style={{ fontSize: '18px', fontWeight: 600, marginBottom: '16px', color: '#374151' }}>
          Nova Pessoa
        </h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Input
            label="Nome"
            name="nome"
            register={register}
            error={errors.nome?.message}
            placeholder="Digite o nome completo"
          />
          <Input
            label="Idade"
            name="idade"
            type="number"
            register={register}
            error={errors.idade?.message}
            placeholder="Digite a idade"
          />
          <Button type="submit">Cadastrar Pessoa</Button>
        </form>
      </div>

      {/* Listagem */}
      <div style={cardStyle}>
        <h2 style={{ fontSize: '18px', fontWeight: 600, marginBottom: '16px', color: '#374151' }}>
          Pessoas Cadastradas
        </h2>
        {pessoas.length === 0 ? (
          <p style={{ color: '#9ca3af', fontSize: '14px' }}>Nenhuma pessoa cadastrada ainda.</p>
        ) : (
          <table style={tableStyle}>
            <thead>
              <tr>
                <th style={thStyle}>ID</th>
                <th style={thStyle}>Nome</th>
                <th style={thStyle}>Idade</th>
                <th style={thStyle}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map((pessoa) => (
                <tr key={pessoa.id}>
                  <td style={tdStyle}>{pessoa.id}</td>
                  <td style={tdStyle}>{pessoa.nome}</td>
                  <td style={tdStyle}>{pessoa.idade} anos</td>
                  <td style={tdStyle}>
                    <Button
                      variant="danger"
                      onClick={() => handleDelete(pessoa.id)}
                    >
                      Excluir
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default PessoasPage;
