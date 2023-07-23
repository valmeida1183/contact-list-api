namespace ContactListApi.ViewModels;

// Serve para padronizar todos os retornos da nossa Api, desta forma o front sempre sabe como os dados serão retornados.
public class ResultViewModel<T>
{
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; } = new();

    public ResultViewModel(T data) => Data = data;

    public ResultViewModel(List<string> errors) => Errors = errors;

    public ResultViewModel(string error) => Errors.Add(error);

    public ResultViewModel(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }
}
