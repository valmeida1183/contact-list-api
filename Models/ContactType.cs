using ContactListApi.Enums;

namespace ContactListApi.Models;

public class ContactType
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public IList<Contact>? Contacts { get; set; }
}