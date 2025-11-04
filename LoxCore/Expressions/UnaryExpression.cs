namespace LoxParser.Expressions;

public class UnaryExpression :  IExpression
{
    public TokenType Operator { get; } // TODO: Can this be made into a smaller enum??
    public IExpression Expression { get; }

    public UnaryExpression(TokenType @operator, IExpression expression)
    {
        Operator = @operator;
        Expression = expression;
    }
}