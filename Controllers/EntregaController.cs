using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisafeguardpro.Context;
using apisafeguardpro.Models;
using Microsoft.AspNetCore.Authorization;

namespace apisafeguardpro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna os cadastros existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get do cadastro os dados retornados serão
        ///     {
        ///         "entrega_cod": "codigo da entrega";
        ///         "colaborador_cod": "Codigo do colaborador";
        ///         "data_validade": "data de validade da entrega";
        ///         "data_entrega": "data da entrega";
        ///         "epi_cod": "codigo de epi";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Entrega
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            return await _context.Entregas.ToListAsync();
        }
        /// <summary>
        /// Retorna os cadastros existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get do cadastro os dados retornados serão
        ///     {
        ///         "entrega_cod": "codigo da entrega";
        ///         "colaborador_cod": "Codigo do colaborador";
        ///         "data_validade": "data de validade da entrega";
        ///         "data_entrega": "data da entrega";
        ///         "epi_cod": "codigo de epi";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Entrega/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
          if (_context.Entregas == null)
          {
              return NotFound();
          }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }
        /// <summary>
        /// Altera os dados existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put do cadastro os dados alterados serão
        ///     {
        ///         "entrega_cod": "codigo da entrega";
        ///         "colaborador_cod": "Codigo do colaborador";
        ///         "data_validade": "data de validade da entrega";
        ///         "data_entrega": "data da entrega";
        ///         "epi_cod": "codigo de epi";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso a alteração dos dados</response>
        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.EntregaCod)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        /// <summary>
        /// Cadastra os dados informados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post do cadastro, os dados cadastrados serão
        ///     {
        ///         "entrega_cod": "codigo da entrega";
        ///         "colaborador_cod": "Codigo do colaborador";
        ///         "data_validade": "data de validade da entrega";
        ///         "data_entrega": "data da entrega";
        ///         "epi_cod": "codigo de epi";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao cadastrar dos dados</response>
        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
          if (_context.Entregas == null)
          {
              return Problem("Entity set 'AppDbContext.Entregas'  is null.");
          }
            _context.Entregas.Add(entrega);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EntregaExists(entrega.EntregaCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEntrega", new { id = entrega.EntregaCod }, entrega);
        }
        /// <summary>
        /// Deleta os cadastros existentes
        /// </summary>
        /// <response code="200">Sucesso ao deletar os dados</response>
        // DELETE: api/Entrega/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.EntregaCod == id)).GetValueOrDefault();
        }
    }
}
