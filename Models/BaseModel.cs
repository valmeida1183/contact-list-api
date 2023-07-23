namespace ContactListApi.Models;

public abstract class BaseModel
{
    public Guid Id { get; private set; }

    protected BaseModel()
    {
        Id = Guid.NewGuid();
    }
}