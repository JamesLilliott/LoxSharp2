namespace LoxParser.Expressions;

public class GroupingExpression : IExpression
{ 
    public IExpression Expression { get; }

    public GroupingExpression(IExpression expression)
    {
        Expression = expression;
    }
}
