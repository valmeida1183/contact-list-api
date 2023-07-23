using System.ComponentModel.DataAnnotations;

namespace ContactListApi.ViewModels.Contact;
public class SaveWhatsAppContactViewModel : SaveContactsViewModel
{
    [Required(ErrorMessage = "O whatsApp é obrigatório")]
    [Phone(ErrorMessage = "O whatsApp inválido")]
    public override string? Value { get; set; }
}