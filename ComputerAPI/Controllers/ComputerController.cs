using ComputerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPI.Controllers
{
    [Route("computer")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public ComputerController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }
        [HttpPost]
        public ActionResult<Comp> Post(CreateComputerDto createComputerDto)
        {
            var comp = new Comp()
            {
                Id = Guid.NewGuid(),
                Brand=createComputerDto.brand,
                Type = createComputerDto.type,
                Display=createComputerDto.display,
                Memory=createComputerDto.memory,
                CreatedTime=DateTime.Now,
                OsId=createComputerDto.OsId,


            };
            if (comp != null)
            {
                computerContext.Comps.Add(comp);
                computerContext.SaveChanges();
                return StatusCode(201, comp);
            }
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<Comp>> Get()
        {
            return Ok(await computerContext.Comps.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comp>> GetById(Guid id)
        {
            var comp = await computerContext.Comps.FirstOrDefaultAsync(s => s.Id == id);
            if (comp != null)
            {
                return Ok(comp);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult<Comp>> Put(Guid id, UpdateComputerDto updateCompDto)
        {
            var existingComp = await computerContext.Comps.FirstOrDefaultAsync(os => os.Id == id);
            if (existingComp != null)
            {
                existingComp.Brand = updateCompDto.brand;
                existingComp.Type = updateCompDto.type;
                existingComp.Memory= updateCompDto.memory;
                existingComp.Display = updateCompDto.display;
                computerContext.Comps.Update(existingComp);
                await computerContext.SaveChangesAsync();
                return Ok(existingComp);
            }
            return NotFound(new { message = "Nincs ilyen találat." });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var comp = await computerContext.Comps.FirstOrDefaultAsync(o => o.Id == id);
            if (comp != null)
            {
                computerContext.Comps.Remove(comp);
                await computerContext.SaveChangesAsync();
                return Ok(new { message = "sikeres törlés" });
            }
            return NotFound();
        }
    }
}
