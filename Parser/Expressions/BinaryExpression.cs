namespace LoxParser.Expressions;

public class BinaryExpression : IExpression
{
    public IExpression LeftExpression { get; }
    
    public TokenType Operator { get; } // TODO: Can this be made into a smaller enum??
    
    public IExpression RightExpression { get; }

    public BinaryExpression(IExpression leftExpression, TokenType @operator, IExpression rightExpression)
    {
        LeftExpression = leftExpression;
        Operator = @operator;
        RightExpression = rightExpression;
    }
}