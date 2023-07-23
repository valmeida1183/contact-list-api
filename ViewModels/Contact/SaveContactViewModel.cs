using System.ComponentModel.DataAnnotations;

namespace ContactListApi.ViewModels.Contact;
public abstract class SaveContactsViewModel
{
    [Required(ErrorMessage = "O Id da pessoa é obrigatório")]
    public Guid? PersonId { get; set; }

    [Required(ErrorMessage = "O Id do tipo é obrigatório")]
    public int? ContactTypeId { get; set; }

    public abstract string? Value { get; set; }
}