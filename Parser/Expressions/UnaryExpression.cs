namespace LoxParser.Expressions;

public class UnaryExpression :  Expression
{
    public TokenType Operator { get; } // TODO: Can this be made into a smaller enum??
    public Expression Expression { get; }

    public UnaryExpression(TokenType @operator, Expression expression)
    {
        Operator = @operator;
        Expression = expression;
    }
}