using ContactListApi.Data;
using ContactListApi.Extensions;
using ContactListApi.Models;
using ContactListApi.ViewModels;
using ContactListApi.ViewModels.Person;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListApi.Controllers;

[ApiController]
[Route("v1")]
public class PersonController : ControllerBase
{
    [HttpGet("persons")]
    public async Task<IActionResult> GetAllAsync([FromServices] AppDbContext context)
    {
        try
        {
            var persons = await context.Persons
                .AsNoTracking()
                .Include(x => x.Contacts)
                .ToListAsync();

            return Ok(new ResultViewModel<IList<Person>>(persons));
        }
        catch (SystemException ex)
        {
            return StatusCode(500, new ResultViewModel<IList<Person>>((ex.InnerException.Message)));
            //return StatusCode(500, new ResultViewModel<IList<Person>>(("01EPerson - Não foi possível retornar a lista de pessoas")));
        }
    }

    [HttpGet("persons/{id:guid}")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
    {
        try
        {
            var person = await context
                .Persons
                .AsNoTracking()
                .Include(x => x.Contacts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound(new ResultViewModel<Person>("Conteúdo não encontrado"));
            }

            return Ok(new ResultViewModel<Person>(person));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultViewModel<Person>("02EPerson - Não foi possível retornar a pessoa"));
        }
    }

    [HttpPost("persons")]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonViewModel model, [FromServices] AppDbContext context)
    {
        try
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Person>(ModelState.GetErrors()));
            }

            var person = new Person { Name = model.Name! };

            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();

            return Created($"v1/persons/{person.Id}", new ResultViewModel<Person>(person));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Person>("03EPerson - Não foi possível incluir a pessoa"));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultViewModel<Person>("04EPerson - Falha interna no servidor"));
        }
    }

    [HttpPut("persons/{id:guid}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] Guid id,
        [FromBody] SavePersonViewModel model,
        [FromServices] AppDbContext context)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Person>(ModelState.GetErrors()));
            }

            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound(new ResultViewModel<Person>("Conteúdo não encontrado"));
            }

            person.Name = model.Name!;

            context.Persons.Update(person);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Person>(person));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Person>("05EPerson - Não foi possível alterar a pessoa"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Person>("06EPerson - Falha interna no servidor"));
        }
    }

    [HttpDelete("persons/{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] AppDbContext context)
    {
        try
        {
            var person = await context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound(new ResultViewModel<Person>("Conteúdo não encontrado"));
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Person>(person));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Person>("07EPerson - Não foi possível excluir a pessoa"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Person>("08EPerson - Falha interna no servidor"));
        }
    }
}