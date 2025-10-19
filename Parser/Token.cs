namespace LoxParser;

public record Token
{
    public readonly TokenType Type;
    public object? Literal;

    public Token(TokenType type)
    {
        Type = type;
    }
    
    public Token(TokenType type, object? literal)
    {
        Type = type;
        Literal = literal;
    }
}