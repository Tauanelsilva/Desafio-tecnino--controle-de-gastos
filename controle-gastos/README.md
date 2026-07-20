# Desafio Técnico – Controle de Gastos Residenciais

Este é um projeto full-stack desenvolvido para o desafio técnico de estágio. O sistema permite o gerenciamento de pessoas, o registro de transações financeiras (receitas e despesas) e a visualização de totais por pessoa.

## 🛠 Tecnologias Utilizadas

- **Back-end:** .NET 8 (C#), ASP.NET Core Web API, Entity Framework Core (SQLite).
- **Front-end:** React 19, TypeScript, Vite, React Router, React Hook Form, Zod.
- **Banco de Dados:** SQLite (persistência local no arquivo `gastos.db`).

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
- [.NET 8 SDK](https://dotnet.microsoft.com/download) instalado.
- [Node.js](https://nodejs.org/) (versão 18+ recomendada) instalado.

### 1. Rodando o Back-end (API)

Abra um terminal na pasta `ControleGastos.Api` e execute:

```bash
cd ControleGastos.Api
dotnet run
```
> A API estará disponível em: `http://localhost:5000`
> O banco de dados SQLite (`gastos.db`) será criado automaticamente na primeira execução através do `EnsureCreated()`.
> O Swagger (Documentação da API) está disponível em `http://localhost:5000/swagger`

### 2. Rodando o Front-end

Abra um *outro* terminal na pasta `frontend` e execute:

```bash
cd frontend
npm install
npm run dev
```
> O front-end estará disponível em: `http://localhost:5173`

## 🏗 Decisões de Arquitetura e Melhorias

Para garantir a qualidade de um projeto de nível profissional, foram aplicadas as seguintes práticas:
- **Design System Customizado:** O frontend não utiliza frameworks de CSS prontos, mas sim uma folha de estilos (`index.css`) estruturada com variáveis globais (CSS Variables), garantindo manutenibilidade e padrão visual (UI Clean).
- **Tipagem Estrita e Validação:** Uso do `zod` no React Hook Form e `DataAnnotations` na API para garantir que dados incorretos nunca cheguem ao banco.
- **Tratamento Global de Exceções:** Criação de classes como `BusinessRuleException` e `NotFoundException` no backend, limpando os Controllers e centralizando regras de negócio na camada *Service*.
- **Documentação de Código (XML Docs):** Todo o backend (.cs) foi devidamente comentado utilizando as tags summary do C#, facilitando a manutenção e a geração de documentação no Swagger.
