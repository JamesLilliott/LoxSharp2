using LoxParser.Expressions;

namespace LoxParser;

public class Parser
{
    private List<Token> _tokens;
    private int _index;

    public DataResult<List<IStatement>> Parse(List<Token> tokens)
    {
        var statements = new List<IStatement>();
        _tokens = tokens;
        _index = 0;

        while (_tokens.Count >= _index + 1 && _tokens[_index].Type != TokenType.Eof)
        {
            var statementResult = Statement();
            if (statementResult.Failed)
            {
                return new DataResult<List<IStatement>>(false, statementResult.ErrorMessage);
            }

            statements.Add(statementResult.Value);
        }
        
        if (!PopToken(out var lastToken) || lastToken.Type != TokenType.Eof)
        {
            return new DataResult<List<IStatement>>(false, "`EOF` Missing");    
        }
        
        return new DataResult<List<IStatement>>(true, statements);
    }
    
    private DataResult<IStatement> Statement()
    {
        if (PeakToken(out var nextToken) && nextToken.Type == TokenType.Print)
        {
            _index++;
            return PrintStatement();
        }
    
        return StatementExpression();
    }
    
    private DataResult<IStatement> PrintStatement()
    { 
        var expression = Expression();
        if (!PopToken(out var nextToken) || nextToken.Type != TokenType.SemiColon)
        {
            return new DataResult<IStatement>(false, "Statement must end in `;`");    
        }
        
        return new DataResult<IStatement>(true, new PrintStatement(expression));
    }
    
    private DataResult<IStatement> StatementExpression()
    { 
        var expression = Expression();
        if (!PopToken(out var nextToken) || nextToken.Type != TokenType.SemiColon)
        {
            return new DataResult<IStatement>(false, "Statement must end in `;`");    
        }
        
        return new DataResult<IStatement>(true, new ExpressionStatement(expression));
    }

    private IExpression Expression()
    {
        return Equality();
    }

    private IExpression Equality()
    {
        var expression = Comparison();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.EqualEqual || token.Type == TokenType.BangEqual)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Comparison();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;    
    }

    private IExpression Comparison()
    {
        var expression = Term();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        TokenType[] validTokenTypes = [TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual]; 
        if (validTokenTypes.Contains(token.Type))
        {
            var @operator = Consume(token).Type;
            var rightExpression = Term();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    private IExpression Term()
    {
        var expression = Factor();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.Plus || token.Type == TokenType.Minus)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Factor();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    private IExpression Factor()
    {
        var expression = Unary();
        if (_tokens.Count < _index + 1)
        {
            return expression;
        }

        var token = _tokens[_index];
        if (token.Type == TokenType.Asterisk || token.Type == TokenType.Slash)
        {
            var @operator = Consume(token).Type;
            var rightExpression = Unary();
            expression = new BinaryExpression(expression, @operator, rightExpression);
        }
        
        return expression;
    }

    private IExpression Unary()
    {
        var token = _tokens[_index]; 
        if (token.Type == TokenType.Minus || token.Type == TokenType.Bang)
        {
            return new UnaryExpression(Consume(token).Type, Unary());
        }

        return Primary();
    }

    private IExpression Primary()
    {
        var token = Consume(_tokens[_index]);

        if (token.Type == TokenType.LeftParen)
        {
            var expression = Expression();
            if (_tokens.Count < _index + 1)
            {
                throw new Exception("Missing ')'");
            }

            _index++;
            return new GroupingExpression(expression);
        }
        
        return token.Type switch
        {
            TokenType.False => new LiteralExpression(false),
            TokenType.True => new LiteralExpression(true),
            TokenType.Nil => new LiteralExpression(null),
            TokenType.String => new LiteralExpression(token.Literal),
            TokenType.Number => new LiteralExpression(token.Literal),
            _ => throw new Exception("Expected literal expression")
        };
    }

    private Token Consume(Token token)
    {
        _index++;
        return token;
    }

    private bool PeakToken(out Token? token)
    {
        token = null;
        if (_tokens.Count < _index + 1)
        {
            return false;
        }

        token =  _tokens[_index];
        return true;
    }

    private bool PopToken(out Token? token)
    {
        token = null;
        if (_tokens.Count < _index + 1)
        {
            return false;
        }

        token =  _tokens[_index];
        _index++;
        return true;
    }
}