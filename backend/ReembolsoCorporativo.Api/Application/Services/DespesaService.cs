
using ReembolsoCorporativo.Api.Contracts.Dtos;
using ReembolsoCorporativo.Api.Domain.Entities;
using ReembolsoCorporativo.Api.Infrastructure.Persistence;

namespace ReembolsoCorporativo.Api.Application.Services;

public class DespesaService : IDespesaService
{
    private readonly AppDbContext _context;

    public DespesaService(AppDbContext context)
    {
        _context = context;
    }

    public DespesaResponseDto Criar(CreateDespesaRequestDto request)
    {
        ValidarDuplicidade(request);

        var despesa = new Despesa(
            request.Tipo,
            request.Valor,
            request.Data!.Value
        );

        _context.Despesas.Add(despesa);
        _context.SaveChanges();

        return ConverterParaDto(despesa);
    }

    public IEnumerable<DespesaResponseDto> Listar()
    {
        return _context.Despesas
            .OrderByDescending(d => d.DataCriacao)
            .Select(d => new DespesaResponseDto
            {
                Id = d.Id,
                Tipo = d.Tipo,
                Valor = d.Valor,
                Data = d.Data,
                Status = d.Status
            })
            .ToList();
    }

    public bool Aprovar(Guid id)
    {
        var despesa = BuscarDespesa(id);

        if (despesa is null)
            return false;

        despesa.Aprovar();

        _context.SaveChanges();

        return true;
    }

    public bool Rejeitar(Guid id)
    {
        var despesa = BuscarDespesa(id);

        if (despesa is null)
            return false;

        despesa.Rejeitar();

        _context.SaveChanges();

        return true;
    }

    public bool Excluir(Guid id)
    {
        var despesa = BuscarDespesa(id);

        if (despesa is null)
            return false;

        if (despesa.Valor >= 50)
        {
            throw new InvalidOperationException(
                "Somente despesas com valor inferior a R$ 50,00 podem ser excluídas.");
        }

        _context.Despesas.Remove(despesa);
        _context.SaveChanges();

        return true;
    }

    private Despesa? BuscarDespesa(Guid id)
    {
        return _context.Despesas
            .FirstOrDefault(d => d.Id == id);
    }

    private static DespesaResponseDto ConverterParaDto(Despesa despesa)
    {
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

        var existeDuplicada = _context.Despesas.Any(d =>
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
