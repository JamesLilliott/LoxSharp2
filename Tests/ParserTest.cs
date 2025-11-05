using LoxParser;
using LoxParser.Expressions;

namespace Tests;

public class ParserTest
{
    [TestCase(TokenType.False, false)]
    [TestCase(TokenType.True, true)]
    [TestCase(TokenType.Nil, null)]
    [Test]
    public void TestParserReturnsLiteral(TokenType tokenType, object? expected)
    {
        var input = new List<Token> { new Token(tokenType) };
        var expectedOutput = new LiteralExpression(expected);
        
        var result = new Parser().Parse(input) as LiteralExpression;
        
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [TestCase(TokenType.String, "Hello", "Hello")]
    [TestCase(TokenType.Number, 567, 567)]
    [Test]
    public void TestParserReturnsLiteralValue(TokenType tokenType, object value, object expected)
    {
        var input = new List<Token> { new Token(tokenType, value) };
        var expectedOutput = new LiteralExpression(expected);
        
        var result = new Parser().Parse(input) as LiteralExpression;
        
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsGrouping()
    {
        var input = new List<Token> { new Token(TokenType.LeftParen), new Token(TokenType.Number, 567), new Token(TokenType.RightParen) };
        
        var expectedOutput = new GroupingExpression(new LiteralExpression(567));
        
        var result = new Parser().Parse(input) as GroupingExpression;
        
        var actualExpression = result.Expression as LiteralExpression;
        var expectedExpression = expectedOutput.Expression as LiteralExpression;
        Assert.AreEqual(actualExpression.Literal, expectedExpression.Literal);
    }

    [Test]
    public void TestParserReturnsBangUnary()
    {
        var input = new List<Token> { new Token(TokenType.Bang), new Token(TokenType.True) };
        var expectedOutput = new UnaryExpression(TokenType.Bang, new LiteralExpression(true));
        
        var parser = new Parser(input);
        var result = parser.Unary() as UnaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        var actualExpression = result.Expression as LiteralExpression;
        var expectedExpression = expectedOutput.Expression as LiteralExpression;
        Assert.AreEqual(actualExpression.Literal, expectedExpression.Literal);
    }
    
    [Test]
    public void TestParserReturnsMinusUnary()
    {
        var input = new List<Token> { new Token(TokenType.Minus), new Token(TokenType.Number, 567) };
        var expectedOutput = new UnaryExpression(TokenType.Minus, new LiteralExpression(567));
        
        var result = new Parser().Parse(input) as UnaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        var actualExpression = result.Expression as LiteralExpression;
        var expectedExpression = expectedOutput.Expression as LiteralExpression;
        Assert.AreEqual(actualExpression.Literal, expectedExpression.Literal);
    }
    
    [TestCase(3, TokenType.Asterisk, 2)]
    [TestCase(6, TokenType.Slash, 2)]
    [TestCase(5, TokenType.Plus, 4)]
    [TestCase(10, TokenType.Minus, 5)]
    [TestCase(10, TokenType.BangEqual,11)]
    [TestCase(11, TokenType.EqualEqual,11)]
    [TestCase(5, TokenType.Greater,6)]
    [TestCase(6, TokenType.GreaterEqual,7)]
    [TestCase(7, TokenType.Less,8)]
    [TestCase(8, TokenType.LessEqual,9)]
    [Test]
    public void TestParserReturnsBinary(int leftNumber, TokenType @operator, int rightNumber)
    {
        var input = new List<Token> { new Token(TokenType.Number, leftNumber), new Token(@operator), new Token(TokenType.Number, rightNumber) };
        var expectedOutput = new BinaryExpression(new LiteralExpression(leftNumber), @operator, new LiteralExpression(rightNumber));
        
        var result = new Parser().Parse(input) as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    // TODO: Refactor tests to all end in SemiColon & EOF Token
    // TODO: Refactor tests to have parse output list of Statements

    [Test]
    public void TestParserReturnsExpression()
    {
        // 4 * ((-2 + 3) / 2) == 5 - 3
        var input = new List<Token>
        {
            new Token(TokenType.Number, 4),
            new Token(TokenType.Asterisk),
            new Token(TokenType.LeftParen),
            new Token(TokenType.LeftParen),
            new Token(TokenType.Minus),
            new Token(TokenType.Number, 2),
            new Token(TokenType.Plus),
            new Token(TokenType.Number, 3),
            new Token(TokenType.RightParen),
            new Token(TokenType.Slash),
            new Token(TokenType.Number, 2),
            new Token(TokenType.RightParen),
            new Token(TokenType.EqualEqual),
            new Token(TokenType.Number, 5),
            new Token(TokenType.Minus),
            new Token(TokenType.Number, 3),
        };

        var expectedOutput = 
            new BinaryExpression(
                new BinaryExpression(
                    new LiteralExpression(4),
                    TokenType.Asterisk,
                    new GroupingExpression(
                        new BinaryExpression(
                            new GroupingExpression(
                                new BinaryExpression(
                                    new UnaryExpression(
                                        TokenType.Minus,
                                        new LiteralExpression(2)
                                    ),
                                    TokenType.Plus,
                                    new LiteralExpression(2)
                                )
                            ),
                            TokenType.Slash,
                            new LiteralExpression(2)
                        )
                    )
                ), 
                TokenType.EqualEqual,
                new BinaryExpression(
                    new LiteralExpression(5),
                    TokenType.Minus,
                    new LiteralExpression(3)
                )
            );
        
        var parser = new Parser();

        var result = parser.Parse(input) as BinaryExpression;
        
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        // TODO: Test/Assert Left and Right of the expression 
    }
}