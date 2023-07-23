using System.ComponentModel.DataAnnotations;
using ContactListApi.ViewModels.Contact.Intefaces;

namespace ContactListApi.ViewModels.Contact;
public class SaveWhatsAppContactViewModel : SavePersonContactsViewModel, ISaveContactViewModel
{
    [Required(ErrorMessage = "O whatsApp é obrigatório")]
    [Phone(ErrorMessage = "O whatsApp inválido")]
    public string? Value { get; set; }
}