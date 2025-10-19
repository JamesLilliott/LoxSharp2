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
                continue;
            }
            
            if (_source[i] == ')')
            {
                _tokens.Add(new Token(TokenType.RightParen));
                continue;
            }
            
            if (_source[i] == '{')
            {
                _tokens.Add(new Token(TokenType.LeftBrace));
                continue;
            }
            
            if (_source[i] == '}')
            {
                _tokens.Add(new Token(TokenType.RightBrace));
                continue;
            }
            
            if (_source[i] == ',')
            {
                _tokens.Add(new Token(TokenType.Comma));
                continue;
            }
            
            if (_source[i] == '.')
            {
                _tokens.Add(new Token(TokenType.Period));
                continue;
            }
            
            if (_source[i] == '-')
            {
                _tokens.Add(new Token(TokenType.Minus));
                continue;
            }
            
            if (_source[i] == '+')
            {
                _tokens.Add(new Token(TokenType.Plus));
                continue;
            }
            
            if (_source[i] == ';')
            {
                _tokens.Add(new Token(TokenType.SemiColon));
                continue;
            }
            
            if (_source[i] == '/')
            {
                _tokens.Add(new Token(TokenType.Slash));
                continue;
            }
            
            if (_source[i] == '*')
            {
                _tokens.Add(new Token(TokenType.Asterisk));
                continue;
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
                continue;
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
                continue;
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
                continue;
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
                continue;
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
                continue;
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
                continue;
            }
            
            if (IsAlpha(_source[i]))
            {
                var keyWords = Keywords.GetKeywords();
                var endOfWord = false;
                var parsedWord = _source[i].ToString();

                while (!endOfWord)
                {
                    i++;
                    if (i < _source.Length && (IsAlpha(_source[i]) || IsDigit(_source[i])))
                    {
                        parsedWord += _source[i].ToString();
                    }
                    else
                    {
                        endOfWord = true;
                    }
                }

                parsedWord = parsedWord.ToUpper();
                if (keyWords.TryGetValue(parsedWord, out var tokenType))
                {
                    _tokens.Add(new Token(tokenType));
                }
                else
                {
                    _tokens.Add(new Token(TokenType.Identifier, parsedWord));
                }

                continue;
            }

            if (char.IsWhiteSpace(_source[i]))
            {
                continue;
            }
            
            return new DataResult(false, _tokens);
        }
        
        return new DataResult(_tokens.Count > 0, _tokens);
    }

    private bool IsAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
               c == '_';
    }
    
    private bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }
}