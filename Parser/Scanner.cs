namespace LoxParser;

public class Scanner
{
    private string _source;
    
    public DataResult Scan(string source)
    {
        _source = source;
        if (string.IsNullOrEmpty(_source))
        {
            return new DataResult(true, Array.Empty<Token>());    
        }
        
        return new DataResult(false, Array.Empty<Token>());
    }
}