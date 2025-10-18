namespace LoxParser;

public class DataResult
{
    private bool _success;
    private List<Token> _tokens;

    public bool Failed => !_success;
    public List<Token> Value => _tokens;

    public DataResult(bool success, List<Token> tokens)
    {
        _success = success;
        _tokens = tokens;
    }
}