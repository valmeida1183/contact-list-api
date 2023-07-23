using System.ComponentModel.DataAnnotations;

namespace ContactListApi.ViewModels.Contact;
public class SaveEmailContactViewModel : SaveContactsViewModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email é inválido")]
    public override string? Value { get; set; }
}