# Análise de Requisitos Funcionais  
Sistema de Reembolso Corporativo

## RF01 – Cadastro de Despesa
O sistema deve permitir que o funcionário cadastre uma nova despesa corporativa informando, no mínimo:
- Tipo da despesa
- Valor da despesa
- Data da despesa

---

## RF02 – Validação de Data da Despesa
O sistema deve impedir o cadastro de despesas com data futura.

---

## RF03 – Classificação Automática de Despesas de Alimentação
O sistema deve aplicar automaticamente regras de aprovação para despesas do tipo **Alimentação**, conforme o valor informado:
- Despesas de Alimentação com valor **até R$ 50,00** devem ser marcadas como **Aprovadas Automaticamente**.
- Despesas de Alimentação com valor **acima de R$ 50,00** devem ser marcadas como **Requer Aprovação Manual**.

---

## RF04 – Persistência de Despesas Acima do Teto
O sistema deve permitir o salvamento de despesas de Alimentação acima de R$ 50,00, mesmo que estas sejam classificadas como **Requer Aprovação Manual**.

---

## RF05 – Prevenção de Lançamento Duplicado
O sistema deve impedir o cadastro de duas despesas que possuam:
- O mesmo tipo de despesa
- O mesmo valor
- Um intervalo de tempo entre os lançamentos inferior a **2 minutos**

Essa validação deve ser aplicada para evitar lançamentos duplicados ou fraude simples.

---

## RF06 – Consulta de Despesas pelo Gestor
O sistema deve disponibilizar um painel para que o gestor possa visualizar as despesas cadastradas pelos funcionários.

---

## RF07 – Visualização do Status da Despesa
O sistema deve permitir que o gestor visualize o status de cada despesa, indicando se ela foi:
- Aprovada Automaticamente
- Marcada como Requer Aprovação Manual
