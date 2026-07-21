import api from './api';
import type { Transacao, CreateTransacaoDto, Totais } from '../types';

export const transacoesService = {
  async getAll(): Promise<Transacao[]> {
    const response = await api.get<Transacao[]>('/transacoes');
    return response.data;
  },

  async create(dto: CreateTransacaoDto): Promise<Transacao> {
    const response = await api.post<Transacao>('/transacoes', dto);
    return response.data;
  },

  async getTotais(): Promise<Totais> {
    const response = await api.get<Totais>('/transacoes/totais');
    return response.data;
  },
};
