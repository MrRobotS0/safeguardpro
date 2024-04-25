using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apisafeguardpro.Context;
using apisafeguardpro.Models;

namespace apisafeguardpro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
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
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome_colab": "Nome do colaborador";
        ///         "cpf": "cpf do colaborador";
        ///         "telefone": "telefone do colaborador";
        ///         "data_admissao": "data de admissao do colaborador";
        ///         "email": "e-mail do colaborador;
        ///         "ctps": "ctps do colaborador";
        ///      }
        ///      
        /// </remarks>
        /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Colaborador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            return await _context.Colaboradors.ToListAsync();
        }
        /// <summary>
        /// Retorna dados do cadastro do id informado
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     Get do cadastro os dados retornados serão
        ///     {
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome_colab": "Nome do colaborador";
        ///         "cpf": "cpf do colaborador";
        ///         "telefone": "telefone do colaborador";
        ///         "data_admissao": "data de admissao do colaborador";
        ///         "email": "e-mail do colaborador;
        ///         "ctps": "ctps do colaborador";
        ///      }
        ///      
        /// </remarks>
        /// /// <response code="200">Sucesso ao retorno dos dados</response>
        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     put do cadastro, voce deve informar os valores conforme no exemplo abaixo:
        ///     {
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome_colab": "Nome do colaborador";
        ///         "cpf": "cpf do colaborador";
        ///         "telefone": "telefone do colaborador";
        ///         "data_admissao": "data de admissao do colaborador";
        ///         "email": "e-mail do colaborador;
        ///         "ctps": "ctps do colaborador";
        ///      }
        ///      
        /// </remarks>
        /// /// <response code="200">Sucesso ao alterar dos dados</response>
        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.ColaboradorCod)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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
        /// Cadastrar os dados informados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post do cadastro, voce deve informar os valores conforme no exemplo abaixo:
        ///     {
        ///         "colaborador_cod": "codigo do colaborador";
        ///         "nome_colab": "Nome do colaborador";
        ///         "cpf": "cpf do colaborador";
        ///         "telefone": "telefone do colaborador";
        ///         "data_admissao": "data de admissao do colaborador";
        ///         "email": "e-mail do colaborador;
        ///         "ctps": "ctps do colaborador";
        ///      }
        ///      
        /// </remarks>
        /// /// <response code="200">Sucesso ao cadastrar dos dados</response>
        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
          if (_context.Colaboradors == null)
          {
              return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
          }
            _context.Colaboradors.Add(colaborador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColaboradorExists(colaborador.ColaboradorCod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColaborador", new { id = colaborador.ColaboradorCod }, colaborador);
        }
        /// <summary>
        /// Deletar os dados do id informado
        /// </summary>
        /// /// <response code="200">Sucesso ao deletar dos dados</response>
        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.ColaboradorCod == id)).GetValueOrDefault();
        }
    }
}
