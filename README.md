# Sistema de Controle de Gastos Residenciais

Sistema local cliente-servidor para controle de gastos residenciais. Permite cadastrar pessoas da residência, registrar transações financeiras (receitas e despesas) vinculadas a cada pessoa e consultar totais consolidados por pessoa e gerais.

---

## Pré-requisitos

| Ferramenta | Versão | Como verificar |
|-----------|--------|----------------|
| .NET SDK | **10.0** (net10.0) | `dotnet --version` |
| Node.js | ≥ 18 | `node --version` |
| PostgreSQL | **16** | via Docker Compose (arquivo incluso no repositório) |
| Docker / Docker Compose | ≥ 24 | `docker compose version` |

> O projeto tem como alvo `net10.0` (conforme `ControleGastos.Api.csproj`). Certifique-se de ter o SDK do .NET 10 instalado.

---

## Como executar

### 1. Subir o banco de dados

```bash
docker compose up -d
```

Isso cria um container PostgreSQL 16 com o banco `controle_gastos`, usuário `postgres`, senha `postgres`, escutando na porta `5432`.

### 2. Rodar o backend (API)

O schema do banco é criado automaticamente na primeira execução — as migrations são aplicadas via `db.Database.Migrate()` no startup do programa (modo Development). Nenhum comando manual de migration é necessário.

```bash
dotnet run --project backend/ControleGastos.Api
```

A API sobe em **http://localhost:5000**.
Swagger disponível em **http://localhost:5000/swagger**.

### 3. Rodar o frontend

```bash
cd frontend
npm install
npm run dev
```

O frontend inicia em **http://localhost:5173**.

---

## URLs

| Serviço | URL |
|----------|-----|
| Frontend | http://localhost:5173 |
| API (base) | http://localhost:5000 |
| Swagger | http://localhost:5000/swagger |
| Banco (PostgreSQL) | `localhost:5432` |

---

## Configuração de conexão

A connection string já vem pronta nos arquivos de configuração — funciona out-of-the-box com o `docker-compose.yml` fornecido:

| Arquivo | Connection string |
|---------|-------------------|
| `backend/ControleGastos.Api/appsettings.json` | `Host=localhost;Port=5432;Database=controle_gastos;Username=postgres;Password=postgres` |
| `backend/ControleGastos.Api/appsettings.Development.json` | (mesma string) |
| `docker-compose.yml` | `POSTGRES_USER=postgres` / `POSTGRES_PASSWORD=postgres` / `POSTGRES_DB=controle_gastos` / porta `5432` |

Nenhuma variável de ambiente ou arquivo `.env` adicional é necessário — o ambiente development já cobre toda a configuração.

---

## Funcionalidades

### Pessoas

- **Listar** — retorna todas as pessoas cadastradas, ordenadas por nome
- **Criar** — cadastra uma nova pessoa (nome + idade); o `Id` é gerado automaticamente (UUID)
- **Excluir** — remove uma pessoa e todas as suas transações (cascade delete no banco)

### Transações

- **Listar** — retorna todas as transações, ordenadas por descrição, incluindo o nome da pessoa vinculada
- **Criar** — registra uma nova transação (descrição, valor, tipo — `Receita` ou `Despesa`, e `PessoaId`)

### Totais

- **Consulta** — retorna, para cada pessoa (incluindo as que não possuem transações):
  - Total de receitas
  - Total de despesas
  - Saldo (receitas menos despesas)
- Retorna também os totais gerais consolidados (soma dos totais de todas as pessoas)

---

## Regras de negócio

