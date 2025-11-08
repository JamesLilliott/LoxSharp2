namespace LoxParser.Expressions;

public class PrintStatement : IStatement
{
    public IExpression Expression { get; set; }

    public PrintStatement(IExpression expression)
    {
        Expression = expression;
    }
}
