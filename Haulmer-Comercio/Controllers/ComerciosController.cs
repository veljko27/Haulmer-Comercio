
using Haulmer_Comercio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Haulmer_Comercio.Controllers
{
    [ApiController]
    [Route("api/Comercios")]
    public class ComerciosController : ControllerBase
    {
        public ComerciosController(AplicationDbContext context)
        {
            Context = context;
        }
        public AplicationDbContext Context { get; }
       
            [HttpPost("Ingresar Comercio")]
            public async Task<ActionResult> Post([FromBody] Comercios comercios)
            {
                Context.Add(comercios);
                await Context.SaveChangesAsync();
                return Ok();
            }
        [HttpGet("Listar Comercios")]
        public async Task<ActionResult<List<Comercios>>> Get()
        {
            return await Context.Comercios.Include(x => x.ventas).ToListAsync();

        }

        [HttpGet("Consultar Puntos/{id}")]
        public async Task<ActionResult<Comercios>> Get(int id)
        {
            var existe = await Context.Comercios.AnyAsync(x => x.id == id);
            if (!existe)
            {
                return BadRequest($"no existe el comercio:{id}");
            }
            Comercios comercio= await Context.Comercios.FirstOrDefaultAsync(x => x.id == id);

            return Ok($"los puntos acumulados del comercio son:{comercio.puntos}");

        }
    }

    
}
