namespace LoxParser.Expressions;

public class LiteralExpression : IExpression
{
    public object Literal { get; }

    public LiteralExpression(object literal)
    {
        Literal = literal;
    }
}