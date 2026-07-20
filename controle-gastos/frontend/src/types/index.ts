export interface Pessoa {
    id: number;
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

export interface TotaisPorPessoa {
    pessoaId: number;
    nome: string;
    idade: number;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface Totais {
    pessoas: TotaisPorPessoa[];
    totalReceitasGeral: number;
    totalDespesasGeral: number;
    saldoGeral: number;
}