| Regra | Detalhe |
|-------|---------|
| Geração automática de IDs | Todos os IDs são UUIDs gerados automaticamente pelo servidor (via `Guid.NewGuid()` no código e `gen_random_uuid()` no banco como fallback) |
| Validação estrutural de Pessoa | Nome obrigatório, máximo 100 caracteres, não vazio. Idade obrigatória, entre 0 e 150 |
| Validação estrutural de Transação | Descrição obrigatória, máximo 200 caracteres. Valor é obrigatório e deve ser maior que zero. Tipo deve ser exatamente `"Despesa"` ou `"Receita"`. PessoaId obrigatório |
| Pessoa deve existir | Antes de criar uma transação, o sistema verifica se a `PessoaId` informada corresponde a uma pessoa cadastrada. Caso contrário, retorna `404 Not Found` |
| Menores de 18 anos | Pessoas com idade inferior a 18 anos não podem registrar receitas — apenas despesas. Violação retorna `400` com código de erro `MENOR_IDADE_RECEITA` |
| Cascade delete | Ao excluir uma pessoa, todas as suas transações são automaticamente removidas (`ON DELETE CASCADE` na FK) |
| Cálculo do saldo | Saldo por pessoa = `TotalReceitas - TotalDespesas`. O saldo geral (saldo líquido) é a soma do saldo de cada pessoa |
| Pessoas sem transações nos totais | Pessoas que não possuem nenhuma transação aparecem na consulta com todos os valores zerados (receitas = 0, despesas = 0, saldo = 0) |
| Exclusão de pessoa inexistente | Retorna `404` se o `id` informado não corresponder a uma pessoa cadastrada |

---

## Estrutura do projeto

```
SistemaResidencial/
├── docker-compose.yml              # Container PostgreSQL 16
├── backend/
│   ├── ControleGastos.slnx         # Solution file (.NET 10)
│   └── ControleGastos.Api/
│       ├── Program.cs              # Entry point, config DB, Swagger, CORS
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       ├── ControleGastos.Api.csproj
│       ├── Controllers/            # Endpoints REST (Pessoas, Transações, Totais)
│       ├── Services/               # Lógica de negócio
│       ├── Services/Interfaces/    # Contratos dos serviços
│       ├── Validators/             # FluentValidation (CriarPessoa, CriarTransação)
│       ├── Data/                   # AppDbContext, Migrations EF Core
│       ├── Models/                 # Entidades (Pessoa, Transação, TipoTransacao)
│       ├── DTOs/                   # Objetos de transferência (entrada/saída)
│       ├── Exceptions/             # Middleware de erros, exceções customizadas
│       ├── Migrations/             # Migrations EF Core (geradas)
│       └── Properties/
│           └── launchSettings.json
├── frontend/
│   ├── public/                     # Ativos estáticos (favicon, icons SVG)
│   ├── src/
│   │   ├── api/                    # Axios client + módulos por recurso
│   │   ├── components/             # Componentes reutilizáveis (Layout, modals, etc.)
│   │   ├── hooks/                  # Custom hooks (por recurso + useForm)
│   │   ├── pages/                  # Páginas (Pessoas, Transações, Totais)
│   │   ├── types/                  # Tipos TypeScript compartilhados
│   │   ├── App.tsx                 # Rotas da aplicação
│   │   ├── main.tsx                # Entry point React
│   │   └── index.css               # Tailwind CSS base
│   ├── package.json
│   ├── tsconfig.json
│   └── vite.config.ts
└── README.md
```

---

## Troubleshooting

### PostgreSQL não sobe (ex: porta em uso)

```bash
docker compose down  # derrubar contêineres anteriores
sudo lsof -i :5432   # descobrir o que está travando a porta 5432
```

Se outro PostgreSQL local já estiver rodando, escolha uma das duas opções:
- Parar a instância local (`sudo systemctl stop postgresql`)
- Alterar a porta exposta no `docker-compose.yml` de `5432:5432` para `5433:5432` e atualizar a connection string em `appsettings.json` do `Port=5432` para `Port=5433`

### API não conecta ao banco

Verifique se o container está rodando e saudável:

```bash
docker compose ps
```

Confira que a connection string em `appsettings.json` bate com as credenciais do container (usuário `postgres`, senha `postgres`, banco `controle_gastos`).

### Frontend não carrega / dados não aparecem

O backend habilita CORS apenas para `http://localhost:5173`. Certifique-se de que o frontend está rodando exatamente na porta `5173` (padrão do Vite). O backend deve estar rodando em `http://localhost:5000`.