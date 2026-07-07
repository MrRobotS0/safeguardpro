using apisafeguardpro.Context;
using apisafeguardpro.Dtos;
using apisafeguardpro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apisafeguardpro.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EpiController : ControllerBase
{
    private readonly AppDbContext _context;

    public EpiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PagedResult<EpiResponse>>> GetEpis([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1 || pageSize > 100)
        {
            pageSize = 20;
        }

        var query = _context.Epis.AsNoTracking().OrderBy(e => e.EpiCod);
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EpiResponse
            {
                EpiCod = e.EpiCod,
                Nome = e.Nome,
                FormaAdequada = e.FormaAdequada
            })
            .ToListAsync();

        return new PagedResult<EpiResponse>
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
    public async Task<ActionResult<EpiResponse>> GetEpi(int id)
    {
        var epi = await _context.Epis.AsNoTracking().FirstOrDefaultAsync(e => e.EpiCod == id);
        if (epi is null)
        {
            return NotFound();
        }

        return ToResponse(epi);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<EpiResponse>> PostEpi(EpiRequest request)
    {
        var epi = new Epi
        {
            Nome = request.Nome,
            FormaAdequada = request.FormaAdequada
        };

        _context.Epis.Add(epi);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEpi), new { id = epi.EpiCod }, ToResponse(epi));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> PutEpi(int id, EpiRequest request)
    {
        var epi = await _context.Epis.FindAsync(id);
        if (epi is null)
        {
            return NotFound();
        }

        epi.Nome = request.Nome;
        epi.FormaAdequada = request.FormaAdequada;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteEpi(int id)
    {
        var epi = await _context.Epis.FindAsync(id);
        if (epi is null)
        {
            return NotFound();
        }

        _context.Epis.Remove(epi);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static EpiResponse ToResponse(Epi e) => new()
    {
        EpiCod = e.EpiCod,
        Nome = e.Nome,
        FormaAdequada = e.FormaAdequada
    };
}
