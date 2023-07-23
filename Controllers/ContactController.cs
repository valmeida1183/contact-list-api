using ContactListApi.Data;
using ContactListApi.Enums;
using ContactListApi.Extensions;
using ContactListApi.Models;
using ContactListApi.ViewModels;
using ContactListApi.ViewModels.Contact;
using ContactListApi.ViewModels.Contact.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListApi.Controllers;

[ApiController]
[Route("v1")]
public class ContactController : ControllerBase
{

    [HttpGet("contacts")]
    public async Task<IActionResult> GetAllAsync([FromServices] AppDbContext context)
    {
        try
        {
            var contacts = await context.Contacts
                .AsNoTracking()
                .ToListAsync();

            return Ok(new ResultViewModel<IList<Contact>>(contacts));
        }
        catch (SystemException)
        {
            return StatusCode(500, new ResultViewModel<IList<Contact>>(("01EContact - Não foi possível retornar a lista de contatos")));
        }
    }

    [HttpGet("contacts/{id:guid}")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
    {
        try
        {
            var contact = await context
                .Contacts
                .AsNoTracking()
                .Include(x => x.Type)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
            {
                return NotFound(new ResultViewModel<Contact>("Conteúdo não encontrado"));
            }

            return Ok(new ResultViewModel<Contact>(contact));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultViewModel<Person>("02EContact - Não foi possível retornar o contato"));
        }
    }

    [HttpPost("contacts/phone")]
    public async Task<IActionResult> PostAsync([FromBody] SavePhoneContactViewModel model, [FromServices] AppDbContext context)
    {
        return await CreateContact(model, Convert.ToInt32(ContactTypeEnum.Phone), context);
    }

    [HttpPost("contacts/email")]
    public async Task<IActionResult> PostAsync([FromBody] SaveEmailContactViewModel model, [FromServices] AppDbContext context)
    {
        return await CreateContact(model, Convert.ToInt32(ContactTypeEnum.Email), context);
    }

    [HttpPost("contacts/whatsapp")]
    public async Task<IActionResult> PostAsync([FromBody] SaveWhatsAppContactViewModel model, [FromServices] AppDbContext context)
    {
        return await CreateContact(model, Convert.ToInt32(ContactTypeEnum.WhatsApp), context);
    }

    [HttpPut("contacts/phone/{id:guid}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] Guid id,
        [FromBody] SavePhoneContactViewModel model,
        [FromServices] AppDbContext context)
    {
        return await UpdateContact(id, model, context);
    }

    [HttpPut("contacts/email/{id:guid}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] Guid id,
        [FromBody] SaveEmailContactViewModel model,
        [FromServices] AppDbContext context)
    {
        return await UpdateContact(id, model, context);
    }

    [HttpPut("contacts/whatsapp/{id:guid}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] Guid id,
        [FromBody] SaveWhatsAppContactViewModel model,
        [FromServices] AppDbContext context)
    {
        return await UpdateContact(id, model, context);
    }

    [HttpDelete("contacts/{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] AppDbContext context)
    {
        try
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
            {
                return NotFound(new ResultViewModel<Contact>("Conteúdo não encontrado"));
            }

            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Contact>(contact));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Contact>("07EContact - Não foi possível excluir o contato"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Contact>("08EContact - Falha interna no servidor"));
        }
    }

    private async Task<IActionResult> CreateContact(SaveContactsViewModel model, int contactTypeId, AppDbContext context)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Contact>(ModelState.GetErrors()));
            }

            var contact = new Contact
            {
                ContactTypeId = contactTypeId,
                PersonId = model.PersonId!.Value,
                Value = model.Value!
            };

            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();

            return Created($"v1/contacts/{contact.Id}", new ResultViewModel<Contact>(contact));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Contact>("03EContact - Não foi possível incluir o contato"));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultViewModel<Contact>("04EContact - Falha interna no servidor"));
        }
    }

    private async Task<IActionResult> UpdateContact(Guid id, SaveContactsViewModel model, AppDbContext context)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Contact>(ModelState.GetErrors()));
            }

            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
            {
                return NotFound(new ResultViewModel<Contact>("Conteúdo não encontrado"));
            }

            contact.Value = model.Value!;

            context.Contacts.Update(contact);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<Contact>(contact));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Contact>("05EContact - Não foi possível alterar o contato"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Contact>("06EContact - Falha interna no servidor"));
        }
    }
}