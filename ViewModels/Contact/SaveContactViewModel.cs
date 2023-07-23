using System.ComponentModel.DataAnnotations;
using ContactListApi.Enums;
using ContactListApi.ViewModels.Contact.Intefaces;

namespace ContactListApi.ViewModels.Contact;
public abstract class SavePersonContactsViewModel
{
    [Required(ErrorMessage = "O Id da pessoa é obrigatório")]
    public Guid? PersonId { get; set; }

    [Required(ErrorMessage = "O Id do tipo é obrigatório")]
    public int? ContactTypeId { get; set; }
}