# Sistema de Reembolso Corporativo

Este projeto é uma aplicação fullstack desenvolvida como desafio técnico, com o objetivo de gerenciar **reembolsos de despesas corporativas** seguindo regras específicas de negócio.

## 📌 Visão Geral

Funcionários podem registrar despesas de viagem, enquanto gestores têm acesso a um painel para visualização e acompanhamento dessas solicitações.

O sistema aplica automaticamente políticas internas da empresa no momento do lançamento das despesas.

## ⚙️ Regras de Negócio Principais

- Funcionários podem cadastrar despesas informando:
  - Tipo da despesa
  - Valor
  - Data
- Despesas do tipo **Alimentação**:
  - Até R$ 50,00 → **Aprovada automaticamente**
  - Acima de R$ 50,00 → **Requer aprovação manual**
- O sistema **não permite**:
  - Lançamento de despesas com data futura
  - Lançamento de duas despesas com o **mesmo valor e tipo** dentro de um intervalo de **2 minutos**

## 🧱 Arquitetura e Tecnologias

- **Backend:** C# .NET  
- **Frontend:** Angular  
- **Comunicação:** API REST

## 🎯 Objetivo do Projeto

Demonstrar a capacidade de traduzir requisitos de negócio em uma aplicação funcional, com regras bem definidas, validações consistentes e separação clara de responsabilidades entre frontend e backend.
