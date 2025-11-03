using LoxParser;
using LoxParser.Expressions;

namespace Tests;

public class ParserTest
{
    [Test]
    public void TestParserReturnsLiteralFalse()
    {
        var input = new List<Token> { new Token(TokenType.False) };
        var expectedOutput = new LiteralExpression(false);
        
        var parser = new Parser(input);
        var result = parser.Primary() as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralTrue()
    {
        var input = new List<Token> { new Token(TokenType.True) };
        var expectedOutput = new LiteralExpression(true);
        
        var parser = new Parser(input);
        var result = parser.Primary() as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNil()
    {
        var input = new List<Token> { new Token(TokenType.Nil) };
        var expectedOutput = new LiteralExpression(null);
        
        var parser = new Parser(input);
        var result = parser.Primary() as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralString()
    {
        var input = new List<Token> { new Token(TokenType.String, "Hello") };
        var expectedOutput = new LiteralExpression("Hello");
        
        var parser = new Parser(input);
        var result = parser.Primary() as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNumber()
    {
        var input = new List<Token> { new Token(TokenType.Number, 567) };
        var expectedOutput = new LiteralExpression(567);
        
        var parser = new Parser(input);
        var result = parser.Primary() as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsGrouping()
    {
        var input = new List<Token> { new Token(TokenType.LeftParen), new Token(TokenType.Number, 567), new Token(TokenType.RightParen) };
        
        var expectedOutput = new GroupingExpression(new LiteralExpression(567));
        
        var parser = new Parser(input);
        var result = parser.Primary() as GroupingExpression;
        
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
        
        var parser = new Parser(input);
        var result = parser.Unary() as UnaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        var actualExpression = result.Expression as LiteralExpression;
        var expectedExpression = expectedOutput.Expression as LiteralExpression;
        Assert.AreEqual(actualExpression.Literal, expectedExpression.Literal);
    }
    
    [Test]
    public void TestParserReturnsMultiplyFactor()
    {
        var input = new List<Token> { new Token(TokenType.Number, 3), new Token(TokenType.Asterisk), new Token(TokenType.Number, 2) };
        var leftUnary = new LiteralExpression(3);
        var comparison = TokenType.Asterisk;
        var rightUnary = new LiteralExpression(2);
        var expectedOutput = new BinaryExpression(leftUnary, comparison, rightUnary);
        
        var parser = new Parser(input);
        var result = parser.Factor() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    [Test]
    public void TestParserReturnsDivisionFactor()
    {
        var input = new List<Token> { new Token(TokenType.Number, 6), new Token(TokenType.Slash), new Token(TokenType.Number, 2) };
        var leftUnary = new LiteralExpression(6);
        var comparison = TokenType.Slash;
        var rightUnary = new LiteralExpression(2);
        var expectedOutput = new BinaryExpression(leftUnary, comparison, rightUnary);
        
        var parser = new Parser(input);
        var result = parser.Factor() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    [Test]
    public void TestParserReturnsAdditionTerm()
    {
        var input = new List<Token> { new Token(TokenType.Number, 5), new Token(TokenType.Plus), new Token(TokenType.Number, 4) };
        var leftUnary = new LiteralExpression(5);
        var comparison = TokenType.Plus;
        var rightUnary = new LiteralExpression(4);
        var expectedOutput = new BinaryExpression(leftUnary, comparison, rightUnary);
        
        var parser = new Parser(input);
        var result = parser.Term() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    [Test]
    public void TestParserReturnsSubtractionTerm()
    {
        var input = new List<Token> { new Token(TokenType.Number, 10), new Token(TokenType.Minus), new Token(TokenType.Number, 5) };
        var leftUnary = new LiteralExpression(10);
        var comparison = TokenType.Minus;
        var rightUnary = new LiteralExpression(5);
        var expectedOutput = new BinaryExpression(leftUnary, comparison, rightUnary);
        
        var parser = new Parser(input);
        var result = parser.Term() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    [TestCase(5, TokenType.Greater,6)]
    [TestCase(6, TokenType.GreaterEqual,7)]
    [TestCase(7, TokenType.Less,8)]
    [TestCase(8, TokenType.LessEqual,9)]
    [Test]
    public void TestParserReturnsComparison(int leftNumber, TokenType @operator, int rightNumber)
    {
        var input = new List<Token> { new Token(TokenType.Number, leftNumber), new Token(@operator), new Token(TokenType.Number, rightNumber) };
        var expectedOutput = new BinaryExpression(new LiteralExpression(leftNumber), @operator, new LiteralExpression(rightNumber));
        
        var parser = new Parser(input);

        var result = parser.Comparison() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }

    [TestCase(10, TokenType.BangEqual,11)]
    [TestCase(11, TokenType.Equal,11)]
    [Test]
    public void TestParserReturnsEquality(int leftNumber, TokenType @operator, int rightNumber)
    {
        var input = new List<Token> { new Token(TokenType.Number, leftNumber), new Token(@operator), new Token(TokenType.Number, rightNumber) };
        var expectedOutput = new BinaryExpression(new LiteralExpression(leftNumber), @operator, new LiteralExpression(rightNumber));
        
        var parser = new Parser(input);

        var result = parser.Equality() as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
    
    // TODO: Add a set of tokens together and expect a tree of expressions
}