namespace LoxParser;

public class DataResult<T>
{
    private readonly bool _success;

    public bool Failed => !_success;
    public T Value { get; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DataResult(bool success, T value)
    {
        _success = success;
        Value = value;
    }

    public DataResult(bool success, string errorMessage)
    {
        _success = success;
        ErrorMessage = errorMessage;
    }
}