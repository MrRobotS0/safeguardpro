using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisafeguardpro.Context;
using apisafeguardpro.Models;

namespace apisafeguardpro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EpiController(AppDbContext context)
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
        ///         "epi_cod": "codigo de epi";
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome": "nome do EPI";
        ///         "forma_adequada": "forma adequada para usar o EPI";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Epi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis()
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            return await _context.Epis.ToListAsync();
        }
        /// <summary>
        /// Retorna os cadastros existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Get do cadastro, os dados retornados por Id serão
        ///     {
        ///         "epi_cod": "codigo de epi";
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome": "nome do EPI";
        ///         "forma_adequada": "forma adequada para usar o EPI";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Epi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
          if (_context.Epis == null)
          {
              return NotFound();
          }
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound();
            }

            return epi;
        }
        /// <summary>
        /// Altera os dados existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put do cadastro, os dados alterados serão
        ///     {
        ///         "epi_cod": "codigo de epi";
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome": "nome do EPI";
        ///         "forma_adequada": "forma adequada para usar o EPI";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao alterar os dados</response>
        // PUT: api/Epi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpi(int id, Epi epi)
        {
            if (id != epi.EpiCod)
            {
                return BadRequest();
            }

            _context.Entry(epi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
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
        /// Cadastra de novos dados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post do cadastro, os dados cadastrados serão
        ///     {
        ///         "epi_cod": "codigo de epi";
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome": "nome do EPI";
        ///         "forma_adequada": "forma adequada para usar o EPI";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao cadastrar dos dados</response>
        // POST: api/Epi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Epi>> PostEpi(Epi epi)
        {
          if (_context.Epis == null)
          {
              return Problem("Entity set 'AppDbContext.Epis'  is null.");
          }
            _context.Epis.Add(epi);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EpiExists(epi.EpiCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEpi", new { id = epi.EpiCod }, epi);
        }
        /// <summary>
        /// Deleta os cadastros existentes
        /// </summary>
        /// <response code="200">Sucesso ao deletar os dados</response>
        // DELETE: api/Epi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpiExists(int id)
        {
            return (_context.Epis?.Any(e => e.EpiCod == id)).GetValueOrDefault();
        }
    }
}
