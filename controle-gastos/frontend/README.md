# Front-end — Controle de Gastos

Interface web (SPA) do sistema de controle de gastos residenciais, construída com React 19, TypeScript e Vite.

## Estrutura

```
src/
├── components/common/   # Componentes reutilizáveis (Input, Select)
├── pages/               # Páginas (Home, Pessoas, Transações, Totais)
├── services/            # Camada de acesso à API (axios)
├── types/               # Tipos/contratos compartilhados
├── App.tsx              # Rotas e navegação
└── index.css            # Design system (variáveis CSS + componentes)
```

## Scripts

| Comando | Descrição |
|---------|-----------|
| `npm run dev` | Servidor de desenvolvimento (http://localhost:5173) |
| `npm run build` | Build de produção com checagem de tipos |
| `npm run lint` | Análise estática com Oxlint |
| `npm run preview` | Pré-visualiza o build de produção |

## Configuração

A URL da API pode ser definida por variável de ambiente. Crie um arquivo `.env`:

```
VITE_API_URL=http://localhost:5000/api
```

Se não for definida, o padrão é `http://localhost:5000/api`.

## Validação

Os formulários usam **React Hook Form** com **Zod** para validação em tempo real. A regra de negócio de menores de idade (apenas despesas) é refletida na interface, mas também é validada no back-end.
