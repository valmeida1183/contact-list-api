using ContactListApi.Data;
using ContactListApi.Models;
using ContactListApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListApi.Controllers;

[ApiController]
[Route("v1")]
public class ContactTypeController : ControllerBase
{
    [HttpGet("contactTypes")]
    public async Task<IActionResult> GetAllAsync([FromServices] AppDbContext context)
    {
        try
        {
            var contactTypes = await context.ContactTypes
                .AsNoTracking()
                .ToListAsync();

            return Ok(new ResultViewModel<IList<ContactType>>(contactTypes));
        }
        catch (SystemException)
        {
            return StatusCode(500, new ResultViewModel<IList<Person>>(("01EContactType - Não foi possível retornar a lista de tipos de contato")));
        }
    }
}