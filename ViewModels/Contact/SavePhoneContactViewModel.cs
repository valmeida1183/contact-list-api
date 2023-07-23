using System.ComponentModel.DataAnnotations;
using ContactListApi.ViewModels.Contact.Intefaces;

namespace ContactListApi.ViewModels.Contact;
public class SavePhoneContactViewModel : SavePersonContactsViewModel, ISaveContactViewModel
{
    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Phone(ErrorMessage = "O telefone inválido")]
    public string? Value { get; set; }
}
