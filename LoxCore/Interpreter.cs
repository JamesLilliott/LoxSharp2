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
            BinaryExpression binary => HandleBinary(binary),
            _ => throw new NotImplementedException(expression.GetType().Name + " is not implemented")
        };
    }

    private object HandleBinary(BinaryExpression binary)
    {
        var left = Evaluate(binary.LeftExpression);
        var right = Evaluate(binary.RightExpression);

        return binary.Operator switch
        {
            TokenType.Minus => Convert.ToDouble(left) - Convert.ToDouble(right),
            TokenType.Asterisk => Convert.ToDouble(left) * Convert.ToDouble(right),
            TokenType.Slash => Convert.ToDouble(left) / Convert.ToDouble(right),
            TokenType.Greater => Convert.ToDouble(left) > Convert.ToDouble(right),
            TokenType.GreaterEqual => Convert.ToDouble(left) >= Convert.ToDouble(right),
            TokenType.Less => Convert.ToDouble(left) < Convert.ToDouble(right),
            TokenType.LessEqual => Convert.ToDouble(left) <= Convert.ToDouble(right),
            TokenType.EqualEqual => IsEqual(left, right),
            TokenType.BangEqual => !IsEqual(left, right),
            TokenType.Plus => left is string || right is string 
                ? Convert.ToString(left) + Convert.ToString(right) 
                : Convert.ToDouble(left) + Convert.ToDouble(right),
            _ => throw new NotImplementedException(binary.Operator.GetType().Name + " is not implemented")
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

    private bool IsEqual(object? left, object? right)
    {
        if (left == null && right == null)
        {
            return true;
        }

        if (left == null)
        {
            return false;
        }
        
        return left.Equals(right);
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