using System.ComponentModel.DataAnnotations;

namespace ContactListApi.ViewModels.Contact;
public class SavePhoneContactViewModel : SaveContactsViewModel
{
    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Phone(ErrorMessage = "O telefone inválido")]
    public override string? Value { get; set; }
}
