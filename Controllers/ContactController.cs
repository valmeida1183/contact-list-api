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

    [HttpPost("contactTypes")]
    public async Task<IActionResult> PostAsync([FromBody] ISaveContactViewModel model, [FromServices] AppDbContext context)
    {
        try
        {
            if (model is SaveEmailContactViewModel)
            {
                TryValidateModel(model, nameof(model));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Contact>(ModelState.GetErrors()));
            }

            var contact = new Contact
            {
                ContactTypeId = Convert.ToInt32(ContactTypeEnum.Phone),
                PersonId = model.PersonId!.Value,
                Value = model.Value!
            };

            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();

            return Created($"v1/contacts/{contact.Id}", new ResultViewModel<Contact>(contact));
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
}