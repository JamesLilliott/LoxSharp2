namespace LoxParser.Expressions;

public class LiteralExpression : Expression
{
    public object Literal { get; }

    public LiteralExpression(object literal)
    {
        Literal = literal;
    }
}