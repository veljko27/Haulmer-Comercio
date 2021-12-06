
using Haulmer_Comercio.Entidades;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Haulmer_Comercio.Controllers
{
    [ApiController]
    [Route("api/Haulmer")]
    public class HaulmerController : ControllerBase
    {
        private readonly IDataProtector dataProtector;
        public HaulmerController(AplicationDbContext context, IDataProtectionProvider dataProtectionProvider)
        {
            Context = context;
            dataProtector = dataProtectionProvider.CreateProtector("llave");
        }

        public AplicationDbContext Context { get; }
       
        [HttpPost("Venta")]
        public async Task<ActionResult> Realizar_Venta(Ventas ventas)
        {
            var textoSifrado = dataProtector.Protect(ventas.codigoSeguridad);
            ventas.codigoSeguridad = textoSifrado;
            var existe = await Context.Comercios.AnyAsync(x => x.id == ventas.ComercioId);
            if (!existe)
            {
                return BadRequest($"no existe el comercio:{ventas.ComercioId}");
            }
            Comercios comercios = await Context.Comercios.Include(x => x.ventas).FirstOrDefaultAsync(x => x.id == ventas.ComercioId);
            int puntos = comercios.puntos;
            puntos = puntos + 10;
            comercios.puntos = puntos;
            Context.Update(comercios);
            await Context.SaveChangesAsync();
            Context.Add(ventas);
            await Context.SaveChangesAsync();
            return Ok($"se realizo con exitos la venta:{ventas.id}");
        }

        [HttpPost("Anular")]
        public async Task<ActionResult> Anular_Venta(Ventas ventas)
        {
           
            var existe = await Context.Comercios.AnyAsync(x => x.id == ventas.ComercioId);
            if (!existe)
            {
                return BadRequest($"no existe el comercio:{ventas.ComercioId}");
            }
            var existeventa=await Context.Ventas.AnyAsync(x => x.id == ventas.id);

            if (!existeventa)
            {
                return BadRequest($"no existe la venta:{ventas.id}");
            }

            Ventas venta = await Context.Ventas.Include(x => x.Comercio).FirstOrDefaultAsync(x => x.id == ventas.id);
            
            var textodesifrado = dataProtector.Unprotect(venta.codigoSeguridad);
            if (textodesifrado !=ventas.codigoSeguridad)
            {
                return BadRequest($" codigo de seguridad incorrecto:{ventas.codigoSeguridad}");
            }

            Comercios comercios = await Context.Comercios.Include(x => x.ventas).FirstOrDefaultAsync(x => x.id == ventas.ComercioId);
            
            int puntos = comercios.puntos;
            if (puntos >= 10)
            {
                puntos = puntos - 10;
                comercios.puntos = puntos;
                Context.Update(comercios);
                await Context.SaveChangesAsync();
            }
            Context.Remove(venta);
            await Context.SaveChangesAsync();
            return Ok($"se realizo con exitos la anulacion:{venta.id}");
        }





    }
}





