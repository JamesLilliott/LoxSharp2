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

            string[] twoLetterKeyWords = ["IF", "OR"];
            if (_source.Length >= i + 2 && twoLetterKeyWords.Contains(_source[i..(i+2)]))
            {
                var check = _source[i..(i+2)];
                _tokens.Add(check switch
                {
                    "IF" => new Token(TokenType.If),
                    "OR" => new Token(TokenType.Or),
                    _ => throw new ArgumentException("Invalid case")
                });
                

                i +=1;
                continue;
            }
            
            string[] threeLetterKeyWords = ["AND", "FUN", "FOR", "NIL", "VAR"];
            if (_source.Length >= i + 3 && threeLetterKeyWords.Contains(_source[i..(i+3)]))
            {
                var check = _source[i..(i+3)];
                _tokens.Add(check switch
                {
                    "AND" => new Token(TokenType.And),
                    "FUN" => new Token(TokenType.Fun),
                    "FOR" => new Token(TokenType.For),
                    "VAR" => new Token(TokenType.Var),
                    "NIL" => new Token(TokenType.Nil),
                    _ => throw new ArgumentException("Invalid case")
                });
                

                i +=2;
                continue;
            }

            string[] fourLetterKeyWords = ["ELSE", "THIS", "TRUE"];
            if (_source.Length >= i + 4 && fourLetterKeyWords.Contains(_source[i..(i + 4)]))
            {
                var check = _source[i..(i + 4)];
                _tokens.Add(check switch
                {
                    "ELSE" => new Token(TokenType.Else),
                    "THIS" => new Token(TokenType.This),
                    "TRUE" => new Token(TokenType.True),
                    _ => throw new ArgumentException("Invalid case")
                });
                

                i +=3;
                continue;
            }
            
            string[] fiveLetterKeyWords = ["CLASS", "FALSE", "PRINT", "SUPER", "WHILE"];
            if (_source.Length >= i + 5 && fiveLetterKeyWords.Contains(_source[i..(i + 5)]))
            {
                var check = _source[i..(i + 5)];
                _tokens.Add(check switch
                {
                    "CLASS" => new Token(TokenType.Class),
                    "FALSE" => new Token(TokenType.False),
                    "PRINT" => new Token(TokenType.Print),
                    "SUPER" => new Token(TokenType.Super),
                    "WHILE" => new Token(TokenType.While),
                    _ => throw new ArgumentException("Invalid case")
                });
                

                i +=4;
                continue;
            }
            
            string[] sixLetterKeyWords = ["RETURN"];
            if (_source.Length >= i + 6 && sixLetterKeyWords.Contains(_source[i..(i + 6)]))
            {
                var check = _source[i..(i + 6)];
                _tokens.Add(check switch
                {
                    "RETURN" => new Token(TokenType.Return),
                    _ => throw new ArgumentException("Invalid case")
                });
                

                i +=5;
                continue;
            }
        }
        
        return new DataResult(_tokens.Count > 0, _tokens);
    }
}