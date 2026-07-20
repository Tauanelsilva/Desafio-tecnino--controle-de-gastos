# Controle de Gastos Residenciais

Sistema de controle de gastos residenciais desenvolvido como teste técnico. Permite o cadastro de pessoas, registro de receitas e despesas, e consulta de totais financeiros.

---

## Tecnologias Utilizadas

| Área       | Tecnologia                          |
|------------|-------------------------------------|
| Backend    | .NET 8 / ASP.NET Core Web API       |
| ORM        | Entity Framework Core 8             |
| Banco      | SQLite                              |
| Frontend   | React 18 + TypeScript               |
| Build      | Vite                                |
| Formulários| React Hook Form + Zod               |
| HTTP       | Axios                               |
| Rotas      | React Router DOM                    |
| Notificações| React Toastify                     |
| Documentação API | Swagger (Swashbuckle)         |

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- [npm](https://www.npmjs.com/) ou [pnpm](https://pnpm.io/)

---

## Como Rodar a Aplicação

### 1. Backend (.NET 8 Web API)

```bash
cd ControleGastos.Api
dotnet restore
dotnet run
```

A API será iniciada em `http://localhost:5000`.

Para acessar o Swagger, abra: `http://localhost:5000/swagger`

O banco de dados SQLite (`gastos.db`) será criado automaticamente na pasta do projeto ao iniciar.

### 2. Frontend (React + TypeScript)

```bash
cd frontend
npm install
npm run dev
```

O frontend será iniciado em `http://localhost:5173`.

---

## Endpoints da API

### Pessoas (`/api/pessoas`)

| Método | Endpoint              | Descrição                                      |
|--------|-----------------------|------------------------------------------------|
| POST   | `/api/pessoas`        | Cadastra uma nova pessoa (nome e idade).       |
| GET    | `/api/pessoas`        | Retorna todas as pessoas cadastradas.          |
| DELETE | `/api/pessoas/{id}`   | Exclui uma pessoa e todas as suas transações.  |

### Transações (`/api/transacoes`)

| Método | Endpoint               | Descrição                                      |
|--------|------------------------|------------------------------------------------|
| POST   | `/api/transacoes`      | Cadastra uma nova transação vinculada a uma pessoa. |
| GET    | `/api/transacoes`      | Retorna todas as transações com nome da pessoa. |
| GET    | `/api/transacoes/totais` | Retorna totais de receitas, despesas e saldo.  |

---

## Regras de Negócio

1. **Pessoa obrigatória:** A pessoa referenciada deve existir no banco antes de cadastrar uma transação.
2. **Restrição de idade:** Menores de 18 anos só podem cadastrar despesas.
3. **Exclusão em cascata:** Ao excluir uma pessoa, todas as suas transações são removidas automaticamente.
4. **IDs automáticos:** Os IDs são gerados automaticamente pelo banco de dados.
5. **Persistência:** Os dados são salvos em arquivo SQLite (`gastos.db`) e permanecem após fechar a aplicação.

---

## Estrutura do Projeto

```
controle-gastos/
├── README.md
├── ControleGastos.Api/          # Backend .NET 8
│   ├── Controllers/
│   ├── DTOs/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── Data/
│   └── Program.cs
└── frontend/                    # Frontend React + TS
    ├── src/
    │   ├── components/
    │   ├── pages/
    │   ├── services/
    │   ├── types/
    │   └── App.tsx
    └── package.json
```

---

## Arquitetura

A aplicação segue o padrão de camadas:

```
Controller → Service → Repository → Database
```

- **Controllers** recebem as requisições HTTP e delegam para os Services.
- **Services** contêm as regras de negócio e validações.
- **Repositories** encapsulam o acesso ao banco de dados (EF Core).
- **Database** é o SQLite gerenciado pelo Entity Framework Core.

---

## Licença

Este projeto foi desenvolvido para fins educacionais e de avaliação técnica.
