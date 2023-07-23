using System.Diagnostics.CodeAnalysis;

namespace ContactListApi.Models;

public class Person : BaseModel
{
    public required string Name { get; set; }

    public IList<Contact>? Contacts { get; set; }
}