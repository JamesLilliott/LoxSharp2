using LoxParser.Expressions;

namespace LoxParser;

public class Interpreter
{
    public Object Evaluate(IExpression expression)
    {
        switch (expression)
        {
            case LiteralExpression literal:
                return HandleLiteral(literal);
        }
        
        throw new NotImplementedException(expression.GetType().Name + " is not implemented");
    }

    private object HandleLiteral(LiteralExpression expression)
    {
        return expression.Literal;
    }
}