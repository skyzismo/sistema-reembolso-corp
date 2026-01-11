# Análise de Casos de Uso  
Sistema de Reembolso Corporativo

## Atores

- **Funcionário**: responsável por cadastrar despesas corporativas.
- **Gestor**: responsável por visualizar as despesas cadastradas.

---

## UC01 – Cadastrar Despesa

### Ator Principal
Funcionário

### Descrição
Permite que o funcionário registre uma nova despesa corporativa no sistema.

### Pré-condições
- O sistema está disponível.
- O funcionário informa os dados obrigatórios da despesa.

### Fluxo Principal
1. O funcionário acessa o formulário de cadastro de despesas.
2. O funcionário informa o tipo da despesa.
3. O funcionário informa o valor da despesa.
4. O funcionário informa a data da despesa.
5. O sistema valida se a data informada não é futura.
6. O sistema verifica se já existe uma despesa com o mesmo tipo e valor registrada nos últimos 2 minutos.
7. O sistema aplica as regras de negócio para classificação automática da despesa.
8. O sistema salva a despesa com o status correspondente.
9. O sistema exibe uma mensagem de sucesso.

### Fluxos Alternativos
- **FA01 – Data futura**
  - No passo 5, se a data for futura, o sistema impede o cadastro e informa o erro ao funcionário.
- **FA02 – Lançamento duplicado**
  - No passo 6, se for identificada uma despesa duplicada dentro do intervalo de 2 minutos, o sistema impede o cadastro e informa o erro ao funcionário.

### Pós-condições
- A despesa é persistida no sistema com status definido automaticamente.

---

## UC02 – Classificar Despesa de Alimentação

### Ator Principal
Sistema

### Descrição
Realiza automaticamente a classificação do status da despesa do tipo Alimentação com base no valor informado.

### Pré-condições
- Uma despesa do tipo Alimentação foi informada no cadastro.

### Fluxo Principal
1. O sistema identifica que o tipo da despesa é Alimentação.
2. O sistema verifica o valor informado.
3. O sistema define o status como:
   - **Aprovada Automaticamente**, se o valor for até R$ 50,00.
   - **Requer Aprovação Manual**, se o valor for superior a R$ 50,00.

### Pós-condições
- A despesa é salva com o status definido conforme a regra de negócio.

---

## UC03 – Visualizar Despesas

### Ator Principal
Gestor

### Descrição
Permite que o gestor visualize todas as despesas cadastradas no sistema.

### Pré-condições
- Existem despesas cadastradas no sistema.

### Fluxo Principal
1. O gestor acessa o painel de visualização de despesas.
2. O sistema recupera a lista de despesas cadastradas.
3. O sistema exibe as despesas com suas informações principais.
4. O gestor visualiza o status de cada despesa.

### Pós-condições
- Nenhuma alteração é realizada nos dados.

---

## UC04 – Visualizar Status da Despesa

### Ator Principal
Gestor

### Descrição
Permite que o gestor identifique o status de aprovação de cada despesa cadastrada.

### Pré-condições
- Existem despesas cadastradas.

### Fluxo Principal
1. O gestor acessa o painel de despesas.
2. O sistema exibe o status associado a cada despesa:
   - Aprovada Automaticamente
   - Requer Aprovação Manual

### Pós-condições
- Nenhuma alteração é realizada no sistema.
