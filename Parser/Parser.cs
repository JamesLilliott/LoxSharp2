using LoxParser.Expressions;

namespace LoxParser;

public class Parser
{
    private List<Token> _tokens;
    private int _index = 0;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
    }

    public IExpression Expression()
    {
        return Equality();
    }

    public IExpression Equality()
    {
        var expression = Comparison();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.Equal || token.Type == TokenType.BangEqual)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Comparison();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;    
    }

    public IExpression Comparison()
    {
        var expression = Term();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        TokenType[] validTokenTypes = [TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual]; 
        if (validTokenTypes.Contains(token.Type))
        {
            var @operator = Consume(token).Type;
            var rightExpression = Term();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    public IExpression Term()
    {
        var expression = Factor();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.Plus || token.Type == TokenType.Minus)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Factor();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    public IExpression Factor()
    {
        var expression = Unary();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.Asterisk || token.Type == TokenType.Slash)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Unary();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    public IExpression Unary()
    {
        var token = _tokens[_index]; 
        if (token.Type == TokenType.Minus || token.Type == TokenType.Bang)
        {
            return new UnaryExpression(Consume(token).Type, Unary());
        }

        return Primary();
    }

    public IExpression Primary()
    {
        var token = Consume(_tokens[_index]);

        if (token.Type == TokenType.LeftParen)
        {
            var expression = Expression();
            if (_tokens.Count < _index + 1)
            {
                throw new Exception("Missing ')'");
            }

            _index++;
            return new GroupingExpression(expression);
        }
        
        return token.Type switch
        {
            TokenType.False => new LiteralExpression(false),
            TokenType.True => new LiteralExpression(true),
            TokenType.Nil => new LiteralExpression(null),
            TokenType.String => new LiteralExpression(token.Literal),
            TokenType.Number => new LiteralExpression(token.Literal),
            _ => throw new Exception("Expected literal expression")
        };
    }

    private Token Consume(Token token)
    {
        _index++;
        return token;
    }
}