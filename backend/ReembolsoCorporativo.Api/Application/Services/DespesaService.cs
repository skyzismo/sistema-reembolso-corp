using ReembolsoCorporativo.Api.Contracts.Dtos;
using ReembolsoCorporativo.Api.Domain.Entities;

namespace ReembolsoCorporativo.Api.Application.Services;

public class DespesaService : IDespesaService
{
    // Simula persistência temporária (em memória)
    private static readonly List<Despesa> _despesas = new();

    public DespesaResponseDto Criar(CreateDespesaRequestDto request)
    {
        ValidarDuplicidade(request);

        var despesa = new Despesa(
            request.Tipo,
            request.Valor,
            request.Data
        );

        _despesas.Add(despesa);

        return new DespesaResponseDto
        {
            Id = despesa.Id,
            Tipo = despesa.Tipo,
            Valor = despesa.Valor,
            Data = despesa.Data,
            Status = despesa.Status
        };
    }

    private void ValidarDuplicidade(CreateDespesaRequestDto request)
    {
        var limite = DateTime.Now.AddMinutes(-2);
    
        var existeDuplicada = _despesas.Any(d =>
            d.Tipo == request.Tipo &&
            d.Valor == request.Valor &&
            d.DataCriacao >= limite
        );
    
        if (existeDuplicada)
        {
            throw new InvalidOperationException(
                "Já existe uma despesa com o mesmo tipo e valor cadastrada nos últimos 2 minutos."
            );
        }
    }

}
