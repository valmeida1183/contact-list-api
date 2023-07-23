using ContactListApi.Enums;

namespace ContactListApi.Models;

public class Contact : BaseModel
{
    public required string Value { get; set; }
    public required Guid PersonId { get; set; }
    public required int ContactTypeId { get; set; }

    public Person? Person { get; set; }
    public ContactType? Type { get; set; }
}