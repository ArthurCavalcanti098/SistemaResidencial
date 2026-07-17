# ROADMAP — Sistema de Controle de Gastos Residenciais

**Granularity:** coarse  
**Mode:** mvp  
**Total phases:** 9  
**Requirements covered:** 35/35 ✓

---

### Phase 1: Infraestrutura e Setup

**Goal:** Estrutura base do projeto com todos os frameworks configurados e comunicação funcionando.

**Mode:** mvp

**Success Criteria:**
1. `docker-compose up -d` sobe PostgreSQL 16 com database `controle_gastos` acessível na porta 5432
2. `dotnet run` inicia a API .NET 8 em `http://localhost:5000` com Swagger em `/swagger`
3. `npm run dev` inicia o front-end React + TypeScript + Tailwind em `http://localhost:5173`
4. Front-end consegue fazer uma requisição HTTP para o back-end (CORS funcionando)

**Requirements:** INFRA-01, INFRA-02, INFRA-03, INFRA-04

**Tasks:**
- Criar docker-compose.yml com PostgreSQL 16
- Criar solução .NET 8 + projeto Web API
- Adicionar packages NuGet: Npgsql, FluentValidation, Swashbuckle
- Configurar CORS para localhost:5173
- Criar projeto React + TypeScript + Vite
- Configurar Tailwind CSS 3 + PostCSS
- Instalar Axios e React Router
- Configurar cliente Axios com baseURL

---

### Phase 2: Modelo de Dados e Migrations

**Goal:** Entidades de domínio, DbContext, constraints e migration inicial aplicada.

**Mode:** mvp

**Success Criteria:**
1. Migration `InitialCreate` gerada com tabelas `pessoas` e `transacoes`
2. Constraints CHECK ativas: Nome not empty, Idade 0-150, Valor > 0
3. FK `PessoaId` com `ON DELETE CASCADE` configurada
4. Índice em `PessoaId` na tabela `transacoes`
5. `Database.Migrate()` executado no startup — dados persistem após reiniciar

**Requirements:** DATA-01, DATA-02, DATA-03, DATA-04, DATA-05, INFRA-05

**Tasks:**
- Criar Models: Pessoa.cs, Transacao.cs, TipoTransacao.cs
- Criar AppDbContext com DbSets e OnModelCreating (Fluent API)
- Criar Entity Configurations: PessoaConfiguration, TransacaoConfiguration
- Configurar Connection String no appsettings.Development.json
- Registrar DbContext com Npgsql no Program.cs
- Gerar e aplicar migration inicial

---

### Phase 3: API — Pessoas

**Goal:** CRUD parcial de pessoas com validação, cascade delete e respostas HTTP padronizadas.

**Mode:** mvp

**Success Criteria:**
1. POST /api/pessoas cria pessoa com UUID gerado, retorna 201
2. Nome vazio ou Idade inválida retorna 400 com mensagem
3. GET /api/pessoas lista todas ordenadas por Nome
4. DELETE /api/pessoas/{id} exclui pessoa e transações (cascade), retorna 204
5. DELETE com id inexistente retorna 404

**Requirements:** API-P-01, API-P-02, API-P-03, API-P-04, API-P-05

**Tasks:**
- Criar DTOs: CriarPessoaDto, PessoaResponseDto
- Criar IPessoaService e PessoaService
- Criar CriarPessoaValidator (FluentValidation)
- Criar BusinessException, NotFoundException
- Criar PessoasController
- Configurar middleware de exceções para mapear para HTTP

---

### Phase 4: API — Transações

**Goal:** CRUD parcial de transações com validações de negócio RN-02, RN-03, RN-05.

**Mode:** mvp

**Success Criteria:**
1. POST /api/transacoes cria transação para pessoa existente, retorna 201
2. Transação para pessoa inexistente retorna 404 "Pessoa não encontrada"
3. Receita para menor de 18 retorna 400 "Menores de 18 anos só podem registrar despesas"
4. Valor zero ou negativo retorna 400 "Valor deve ser maior que zero"
5. GET /api/transacoes lista todas com nome da pessoa, ordenadas por Descricao

**Requirements:** API-T-01, API-T-02, API-T-03, API-T-04, API-T-05, API-T-06

**Tasks:**
- Criar DTOs: CriarTransacaoDto, TransacaoResponseDto
- Criar ITransacaoService e TransacaoService (RN-02, RN-03, RN-05)
- Criar CriarTransacaoValidator (FluentValidation)
- Criar TransacoesController

