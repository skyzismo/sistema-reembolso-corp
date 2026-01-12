using ReembolsoCorporativo.Api.Domain.Enums;

namespace ReembolsoCorporativo.Api.Domain.Entities;

public class Despesa
{
    public Guid Id { get; private set; }
    public TipoDespesa Tipo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public StatusDespesa Status { get; private set; }

    protected Despesa() { }

    public Despesa(TipoDespesa tipo, decimal valor, DateTime data)
    {
        Id = Guid.NewGuid();
        Tipo = tipo;
        Valor = valor;
        Data = data;
        DataCriacao = DateTime.Now;

        DefinirStatus();
    }

    private void DefinirStatus()
    {
        if (Tipo == TipoDespesa.Alimentacao && Valor > 50)
        {
            Status = StatusDespesa.RequerAprovacaoManual;
            return;
        }

        Status = StatusDespesa.AprovadaAutomaticamente;
    }
}
