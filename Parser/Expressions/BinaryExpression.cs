namespace LoxParser.Expressions;

public class BinaryExpression : Expression
{
    public Expression LeftExpression { get; }
    
    public TokenType Operator { get; } // TODO: Can this be made into a smaller enum??
    
    public Expression RightExpression { get; }

    public BinaryExpression(Expression leftExpression, TokenType @operator, Expression rightExpression)
    {
        LeftExpression = leftExpression;
        Operator = @operator;
        RightExpression = rightExpression;
    }
}