---

### Phase 5: API — Totais

**Goal:** Consulta de totais por pessoa e gerais conforme RN-06 e RN-07.

**Mode:** mvp

**Success Criteria:**
1. GET /api/totais retorna totais de todas as pessoas cadastradas
2. Pessoas sem transações aparecem com receitas=0, despesas=0, saldo=0
3. Saldo = TotalReceitas - TotalDespesas (ordem correta)
4. Totais gerais = soma de todas as pessoas
5. Resposta inclui `pessoas[]` (per-person) e `totaisGerais` (consolidado)

**Requirements:** API-TOT-01, API-TOT-02, API-TOT-03, API-TOT-04

**Tasks:**
- Criar DTOs: TotalPorPessoaDto, TotaisResponseDto
- Criar ITotaisService e TotaisService
- Criar TotaisController

---

### Phase 6: Front-end — Layout e Pessoas

**Goal:** Interface funcional com navegação e tela completa de Pessoas.

**Mode:** mvp

**Success Criteria:**
1. Barra de navegação fixa com links Pessoas, Transações, Totais
2. Rota `/` redireciona para `/pessoas`
3. Formulário de criação de pessoa funcional com validação de campos
4. Listagem de pessoas com botão Excluir por linha
5. Modal de confirmação antes de excluir com texto sobre cascade
6. Mensagens de erro/sucesso exibidas inline

**Requirements:** FE-I-01, FE-I-02, FE-I-03, FE-I-04, FE-I-05, FE-P-01, FE-P-02, FE-P-03, FE-P-04

**Tasks:**
- Criar types: pessoa.ts, transacao.ts, totais.ts
- Criar api/client.ts (Axios com interceptor)
- Criar api/pessoasApi.ts
- Criar componentes: Layout.tsx, ConfirmModal.tsx, FormMessage.tsx
- Criar hooks: usePessoas.ts
- Criar PessoasPage.tsx
- Configurar App.tsx com React Router

---

### Phase 7: Front-end — Transações

**Goal:** Tela de transações com regra RN-03 aplicada na UI.

**Mode:** mvp

**Success Criteria:**
1. Formulário de criação com selects de Tipo e Pessoa populados
2. Select de Pessoa carrega dados via GET /api/pessoas
3. Ao selecionar pessoa < 18, opção Receita fica disabled
4. Texto amber "Menores de 18 anos só podem registrar despesas" aparece
5. Listagem com Descricao, Valor (R$), Tipo, Pessoa
6. Erros da API exibidos inline

**Requirements:** FE-T-01, FE-T-02, FE-T-03, FE-T-04

**Tasks:**
- Criar api/transacoesApi.ts
- Criar hooks: useTransacoes.ts
- Criar TransacoesPage.tsx

---

### Phase 8: Front-end — Totais

**Goal:** Tela de totais com formatação brasileira e indicadores visuais.

**Mode:** mvp

**Success Criteria:**
1. Tabela com uma linha por pessoa: Nome, Receitas, Despesas, Saldo
2. Bloco Totais Gerais abaixo da tabela
3. Valores formatados em R$ (pt-BR) com `Intl.NumberFormat`
4. Saldo negativo exibido em vermelho (`text-red-600`)
5. Pessoa sem transações aparece com R$ 0,00

**Requirements:** FE-TOT-01, FE-TOT-02, FE-TOT-03, FE-TOT-04

**Tasks:**
- Criar api/totaisApi.ts
- Criar TotaisPage.tsx

---

### Phase 9: Documentação e Polimento

**Goal:** README, comentários no código, Swagger e testes manuais.

**Mode:** mvp

**Success Criteria:**
1. README.md completo com instruções de setup e execução
2. Todos os métodos públicos do back-end com XML docs (`///`)
3. Funções de API e hooks do front-end com JSDoc
4. Swagger acessível em `/swagger` com todos os endpoints documentados
5. Checklist de 11 testes manuais executado com sucesso

**Requirements:** DOC-01, DOC-02, DOC-03, DOC-04

**Tasks:**
- Escrever README.md
- Adicionar XML docs em Controllers, Services, métodos públicos
- Adicionar JSDoc em api/*.ts e hooks/*.ts
- Executar checklist de testes do SDD Seção 15.1