namespace LoxParser;

public record Token
{
    public readonly TokenType Type;
    public string? Literal;

    public Token(TokenType type)
    {
        Type = type;
    }
    
    public Token(TokenType type, string? literal)
    {
        Type = type;
        Literal = literal;
    }
}