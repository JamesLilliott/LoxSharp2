using LoxParser.Expressions;

namespace LoxParser;

public class Interpreter
{
    public object Evaluate(IExpression expression)
    {
        return expression switch
        {
            LiteralExpression literal => HandleLiteral(literal),
            GroupingExpression grouping => HandleGrouping(grouping),
            UnaryExpression unary => HandleUnary(unary),
            _ => throw new NotImplementedException(expression.GetType().Name + " is not implemented")
        };
    }

    private object HandleUnary(UnaryExpression unary)
    {
        var right = Evaluate(unary.Expression);

        return unary.Operator switch
        {
            TokenType.Bang => !IsTruthy(right),
            TokenType.Minus => -Convert.ToDouble(right),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private object HandleGrouping(GroupingExpression grouping)
    {
        return Evaluate(grouping.Expression);
    }

    private object HandleLiteral(LiteralExpression expression)
    {
        return expression.Literal;
    }
    
    private bool IsTruthy(object? right)
    {
        if (right == null)
        {
            return false;
        }

        if (right is bool)
        {
            return (bool)right;
        }
        
        return true;
    }
}