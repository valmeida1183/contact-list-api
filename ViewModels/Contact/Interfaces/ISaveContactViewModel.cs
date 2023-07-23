namespace ContactListApi.ViewModels.Contact.Intefaces;

public interface ISaveContactViewModel
{
    string? Value { get; set; }
    Guid? PersonId { get; set; }
    int? ContactTypeId { get; set; }
}