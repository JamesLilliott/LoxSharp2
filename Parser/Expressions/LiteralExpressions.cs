namespace LoxParser.Expressions;

public class LiteralExpressions
{
    public object Literal { get; set; }

    public LiteralExpressions(object literal)
    {
        Literal = literal;
    }
}