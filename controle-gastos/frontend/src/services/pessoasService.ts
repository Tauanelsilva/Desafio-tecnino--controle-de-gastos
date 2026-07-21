import api from './api';
import type { Pessoa, CreatePessoaDto } from '../types';

export const pessoasService = {
  async getAll(): Promise<Pessoa[]> {
    const response = await api.get<Pessoa[]>('/pessoas');
    return response.data;
  },

  async create(dto: CreatePessoaDto): Promise<Pessoa> {
    const response = await api.post<Pessoa>('/pessoas', dto);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await api.delete(`/pessoas/${id}`);
  },
};
