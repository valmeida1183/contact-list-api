using System.ComponentModel.DataAnnotations;
using ContactListApi.ViewModels.Contact.Intefaces;

namespace ContactListApi.ViewModels.Contact;
public class SaveEmailContactViewModel : SavePersonContactsViewModel, ISaveContactViewModel
{
    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email é inválido")]
    public string? Value { get; set; }
}