# Desafio Técnico – Controle de Gastos Residenciais

Este é um projeto full-stack desenvolvido para o desafio técnico de estágio. O sistema permite o gerenciamento de pessoas, o registro de transações financeiras (receitas e despesas) e a visualização de totais por pessoa.

## 📸 Capturas de Tela

> *(Adicione as imagens da sua aplicação aqui)*
> 
> ![Dashboard de Totais](./.github/totais.png)
> ![Cadastro de Pessoas](./.github/pessoas.png)

## 🛠 Tecnologias Utilizadas

- **Back-end:** .NET 8 (C#), ASP.NET Core Web API, Entity Framework Core (SQLite), xUnit, Moq, ILogger.
- **Front-end:** React 19, TypeScript, Vite, React Router, React Hook Form, Zod.
- **Infraestrutura:** Docker e Docker Compose.

## 📌 Funcionalidades e Regras de Negócio

1. **Cadastro de Pessoas:**
   - Permite registrar nome e idade.
   - Pessoas cadastradas não podem ser menores de 1 ano.

2. **Cadastro de Transações:**
   - Obrigatório vincular uma pessoa existente.
   - Valores devem ser positivos (maiores que zero).
   - *Regra de Negócio Crítica:* Menores de 18 anos só podem registrar **Despesas** (saídas).
   
3. **Consulta de Totais (Resumo Financeiro):**
   - Lista todas as pessoas cadastradas exibindo:
     - Total de receitas (individual).
     - Total de despesas (individual).
     - Saldo líquido (individual).
   - Exibe no topo os totais gerais somados (Todas as Receitas, Todas as Despesas, Saldo Global).

4. **Exclusão em Cascata:**
   - Ao deletar uma pessoa, todas as suas transações vinculadas são automaticamente removidas do banco de dados (Cascade Delete).

## 🚀 Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (versão 18+)
- [Docker](https://www.docker.com/) (Opcional, para rodar via container)

### Opção 1: Rodando com Docker (Recomendado)

A aplicação inteira pode ser executada com um único comando a partir da pasta `controle-gastos`:

```bash
cd controle-gastos
docker-compose up --build -d
```
- A API estará em: `http://localhost:5000`
- O Front-end estará em: `http://localhost:5173`

### Opção 2: Rodando Manualmente

#### 1. Configurando o Banco de Dados (Migrations)
Abra um terminal na pasta `controle-gastos/ControleGastos.Api` e gere a migração inicial e atualize o banco:

```bash
cd controle-gastos/ControleGastos.Api
dotnet ef migrations add InitialCreate
dotnet run
```
> O banco de dados `gastos.db` será criado automaticamente pelo comando `Migrate()` no `Program.cs`.

#### 2. Rodando o Front-end
Em outro terminal, na pasta `controle-gastos/frontend`:

```bash
cd controle-gastos/frontend
npm install
npm run dev
```

## 🧪 Testes Unitários

O projeto conta com uma suíte de testes unitários para a camada de negócios, validando as regras de idade e criação de transações.

Para rodar os testes:
```bash
cd controle-gastos/ControleGastos.Tests
dotnet test
```

## 🏗 Decisões de Arquitetura e Engenharia (Diferenciais)

Para garantir a qualidade de um projeto de nível **Sênior**, foram aplicadas as seguintes práticas avançadas:
- **Middleware Global de Exceções (`GlobalExceptionHandlerMiddleware`):** Substituição de blocos `try-catch` nos Controllers por um interceptador global. Isso padroniza as respostas de erro (400 BadRequest, 404 NotFound, 500 InternalServerError) de forma centralizada.
- **Entity Framework Migrations (`Migrate`):** Substituição do método simplório `EnsureCreated()` por `Migrate()`, permitindo evolução do esquema do banco de dados de forma profissional.
- **Testes Automatizados (xUnit + Moq):** Implementação de testes unitários mockando o repositório para garantir que menores de idade não consigam cadastrar receitas sob nenhuma circunstância.
- **Design System Customizado:** O frontend não utiliza frameworks de CSS prontos, mas sim uma folha de estilos (`index.css`) com variáveis globais, Loading States e validação robusta com Zod.
- **Dockerização:** A aplicação está pronta para deploy usando `Docker` e `Docker Compose`.
