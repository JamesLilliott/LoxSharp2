namespace LoxParser;

public class DataResult
{
    private bool _success;
    private IReadOnlyCollection<Token> _tokens;

    public bool Failed => !_success;
    public IReadOnlyCollection<Token> Value => _tokens;

    public DataResult(bool success, IReadOnlyCollection<Token> tokens)
    {
        _success = success;
        _tokens = tokens;
    }
}