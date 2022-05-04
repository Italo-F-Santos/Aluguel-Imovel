using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AluguelImovel.Models;
using System;
using System.Threading.Tasks;

namespace AluguelImovel.Controllers
{

    [ApiController]
    [Route(template: "v1")]

    public class ImovelController : ControllerBase
    {


        [HttpGet]
        [Route(template: "imoveis")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var imovel = await context.Imovel.AsNoTracking().ToListAsync();
            return Ok(imovel);

        }

        [HttpGet]
        [Route(template: "imoveis/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context,
                                                                        [FromRoute] int id)
        {
            var imovel = await context.Imovel.
                AsNoTracking().
                FirstOrDefaultAsync(x => x.Id == id);

            return imovel == null ?
                NotFound()
                : Ok(imovel);

        }

        [HttpPost]
        [Route(template: "imoveis")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
                                                    [FromBody] CriarImovelViewModel model)
         {
            if (!ModelState.IsValid)
                BadRequest();

            var imovel = new Imovel
            {
                Cep = model.Cep,
                Descricao = model.Descricao,
                
            };

            try
            {
                await context.Imovel.AddAsync(imovel);
                await context.SaveChangesAsync();
                return Created($"v1/imoveis/{imovel.Id}", imovel);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }
            [HttpPut(template: "imoveis/{Id}")]

            public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
                                                    [FromBody] CriarImovelViewModel model,
                                                    [FromRoute] int id)
            {
                if (!ModelState.IsValid)
                    BadRequest();

                var imovel = await context.Imovel.
                            FirstOrDefaultAsync(x => x.Id == id);

                if (imovel == null)
                    return NotFound();

                try
                {
                    imovel.Cep = model.Cep;
                    imovel.Descricao   = model.Descricao;

                    context.Imovel.Update(imovel);
                    await context.SaveChangesAsync();
                    return Ok(imovel);
                }
                catch (Exception)
                {
                    return BadRequest();

                }
            }

            [HttpDelete(template:"imoveis/{id}")]

            public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, 
                                                [FromRoute] int id)

            {

                var imovel =  await context.Imovel.
                            FirstOrDefaultAsync(x => x.Id == id);

                try
                {
                    context.Imovel.Remove(imovel);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }



        }
    } 


