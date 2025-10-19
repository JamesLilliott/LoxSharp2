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
            _tokens.Add(new Token(TokenType.Eof));
            return new DataResult(true, _tokens);    
        }

        for (int i = 0; i < _source.Length; i++)
        {
            switch (_source[i])
            {
                case '(':
                    _tokens.Add(new Token(TokenType.LeftParen));
                    continue;
                case ')':
                    _tokens.Add(new Token(TokenType.RightParen));
                    continue;
                case '{':
                    _tokens.Add(new Token(TokenType.LeftBrace));
                    continue;
                case '}':
                    _tokens.Add(new Token(TokenType.RightBrace));
                    continue;
                case ',':
                    _tokens.Add(new Token(TokenType.Comma));
                    continue;
                case '.':
                    _tokens.Add(new Token(TokenType.Period));
                    continue;
                case '-':
                    _tokens.Add(new Token(TokenType.Minus));
                    continue;
                case '+':
                    _tokens.Add(new Token(TokenType.Plus));
                    continue;
                case ';':
                    _tokens.Add(new Token(TokenType.SemiColon));
                    continue;
                case '/':
                    _tokens.Add(new Token(TokenType.Slash));
                    continue;
                case '*':
                    _tokens.Add(new Token(TokenType.Asterisk));
                    continue;
                case '=':
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
                case '!':
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
                case '>':
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
                case '<':
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
                case '"':
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

        _tokens.Add(new Token(TokenType.Eof));
        return new DataResult(true, _tokens);
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