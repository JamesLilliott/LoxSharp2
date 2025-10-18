namespace LoxParser;

public record Token(TokenType Type)
{
    public readonly TokenType Type = Type;
}