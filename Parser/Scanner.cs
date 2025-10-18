namespace LoxParser;

public class Scanner
{
    private string _source;
    private List<Token> _tokens;
    
    public DataResult Scan(string source)
    {
        _source = source;
        _tokens = new List<Token>();

        if (string.IsNullOrEmpty(_source))
        {
            return new DataResult(true, _tokens);    
        }

        for (int i = 0; i < _source.Length; i++)
        {
            if (_source[i] == '(')
            {
                _tokens.Add(new Token(TokenType.LeftParen));
            }
            
            if (_source[i] == ')')
            {
                _tokens.Add(new Token(TokenType.RightParen));
            }
            
            if (_source[i] == '{')
            {
                _tokens.Add(new Token(TokenType.LeftBrace));
            }
            
            if (_source[i] == '}')
            {
                _tokens.Add(new Token(TokenType.RightBrace));
            }
        }
        
        return new DataResult(_tokens.Count > 0, _tokens);
    }
}