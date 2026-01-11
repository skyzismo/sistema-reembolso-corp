# Contratos da API REST  
Sistema de Reembolso Corporativo

## Visão Geral

- Base URL: `/api`
- Formato de dados: JSON
- Comunicação via HTTP REST

---


## Endpoints
`POST /api/despesas — Request`
{
  "tipo": "Alimentacao",
  "valor": 45.5,
  "data": "2026-01-09"
}

`POST /api/despesas — Response 201 (Created)`
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 45.5,
  "data": "2026-01-09",
  "status": "AprovadaAutomaticamente"
}


`POST /api/despesas — Response 400 (Data futura)`
{
  "mensagem": "Não é permitido cadastrar despesas com data futura."
}


`POST /api/despesas — Response 409 (Despesa duplicada)`
{
  "mensagem": "Já existe uma despesa com o mesmo tipo e valor cadastrada nos últimos 2 minutos."
}


`GET /api/despesas — Response 200 (Lista)`
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
  }
]


`GET /api/despesas/{id} — Response 200`
{
  "id": "uuid",
  "tipo": "Alimentacao",
  "valor": 75.0,
  "data": "2026-01-08",
  "status": "RequerAprovacaoManual"
}


`GET /api/despesas/{id} — Response 404`
{
  "mensagem": "Despesa não encontrada."
}


`CreateDespesaRequestDTO`
{
  "tipo": "string",
  "valor": 0,
  "data": "string (ISO 8601)"
}


`DespesaResponseDTO`
{
  "id": "uuid",
  "tipo": "string",
  "valor": 0,
  "data": "string (ISO 8601)",
  "status": "string"
}


`ErrorResponseDTO`
{
  "mensagem": "string"
}


`Enum TipoDespesa`
[
  "Alimentacao",
  "Transporte",
  "Hospedagem",
  "Outros"
]

`Enum StatusDespesa`
[
  "AprovadaAutomaticamente",
  "RequerAprovacaoManual"
]



