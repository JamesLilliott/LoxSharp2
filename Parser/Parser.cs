using LoxParser.Expressions;

namespace LoxParser;

public class Parser
{
    private IReadOnlyCollection<Token> _tokens;

    public LiteralExpressions Parse(IReadOnlyCollection<Token> tokens)
    {
        _tokens = tokens;

        return tokens.First().Type switch
        {
            TokenType.False => new LiteralExpressions(false),
            TokenType.True => new LiteralExpressions(true),
            TokenType.Nil => new LiteralExpressions(null),
            TokenType.String => new LiteralExpressions(tokens.First().Literal),
            TokenType.Number => new LiteralExpressions(tokens.First().Literal),
            _ => throw new Exception("Expected literal expression")
        };
    }
}