using System.ComponentModel.DataAnnotations;

namespace ContactListApi.ViewModels.Person;

public class SavePersonViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 a 40 caracteres")]
    public string? Name { get; set; }
}