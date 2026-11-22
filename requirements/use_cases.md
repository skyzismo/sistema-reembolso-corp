
# Análise de Casos de Uso
Sistema de Reembolso Corporativo

## Atores

- **Funcionário**: responsável por cadastrar despesas corporativas.
- **Gestor**: responsável por visualizar, aprovar, rejeitar e excluir despesas conforme as regras de negócio.
- **Sistema**: responsável por executar as validações e aplicar automaticamente as regras de classificação das despesas.

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
   - Aprovada Manualmente
   - Rejeitada

### Pós-condições
- Nenhuma alteração é realizada no sistema.

---

## UC05 – Aprovar Despesa

### Ator Principal
Gestor

### Descrição
Permite que o gestor aprove manualmente uma despesa que esteja aguardando aprovação.

### Pré-condições
- A despesa está cadastrada no sistema.
- A despesa possui o status **Requer Aprovação Manual**.
- O usuário possui perfil de gestor.

### Fluxo Principal
1. O gestor acessa o painel de despesas.
2. O sistema exibe as despesas cadastradas.
3. O gestor seleciona uma despesa pendente de aprovação.
4. O gestor solicita a aprovação da despesa.
5. O sistema verifica se a despesa existe.
6. O sistema verifica se a despesa está aguardando aprovação manual.
7. O sistema atualiza o status para **Aprovada Manualmente**.
8. O sistema salva a alteração.
9. O sistema informa que a despesa foi aprovada com sucesso.

### Fluxos Alternativos
- **FA01 – Despesa não encontrada**
  - No passo 5, caso a despesa não exista, o sistema informa que o registro não foi encontrado.
- **FA02 – Status incompatível**
  - No passo 6, caso a despesa não esteja aguardando aprovação manual, o sistema impede a operação e informa que a despesa não pode ser aprovada.

### Pós-condições
- A despesa permanece cadastrada com o status **Aprovada Manualmente**.

---

## UC06 – Rejeitar Despesa

### Ator Principal
Gestor

### Descrição
Permite que o gestor rejeite uma despesa que esteja aguardando aprovação manual.

### Pré-condições
- A despesa está cadastrada no sistema.
- A despesa possui o status **Requer Aprovação Manual**.
- O usuário possui perfil de gestor.

### Fluxo Principal
1. O gestor acessa o painel de despesas.
2. O sistema exibe as despesas cadastradas.
3. O gestor seleciona uma despesa pendente de aprovação.
4. O gestor solicita a rejeição da despesa.
5. O sistema verifica se a despesa existe.
6. O sistema verifica se a despesa está aguardando aprovação manual.
7. O sistema atualiza o status para **Rejeitada**.
8. O sistema salva a alteração.
9. O sistema informa que a despesa foi rejeitada com sucesso.

### Fluxos Alternativos
- **FA01 – Despesa não encontrada**
  - No passo 5, caso a despesa não exista, o sistema informa que o registro não foi encontrado.
- **FA02 – Status incompatível**
  - No passo 6, caso a despesa não esteja aguardando aprovação manual, o sistema impede a operação e informa que a despesa não pode ser rejeitada.

### Pós-condições
- A despesa permanece cadastrada com o status **Rejeitada**.

---

## UC07 – Excluir Despesa Inferior a R$ 50,00

### Ator Principal
Gestor

### Descrição
Permite que o gestor exclua despesas cujo valor seja inferior a R$ 50,00.

### Pré-condições
- A despesa está cadastrada no sistema.
- O valor da despesa é inferior a R$ 50,00.
- O usuário possui perfil de gestor.

### Fluxo Principal
1. O gestor acessa o painel de despesas.
2. O sistema exibe as despesas cadastradas.
3. O gestor seleciona uma despesa com valor inferior a R$ 50,00.
4. O gestor solicita a exclusão da despesa.
5. O sistema verifica se a despesa existe.
6. O sistema verifica se o valor da despesa é inferior a R$ 50,00.
7. O sistema exclui a despesa do banco de dados.
8. O sistema informa que a despesa foi excluída com sucesso.

### Fluxos Alternativos
- **FA01 – Despesa não encontrada**
  - No passo 5, caso a despesa não exista, o sistema informa que o registro não foi encontrado.
- **FA02 – Valor incompatível**
  - No passo 6, caso o valor da despesa seja igual ou superior a R$ 50,00, o sistema impede a exclusão e informa que a operação não é permitida.
- **FA03 – Usuário sem permissão**
  - Caso o usuário não possua perfil de gestor, o sistema impede a operação e informa que a exclusão é permitida somente para gestores.

### Pós-condições
- A despesa é removida do banco de dados.
- Nenhuma outra despesa é alterada.

