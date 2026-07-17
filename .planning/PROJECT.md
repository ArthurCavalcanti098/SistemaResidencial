# PROJECT — Sistema de Controle de Gastos Residenciais

## What This Is

Sistema local cliente-servidor para controle de gastos residenciais. Permite cadastrar pessoas da residência, registrar transações financeiras (receitas e despesas) vinculadas a cada pessoa e consultar totais consolidados por pessoa e gerais.

**Tipo:** Aplicação local, single-user, sem autenticação. Back-end .NET 8 + front-end React 18 + PostgreSQL 16. Executado inteiramente na máquina do usuário, comunicação via HTTP em localhost. Dados persistidos em PostgreSQL — sobrevivem ao fechamento da aplicação.

## Core Value

Centralizar o registro de entradas e saídas financeiras de membros de uma residência, com regras específicas por faixa etária (menores de 18 só podem registrar despesas) e visão consolidada do saldo de cada integrante e do grupo como um todo.

## Context

- Projeto greenfield — diretório vazio, sem código existente.
- Especificação completa via SDD.md (versão 1.1, aprovado para implementação).
- Stack tecnológica obrigatória e não negociável: .NET 8, C# 12, ASP.NET Core Web API, EF Core 8, Npgsql, FluentValidation 11, Swagger 6, React 18, TypeScript 5, Vite 5, React Router 6, Axios 1, Tailwind CSS 3, PostgreSQL 16.
- Módulos funcionais: Pessoas (CRUD parcial — criar, listar, excluir), Transações (CRUD parcial — criar, listar), Totais (consulta somente leitura).
- Regras de negócio críticas: RN-03 (menores de 18 só despesas), RN-04 (cascade delete), RN-06 (cálculo de totais).
- Comportamentos proibidos extensivamente documentados no SDD Seção 9.

## Requirements

### Validated

(None yet — ship to validate)

### Active

- [ ] PESSOAS-01: Criar pessoa com Nome e Idade (Id auto UUID)
- [ ] PESSOAS-02: Listar todas as pessoas ordenadas por Nome
- [ ] PESSOAS-03: Excluir pessoa com cascade delete de transações
- [ ] TRANS-01: Criar transação (Descricao, Valor, Tipo, PessoaId) com validações RN-02, RN-03, RN-05
- [ ] TRANS-02: Listar todas as transações ordenadas por Descricao, incluindo nome da pessoa
- [ ] TOTAIS-01: Consultar totais por pessoa (receitas, despesas, saldo) incluindo pessoas sem transações
- [ ] TOTAIS-02: Consultar totais gerais consolidados
- [ ] FRONT-01: Navegação entre telas Pessoas, Transações, Totais com Layout comum
- [ ] FRONT-02: Tela de Pessoas com formulário de criação, listagem e modal de confirmação de exclusão
- [ ] FRONT-03: Tela de Transações com formulário (RN-03 no select de tipo) e listagem
- [ ] FRONT-04: Tela de Totais com tabela por pessoa, totais gerais e formatação pt-BR BRL
- [ ] INFRA-01: Docker Compose PostgreSQL 16
- [ ] INFRA-02: EF Core + migrations com constraints CHECK, FK ON DELETE CASCADE
- [ ] INFRA-03: CORS habilitado para localhost:5173
- [ ] DOCS-01: README.md com instruções de setup e execução
- [ ] DOCS-02: Comentários XML/JSDoc em todo código público

### Out of Scope

- Edição de pessoa — definido no SDD
- Edição e exclusão de transação — definido no SDD
- Autenticação / multi-usuário — sistema local single-user
- Publicação web pública / SaaS — aplicação local
- Relatórios exportáveis (PDF/Excel)
- App mobile nativo

## Key Decisions

| Decision | Rationale | Outcome |
|----------|-----------|---------|
| PostgreSQL 16 | Requisito do projeto; integridade relacional | — Pending |
| UUID como ID | Unicidade global, geração automática | — Pending |
| Tailwind CSS 3 | Única biblioteca de estilização permitida | — Pending |
| FK ON DELETE CASCADE | Garantia de integridade no banco | — Pending |
| FluentValidation para DTOs | Separação de validação estrutural vs regras de negócio | — Pending |
| Arquitetura em camadas | Controllers → Services → EF Core → PostgreSQL | — Pending |

## Evolution

This document evolves at phase transitions and milestone boundaries.

**After each phase transition** (via `/gsd-transition`):
1. Requirements invalidated? → Move to Out of Scope with reason
2. Requirements validated? → Move to Validated with phase reference
3. New requirements emerged? → Add to Active
4. Decisions to log? → Add to Key Decisions
5. "What This Is" still accurate? → Update if drifted

**After each milestone** (via `/gsd-complete-milestone`):
1. Full review of all sections
2. Core Value check — still the right priority?
3. Audit Out of Scope — reasons still valid?
4. Update Context with current state

---
*Last updated: 16/07/2026 after initialization*