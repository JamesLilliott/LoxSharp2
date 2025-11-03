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
    
    public Expression Comparison()
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
    
    public Expression Term()
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

    public Expression Factor()
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

    public Expression Unary()
    {
        var token = _tokens[_index]; 
        if (token.Type == TokenType.Minus || token.Type == TokenType.Bang)
        {
            return new UnaryExpression(Consume(token).Type, Unary());
        }

        return Primary();
    }

    public LiteralExpression Primary()
    {
        var token = Consume(_tokens[_index]);
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