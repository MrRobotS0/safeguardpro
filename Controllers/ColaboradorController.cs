using apisafeguardpro.Context;
using apisafeguardpro.Dtos;
using apisafeguardpro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apisafeguardpro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradorController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColaboradorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedResult<ColaboradorResponse>>> GetColaboradores([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1 || pageSize > 100)
        {
            pageSize = 20;
        }

        var query = _context.Colaboradors.AsNoTracking().OrderBy(c => c.ColaboradorCod);
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ColaboradorResponse
            {
                ColaboradorCod = c.ColaboradorCod,
                NomeColab = c.NomeColab,
                Cpf = c.Cpf,
                Telefone = c.Telefone,
                DataAdmissao = c.DataAdmissao,
                Email = c.Email,
                Ctps = c.Ctps
            })
            .ToListAsync();

        return new PagedResult<ColaboradorResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)pageSize)
        };
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ColaboradorResponse>> GetColaborador(int id)
    {
        var colaborador = await _context.Colaboradors.AsNoTracking().FirstOrDefaultAsync(c => c.ColaboradorCod == id);
        if (colaborador is null)
        {
            return NotFound();
        }

        return ToResponse(colaborador);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<ColaboradorResponse>> PostColaborador(ColaboradorRequest request)
    {
        var colaborador = new Colaborador
        {
            NomeColab = request.NomeColab,
            Cpf = request.Cpf,
            Telefone = request.Telefone,
            DataAdmissao = request.DataAdmissao,
            Email = request.Email,
            Ctps = request.Ctps
        };

        _context.Colaboradors.Add(colaborador);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return Conflict("Já existe um colaborador com esse CPF ou telefone.");
        }

        return CreatedAtAction(nameof(GetColaborador), new { id = colaborador.ColaboradorCod }, ToResponse(colaborador));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> PutColaborador(int id, ColaboradorRequest request)
    {
        var colaborador = await _context.Colaboradors.FindAsync(id);
        if (colaborador is null)
        {
            return NotFound();
        }

        colaborador.NomeColab = request.NomeColab;
        colaborador.Cpf = request.Cpf;
        colaborador.Telefone = request.Telefone;
        colaborador.DataAdmissao = request.DataAdmissao;
        colaborador.Email = request.Email;
        colaborador.Ctps = request.Ctps;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteColaborador(int id)
    {
        var colaborador = await _context.Colaboradors.FindAsync(id);
        if (colaborador is null)
        {
            return NotFound();
        }

        _context.Colaboradors.Remove(colaborador);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}/entregas")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<EntregaResponse>>> GetEntregasDoColaborador(int id)
    {
        var existe = await _context.Colaboradors.AnyAsync(c => c.ColaboradorCod == id);
        if (!existe)
        {
            return NotFound();
        }

        return await _context.Entregas
            .AsNoTracking()
            .Where(e => e.ColaboradorCod == id)
            .Select(e => new EntregaResponse
            {
                EntregaCod = e.EntregaCod,
                ColaboradorCod = e.ColaboradorCod,
                EpiCod = e.EpiCod,
                DataEntrega = e.DataEntrega,
                DataValidade = e.DataValidade
            })
            .ToListAsync();
    }

    private static ColaboradorResponse ToResponse(Colaborador c) => new()
    {
        ColaboradorCod = c.ColaboradorCod,
        NomeColab = c.NomeColab,
        Cpf = c.Cpf,
        Telefone = c.Telefone,
        DataAdmissao = c.DataAdmissao,
        Email = c.Email,
        Ctps = c.Ctps
    };
}
