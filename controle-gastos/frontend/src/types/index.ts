export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

export interface CreatePessoaDto {
  nome: string;
  idade: number;
}

export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: string;
  pessoaId: number;
  pessoaNome: string;
}

export interface CreateTransacaoDto {
  descricao: string;
  valor: number;
  tipo: number;
  pessoaId: number;
}

export interface Totais {
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}
