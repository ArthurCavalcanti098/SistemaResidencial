# Sistema de Controle de Gastos Residenciais

Sistema local cliente-servidor para controle de gastos residenciais. Permite cadastrar pessoas da residência, registrar transações financeiras (receitas e despesas) vinculadas a cada pessoa e consultar totais consolidados por pessoa e gerais.

## Pré-requisitos

- .NET SDK 8+ (recomendado: 10)
- Node.js 18+
- PostgreSQL 16 (local ou Docker/Podman)

## Como executar

### 1. PostgreSQL

```bash
# Com Docker:
docker compose up -d

# Ou com Podman:
podman run -d --name controle_gastos_pg \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=controle_gastos \
  -p 5432:5432 \
  -v pgdata:/var/lib/postgresql/data \
  docker.io/library/postgres:16-alpine
```

### 2. Back-end (.NET API)

```bash
cd backend
dotnet run --project ControleGastos.Api
```

A API inicia em **http://localhost:5000**

Swagger disponível em **http://localhost:5000/swagger**

### 3. Front-end (React)

```bash
cd frontend
npm install
npm run dev
```

O front-end inicia em **http://localhost:5173**

## URLs

| Serviço | URL |
|---------|-----|
| Front-end | http://localhost:5173 |
| API | http://localhost:5000 |
| Swagger | http://localhost:5000/swagger |
| PostgreSQL | localhost:5432 |

## Funcionalidades

### Pessoas
- Criar pessoa (nome, idade)
- Listar pessoas ordenadas por nome
- Excluir pessoa (remove todas as transações vinculadas via cascade)

### Transações
- Criar transação (descrição, valor, tipo, pessoa)
- Listar transações ordenadas por descrição, incluindo nome da pessoa
- **Menores de 18 anos** só podem registrar despesas (regra RN-03)

### Totais
- Totais por pessoa: receitas, despesas, saldo
- Totais gerais consolidados
- Pessoas sem transações aparecem com valores zerados

## Persistência

Todos os dados são armazenados no PostgreSQL (database `controle_gastos`) e sobrevivem ao fechamento da aplicação.

## Estrutura do projeto

```
SistemaResidencial/
├── docker-compose.yml
├── docs/SDD.md
├── backend/
│   ├── ControleGastos.sln
│   └── ControleGastos.Api/
│       ├── Controllers/      (Pessoas, Transações, Totais)
│       ├── Services/         (regras de negócio RN-02 a RN-07)
│       ├── Validators/       (FluentValidation)
│       ├── Data/             (EF Core, Migrations)
│       ├── Models/           (Pessoa, Transacao, TipoTransacao)
│       ├── DTOs/
│       └── Exceptions/
├── frontend/
│   └── src/
│       ├── api/              (Axios client)
│       ├── components/       (Layout, ConfirmModal, FormMessage)
│       ├── hooks/            (usePessoas, useTransacoes)
│       ├── pages/            (Pessoas, Transações, Totais)
│       └── types/
└── README.md
```

## Regras de negócio implementadas

| Regra | Descrição |
|-------|-----------|
| RN-01 | IDs gerados automaticamente (UUID) |
| RN-02 | Pessoa deve existir para transação |
| RN-03 | Menores de 18 só podem registrar despesas |
| RN-04 | Exclusão de pessoa → cascade delete de transações |
| RN-05 | Valor sempre positivo, tipo define sinal |
| RN-06 | Saldo = TotalReceitas - TotalDespesas |
| RN-07 | Pessoas sem transações aparecem nos totais com zeros |