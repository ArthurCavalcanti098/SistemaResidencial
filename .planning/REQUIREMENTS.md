# REQUIREMENTS — Sistema de Controle de Gastos Residenciais

## v1 Requirements

### Infraestrutura

- [ ] **INFRA-01**: PostgreSQL 16 disponível via Docker Compose (database `controle_gastos`)
- [ ] **INFRA-02**: Solução .NET 8 com projeto ASP.NET Core Web API
- [ ] **INFRA-03**: Projeto React 18 + TypeScript + Vite 5 com Tailwind CSS 3
- [ ] **INFRA-04**: CORS habilitado no back-end para `http://localhost:5173`
- [ ] **INFRA-05**: EF Core 8 + Npgsql configurado com Connection String PostgreSQL

### Modelo de Dados

- [ ] **DATA-01**: Entidade `Pessoa` (Id UUID auto, Nome VARCHAR(100), Idade INT) com constraints CHECK
- [ ] **DATA-02**: Entidade `Transacao` (Id UUID auto, Descricao VARCHAR(200), Valor NUMERIC(18,2), Tipo INT, PessoaId FK) com constraints CHECK e ON DELETE CASCADE
- [ ] **DATA-03**: Enum `TipoTransacao` (Despesa=0, Receita=1)
- [ ] **DATA-04**: Índice em `Transacao.PessoaId` para consultas de totais
- [ ] **DATA-05**: Migration inicial criada e aplicada automaticamente no startup

### API — Pessoas

- [ ] **API-P-01**: `GET /api/pessoas` — listar todas ordenadas por Nome ascendente
- [ ] **API-P-02**: `POST /api/pessoas` — criar pessoa, Id gerado automaticamente (RN-01)
- [ ] **API-P-03**: `DELETE /api/pessoas/{id}` — excluir pessoa com cascade delete de transações (RN-04)
- [ ] **API-P-04**: Validação FluentValidation: Nome obrigatório, máx 100, Idade 0-150, Id não enviado pelo cliente
- [ ] **API-P-05**: Respostas HTTP: 200/201 sucesso, 204 exclusão, 400 validação, 404 não encontrado

### API — Transações

- [ ] **API-T-01**: `GET /api/transacoes` — listar todas com nome da pessoa, ordenadas por Descricao
- [ ] **API-T-02**: `POST /api/transacoes` — criar transação, Id gerado automaticamente (RN-01)
- [ ] **API-T-03**: Validação RN-02: PessoaId deve existir (404 se não)
- [ ] **API-T-04**: Validação RN-03: Menor de 18 anos só pode Despesa (400 se Receita)
- [ ] **API-T-05**: Validação RN-05: Valor > 0 (400 se zero/negativo)
- [ ] **API-T-06**: Validação FluentValidation: Descricao obrigatória máx 200, Valor > 0, Tipo válido, PessoaId obrigatório, Id não enviado

### API — Totais

- [ ] **API-TOT-01**: `GET /api/totais` — retornar totais por pessoa (receitas, despesas, saldo)
- [ ] **API-TOT-02**: RN-06: Cálculo correto — TotalReceitas = SUM(Valor WHERE Tipo=Receita), TotalDespesas = SUM(Valor WHERE Tipo=Despesa), Saldo = Receitas - Despesas
- [ ] **API-TOT-03**: RN-07: Pessoas sem transações aparecem com zeros
- [ ] **API-TOT-04**: Totais gerais: soma de todas as pessoas

### Front-end — Infraestrutura

- [ ] **FE-I-01**: Layout comum com barra de navegação (Pessoas, Transações, Totais)
- [ ] **FE-I-02**: Rotas: `/pessoas`, `/transacoes`, `/totais`, `/` redireciona para `/pessoas`
- [ ] **FE-I-03**: Cliente Axios com baseURL `http://localhost:5000/api` e interceptor de erro
- [ ] **FE-I-04**: Tipos TypeScript espelhando contratos da API
- [ ] **FE-I-05**: Estilização exclusivamente com Tailwind CSS 3

### Front-end — Pessoas

- [ ] **FE-P-01**: Formulário de criação: campos Nome e Idade
- [ ] **FE-P-02**: Listagem: tabela com Nome, Idade, botão Excluir
- [ ] **FE-P-03**: Modal de confirmação de exclusão com texto sobre cascade delete
- [ ] **FE-P-04**: Mensagens de erro/sucesso inline

### Front-end — Transações

- [ ] **FE-T-01**: Formulário: Descricao, Valor, Tipo (select), Pessoa (select populado via API)
- [ ] **FE-T-02**: RN-03 no front-end: opção Receita desabilitada quando pessoa selecionada < 18
- [ ] **FE-T-03**: Texto auxiliar amber quando menor selecionado: "Menores de 18 anos só podem registrar despesas"
- [ ] **FE-T-04**: Listagem: Descricao, Valor (R$), Tipo, Pessoa (nome)

### Front-end — Totais

- [ ] **FE-TOT-01**: Tabela por pessoa: Nome, Total Receitas, Total Despesas, Saldo
- [ ] **FE-TOT-02**: Bloco Totais Gerais: Total Receitas, Total Despesas, Saldo Líquido
- [ ] **FE-TOT-03**: Valores formatados em pt-BR BRL (`Intl.NumberFormat`)
- [ ] **FE-TOT-04**: Saldo negativo em `text-red-600 font-semibold`

### Documentação

- [ ] **DOC-01**: README.md com descrição, pré-requisitos, instruções de setup e execução
- [ ] **DOC-02**: Comentários XML (`///`) em todos os métodos públicos do back-end
- [ ] **DOC-03**: Comentários JSDoc em funções de API e hooks do front-end
- [ ] **DOC-04**: Swagger habilitado em desenvolvimento (`/swagger`)

## v2 Requirements (Deferred)

(Nenhum — escopo completo em v1 conforme SDD)

## Out of Scope

- Edição de pessoa — especificação SDD define apenas criar, listar, excluir
- Edição de transação — especificação SDD define apenas criar, listar
- Exclusão individual de transação — apenas cascade na exclusão de pessoa
- Autenticação / multi-usuário — sistema local single-user
- Publicação web pública — aplicação local apenas
- Relatórios exportáveis (PDF/Excel)
- App mobile nativo

## Traceability

| REQ-ID | Fase SDD | Seção SDD |
|--------|----------|-----------|
| INFRA-01 a INFRA-05 | Fase 1 | 4, 12.5, 12.6 |
| DATA-01 a DATA-05 | Fase 2 | 6, 12.3, 12.4 |
| API-P-01 a API-P-05 | Fase 3 | 7 (RN-01, RN-04, RN-08), 8.2, 10.1 |
| API-T-01 a API-T-06 | Fase 4 | 7 (RN-02, RN-03, RN-05), 8.3, 10.2 |
| API-TOT-01 a API-TOT-04 | Fase 5 | 7 (RN-06, RN-07), 10.3 |
| FE-I-01 a FE-I-05 | Fase 6 | 11.1, 11.3, 11.7, 11.8 |
| FE-P-01 a FE-P-04 | Fase 6 | 11.4 |
| FE-T-01 a FE-T-04 | Fase 7 | 11.5 |
| FE-TOT-01 a FE-TOT-04 | Fase 8 | 11.6 |
| DOC-01 a DOC-04 | Fase 9 | 13, 2.4 |