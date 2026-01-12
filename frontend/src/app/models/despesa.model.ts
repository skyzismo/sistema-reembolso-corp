export enum TipoDespesa {
  Alimentacao = 1,
  Transporte = 2,
  Hospedagem = 3,
  Outros = 4
}

export enum StatusDespesa {
  AprovadaAutomaticamente = 1,
  RequerAprovacaoManual = 2
}

export interface Despesa {
  id?: string;
  tipo: TipoDespesa;
  valor: number;
  data: string;
  status?: StatusDespesa;
}
