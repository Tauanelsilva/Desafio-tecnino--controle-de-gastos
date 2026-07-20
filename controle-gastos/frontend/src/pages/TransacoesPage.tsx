import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { toast } from 'react-toastify';
import { transacoesService } from '../services/transacoesService';
import { pessoasService } from '../services/pessoasService';
import { Transacao, CreateTransacaoDto, Pessoa } from '../types';
import Input from '../components/common/Input';
import Select from '../components/common/Select';
import Button from '../components/common/Button';

const transacaoSchema = z.object({
  descricao: z.string().min(1, 'A descrição é obrigatória').max(200, 'Máximo 200 caracteres'),
  valor: z.number().min(0.01, 'O valor deve ser maior que zero'),
  tipo: z.number().min(1, 'Selecione um tipo'),
  pessoaId: z.number().min(1, 'Selecione uma pessoa'),
});

type TransacaoForm = z.infer<typeof transacaoSchema>;

const TransacoesPage: React.FC = () => {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<TransacaoForm>({
    resolver: zodResolver(transacaoSchema),
  });

  const fetchData = async () => {
    try {
      const [transacoesData, pessoasData] = await Promise.all([
        transacoesService.getAll(),
        pessoasService.getAll(),
      ]);
      setTransacoes(transacoesData);
      setPessoas(pessoasData);
    } catch (error) {
      toast.error('Erro ao carregar dados.');
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const onSubmit = async (data: TransacaoForm) => {
    try {
      const dto: CreateTransacaoDto = {
        descricao: data.descricao,
        valor: data.valor,
        tipo: data.tipo,
        pessoaId: data.pessoaId,
      };
      await transacoesService.create(dto);
      toast.success('Transação cadastrada com sucesso!');
      reset();
      fetchData();
    } catch (error: any) {
      toast.error(error.response?.data?.message || 'Erro ao cadastrar transação.');
    }
  };

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  const containerStyle: React.CSSProperties = {
    maxWidth: '1000px',
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

  const tipoBadgeStyle = (tipo: string): React.CSSProperties => ({
    display: 'inline-block',
    padding: '4px 10px',
    borderRadius: '12px',
    fontSize: '12px',
    fontWeight: 600,
    backgroundColor: tipo === 'Receita' ? '#dcfce7' : '#fee2e2',
    color: tipo === 'Receita' ? '#166534' : '#991b1b',
  });

  const pessoaOptions = pessoas.map((p) => ({
    value: p.id,
    label: `${p.nome} (${p.idade} anos)`,
  }));

  const tipoOptions = [
    { value: 1, label: 'Receita' },
    { value: 2, label: 'Despesa' },
  ];

  return (
    <div style={containerStyle}>
      <h1 style={{ fontSize: '24px', fontWeight: 700, marginBottom: '24px', color: '#1e293b' }}>
        Cadastro de Transações
      </h1>

      {/* Formulário */}
      <div style={cardStyle}>
        <h2 style={{ fontSize: '18px', fontWeight: 600, marginBottom: '16px', color: '#374151' }}>
          Nova Transação
        </h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <Select
            label="Pessoa"
            name="pessoaId"
            options={pessoaOptions}
            register={register}
            error={errors.pessoaId?.message}
            placeholder="Selecione uma pessoa"
          />
          <Input
            label="Descrição"
            name="descricao"
            register={register}
            error={errors.descricao?.message}
            placeholder="Ex: Compra de mercado"
          />
          <Input
            label="Valor (R$)"
            name="valor"
            type="number"
            register={register}
            error={errors.valor?.message}
            placeholder="0.00"
          />
          <Select
            label="Tipo"
            name="tipo"
            options={tipoOptions}
            register={register}
            error={errors.tipo?.message}
            placeholder="Selecione o tipo"
          />
          <Button type="submit">Cadastrar Transação</Button>
        </form>
      </div>

      {/* Listagem */}
      <div style={cardStyle}>
        <h2 style={{ fontSize: '18px', fontWeight: 600, marginBottom: '16px', color: '#374151' }}>
          Transações Registradas
        </h2>
        {transacoes.length === 0 ? (
          <p style={{ color: '#9ca3af', fontSize: '14px' }}>Nenhuma transação registrada ainda.</p>
        ) : (
          <table style={tableStyle}>
            <thead>
              <tr>
                <th style={thStyle}>ID</th>
                <th style={thStyle}>Descrição</th>
                <th style={thStyle}>Valor</th>
                <th style={thStyle}>Tipo</th>
                <th style={thStyle}>Pessoa</th>
              </tr>
            </thead>
            <tbody>
              {transacoes.map((transacao) => (
                <tr key={transacao.id}>
                  <td style={tdStyle}>{transacao.id}</td>
                  <td style={tdStyle}>{transacao.descricao}</td>
                  <td style={tdStyle}>{formatCurrency(transacao.valor)}</td>
                  <td style={tdStyle}>
                    <span style={tipoBadgeStyle(transacao.tipo)}>
                      {transacao.tipo}
                    </span>
                  </td>
                  <td style={tdStyle}>{transacao.pessoaNome}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default TransacoesPage;
