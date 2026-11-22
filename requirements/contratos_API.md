
# Contratos da API REST
Sistema de Reembolso Corporativo

## Visão Geral

- Base URL: `/api`
- Formato de dados: JSON
- Comunicação via HTTP REST
- Identificação de recursos por UUID.
- Operações de aprovação, rejeição e exclusão restritas ao perfil de gestor.

---

## Endpoints

### 1. Cadastro de Despesa

`POST /api/despesas — Request`

```json
{
  "tipo": "Alimentacao",
  "valor": 45.5,
  "data": "2026-01-09"
}
```

`POST /api/despesas — Response 201 (Created)`

```json
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 45.5,
  "data": "2026-01-09",
  "status": "AprovadaAutomaticamente"
}
```

`POST /api/despesas — Response 400 (Data futura)`

```json
{
  "mensagem": "Não é permitido cadastrar despesas com data futura."
}
```

`POST /api/despesas — Response 409 (Despesa duplicada)`

```json
{
  "mensagem": "Já existe uma despesa com o mesmo tipo e valor cadastrada nos últimos 2 minutos."
}
```

---

### 2. Listagem de Despesas

`GET /api/despesas — Response 200 (Lista)`

```json
[
  {
    "id": "uuid",
    "tipo": "Alimentacao",
    "valor": 75.0,
    "data": "2026-01-08",
    "status": "RequerAprovacaoManual"
  },
  {
    "id": "uuid",
    "tipo": "Transporte",
    "valor": 30.0,
    "data": "2026-01-07",
    "status": "AprovadaAutomaticamente"
  },
  {
    "id": "uuid",
    "tipo": "Hospedagem",
    "valor": 120.0,
    "data": "2026-01-06",
    "status": "AprovadaManualmente"
  },
  {
    "id": "uuid",
    "tipo": "Outros",
    "valor": 90.0,
    "data": "2026-01-05",
    "status": "Rejeitada"
  }
]
```

---

### 3. Consulta de Despesa por ID

`GET /api/despesas/{id} — Response 200`

```json
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 75.0,
  "data": "2026-01-08",
  "status": "RequerAprovacaoManual"
}
```

`GET /api/despesas/{id} — Response 404`

```json
{
  "mensagem": "Despesa não encontrada."
}
```

---

### 4. Aprovação Manual de Despesa

`PUT /api/despesas/{id}/aprovar — Request`

Não necessita de corpo na requisição.

**Regra de negócio:**
- Somente o gestor pode executar a operação.
- A despesa deve existir.
- A despesa deve possuir o status `RequerAprovacaoManual`.
- Após a aprovação, o status deve ser atualizado para `AprovadaManualmente`.

`PUT /api/despesas/{id}/aprovar — Response 200 (OK)`

```json
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 75.0,
  "data": "2026-01-08",
  "status": "AprovadaManualmente"
}
```

`PUT /api/despesas/{id}/aprovar — Response 403 (Forbidden)`

```json
{
  "mensagem": "Somente gestores podem aprovar despesas."
}
```

`PUT /api/despesas/{id}/aprovar — Response 404 (Not Found)`

```json
{
  "mensagem": "Despesa não encontrada."
}
```

`PUT /api/despesas/{id}/aprovar — Response 409 (Conflict)`

```json
{
  "mensagem": "A despesa não está aguardando aprovação manual."
}
```

---

### 5. Rejeição de Despesa

`PUT /api/despesas/{id}/rejeitar — Request`

Não necessita de corpo na requisição.

**Regra de negócio:**
- Somente o gestor pode executar a operação.
- A despesa deve existir.
- A despesa deve possuir o status `RequerAprovacaoManual`.
- Após a rejeição, o status deve ser atualizado para `Rejeitada`.

`PUT /api/despesas/{id}/rejeitar — Response 200 (OK)`

```json
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 75.0,
  "data": "2026-01-08",
  "status": "Rejeitada"
}
```

`PUT /api/despesas/{id}/rejeitar — Response 403 (Forbidden)`

```json
{
  "mensagem": "Somente gestores podem rejeitar despesas."
}
```

`PUT /api/despesas/{id}/rejeitar — Response 404 (Not Found)`

```json
{
  "mensagem": "Despesa não encontrada."
}
```

`PUT /api/despesas/{id}/rejeitar — Response 409 (Conflict)`

```json
{
  "mensagem": "A despesa não está aguardando aprovação manual."
}
```

---

### 6. Exclusão de Despesa Inferior a R$ 50,00

`DELETE /api/despesas/{id} — Request`

Não necessita de corpo na requisição.

**Regra de negócio:**
- Somente o gestor pode executar a operação.
- A despesa deve existir.
- O valor da despesa deve ser estritamente inferior a R$ 50,00.
- Despesas com valor igual ou superior a R$ 50,00 não podem ser excluídas por essa funcionalidade.
- A exclusão remove o registro do banco de dados.

`DELETE /api/despesas/{id} — Response 204 (No Content)`

A despesa foi excluída com sucesso. A resposta não possui corpo.

`DELETE /api/despesas/{id} — Response 403 (Forbidden)`

```json
{
  "mensagem": "Somente gestores podem excluir despesas."
}
```

`DELETE /api/despesas/{id} — Response 404 (Not Found)`

```json
{
  "mensagem": "Despesa não encontrada."
}
```

`DELETE /api/despesas/{id} — Response 409 (Conflict)`

```json
{
  "mensagem": "Não é permitido excluir despesas com valor igual ou superior a R$ 50,00."
}
```

---

## DTOs

### CreateDespesaRequestDTO

```json
{
  "tipo": "string",
  "valor": 0,
  "data": "string (ISO 8601)"
}
```

### DespesaResponseDTO

```json
{
  "id": "uuid",
  "tipo": "string",
  "valor": 0,
  "data": "string (ISO 8601)",
  "status": "string"
}
```

### ErrorResponseDTO

```json
{
  "mensagem": "string"
}
```

---

## Enum TipoDespesa

```json
[
  "Alimentacao",
  "Transporte",
  "Hospedagem",
  "Outros"
]
```

## Enum StatusDespesa

```json
[
  "AprovadaAutomaticamente",
  "RequerAprovacaoManual",
  "AprovadaManualmente",
  "Rejeitada"
]
```

---

## Regras de Autorização

As operações de aprovação, rejeição e exclusão devem ser restritas ao perfil de gestor.

Caso um usuário sem permissão tente executar essas operações, a API deverá retornar HTTP 403 (Forbidden).

A implementação da autorização deverá garantir que as regras sejam aplicadas no backend, não dependendo exclusivamente das restrições existentes na interface frontend.
