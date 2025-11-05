namespace LoxParser.Expressions;

public class ExpressionStatement : IStatement
{
    public IExpression Expression { get; }

    public ExpressionStatement(IExpression expression)
    {
        Expression = expression;
    }
}