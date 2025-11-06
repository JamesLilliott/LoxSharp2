namespace LoxParser;

public class DataResult<T>
{
    private readonly bool _success;

    public bool Failed => !_success;
    public List<T> Values { get; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DataResult(bool success, List<T> valueses)
    {
        _success = success;
        Values = valueses;
    }

    public DataResult(bool success, string errorMessage)
    {
        _success = success;
        ErrorMessage = errorMessage;
    }
}