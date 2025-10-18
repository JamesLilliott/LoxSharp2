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
            
            if (_source[i] == ',')
            {
                _tokens.Add(new Token(TokenType.Comma));
            }
            
            if (_source[i] == '.')
            {
                _tokens.Add(new Token(TokenType.Period));
            }
            
            if (_source[i] == '-')
            {
                _tokens.Add(new Token(TokenType.Minus));
            }
            
            if (_source[i] == '+')
            {
                _tokens.Add(new Token(TokenType.Plus));
            }
            
            if (_source[i] == ';')
            {
                _tokens.Add(new Token(TokenType.SemiColon));
            }
            
            if (_source[i] == '/')
            {
                _tokens.Add(new Token(TokenType.Slash));
            }
            
            if (_source[i] == '*')
            {
                _tokens.Add(new Token(TokenType.Asterisk));
            }
            
            if (_source[i] == '=')
            {
                if (_source.Length >= i + 2 && _source[i + 1] == '=')
                {
                    i++;
                    _tokens.Add(new Token(TokenType.EqualEqual));    
                }
                else
                {
                    _tokens.Add(new Token(TokenType.Equal));
                }
            }
            
            if (_source[i] == '!')
            {
                if (_source.Length >= i + 2 && _source[i + 1] == '=')
                {
                    i++;
                    _tokens.Add(new Token(TokenType.BangEqual));    
                }
                else
                {
                    _tokens.Add(new Token(TokenType.Bang));
                }
            }
            
            if (_source[i] == '>')
            {
                if (_source.Length >= i + 2 && _source[i + 1] == '=')
                {
                    i++;
                    _tokens.Add(new Token(TokenType.GreaterEqual));    
                }
                else
                {
                    _tokens.Add(new Token(TokenType.Greater));
                }
            }
            
            if (_source[i] == '<')
            {
                if (_source.Length >= i + 2 && _source[i + 1] == '=')
                {
                    i++;
                    _tokens.Add(new Token(TokenType.LessEqual));    
                }
                else
                {
                    _tokens.Add(new Token(TokenType.Less));
                }
            }
            
            if (_source[i] == '"')
            {
                var endOfStringFound = false;
                string parsedString = string.Empty;
                while (!endOfStringFound)
                {
                    if (_source.Length >= i + 2)
                    {
                        i++;
                        if (_source[i] == '"')
                        {
                            endOfStringFound = true;
                            _tokens.Add(new Token(TokenType.String, parsedString));
                        }
                        else
                        {
                            parsedString += _source[i];
                        }
                    }
                    else
                    {
                        return new DataResult(false, _tokens);
                    }
                }
            }
            
            if (char.IsNumber(_source[i]))
            {
                var endOfNumberFound = false;
                string parsedNumber = string.Empty;
                while (!endOfNumberFound)
                {
                    if (_source.Length >= i + 2 && char.IsNumber(_source[i + 1]))
                    {
                        i++;
                        parsedNumber += _source[i];
                    }
                    else
                    {
                        endOfNumberFound = true;
                        _tokens.Add(new Token(TokenType.Number, parsedNumber));
                    }
                }
            }
        }
        
        return new DataResult(_tokens.Count > 0, _tokens);
    }
}