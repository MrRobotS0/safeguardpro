using apisafeguardpro.Context;
using apisafeguardpro.Dtos;
using apisafeguardpro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apisafeguardpro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntregaController : ControllerBase
{
    private readonly AppDbContext _context;

    public EntregaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedResult<EntregaResponse>>> GetEntregas([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1 || pageSize > 100)
        {
            pageSize = 20;
        }

        var query = _context.Entregas.AsNoTracking().OrderBy(e => e.EntregaCod);
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EntregaResponse
            {
                EntregaCod = e.EntregaCod,
                ColaboradorCod = e.ColaboradorCod,
                EpiCod = e.EpiCod,
                DataEntrega = e.DataEntrega,
                DataValidade = e.DataValidade
            })
            .ToListAsync();

        return new PagedResult<EntregaResponse>
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
    public async Task<ActionResult<EntregaResponse>> GetEntrega(int id)
    {
        var entrega = await _context.Entregas.AsNoTracking().FirstOrDefaultAsync(e => e.EntregaCod == id);
        if (entrega is null)
        {
            return NotFound();
        }

        return ToResponse(entrega);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<EntregaResponse>> PostEntrega(EntregaRequest request)
    {
        var colaboradorExiste = await _context.Colaboradors.AnyAsync(c => c.ColaboradorCod == request.ColaboradorCod);
        var epiExiste = await _context.Epis.AnyAsync(e => e.EpiCod == request.EpiCod);

        if (!colaboradorExiste || !epiExiste)
        {
            return BadRequest("Colaborador ou EPI informado não existe.");
        }

        var entrega = new Entrega
        {
            ColaboradorCod = request.ColaboradorCod,
            EpiCod = request.EpiCod,
            DataEntrega = request.DataEntrega,
            DataValidade = request.DataValidade
        };

        _context.Entregas.Add(entrega);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEntrega), new { id = entrega.EntregaCod }, ToResponse(entrega));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> PutEntrega(int id, EntregaRequest request)
    {
        var entrega = await _context.Entregas.FindAsync(id);
        if (entrega is null)
        {
            return NotFound();
        }

        var colaboradorExiste = await _context.Colaboradors.AnyAsync(c => c.ColaboradorCod == request.ColaboradorCod);
        var epiExiste = await _context.Epis.AnyAsync(e => e.EpiCod == request.EpiCod);

        if (!colaboradorExiste || !epiExiste)
        {
            return BadRequest("Colaborador ou EPI informado não existe.");
        }

        entrega.ColaboradorCod = request.ColaboradorCod;
        entrega.EpiCod = request.EpiCod;
        entrega.DataEntrega = request.DataEntrega;
        entrega.DataValidade = request.DataValidade;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteEntrega(int id)
    {
        var entrega = await _context.Entregas.FindAsync(id);
        if (entrega is null)
        {
            return NotFound();
        }

        _context.Entregas.Remove(entrega);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static EntregaResponse ToResponse(Entrega e) => new()
    {
        EntregaCod = e.EntregaCod,
        ColaboradorCod = e.ColaboradorCod,
        EpiCod = e.EpiCod,
        DataEntrega = e.DataEntrega,
        DataValidade = e.DataValidade
    };
}
