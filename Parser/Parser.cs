using LoxParser.Expressions;

namespace LoxParser;

public class Parser
{
    private List<Token> _tokens;
    private int _index;

    public Expression Parse(List<Token> tokens)
    {
        _tokens = tokens;
        _index = 0;

        if (tokens.Count == 3)
        {
            return Factor();
        }
        else if (tokens.Count == 1)
        {
            return Primary();
        }
        else
        {
            return Unary();
        }

        throw new Exception("Unsupported input");
    }

    private Expression Factor()
    {
        var expression = Unary();
        var token = _tokens[_index];
        if (token.Type == TokenType.Asterisk || token.Type == TokenType.Slash)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Unary();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    private Expression Unary()
    {
        var token = _tokens[_index]; 
        if (token.Type == TokenType.Minus || token.Type == TokenType.Bang)
        {
            return new UnaryExpression(Consume(token).Type, Unary());
        }

        return Primary();
    }

    private LiteralExpression Primary()
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