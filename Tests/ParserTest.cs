using LoxParser;
using LoxParser.Expressions;

namespace Tests;

public class ParserTest
{
    private Parser _parser;
    
    [SetUp]
    public void Setup()
    {
        _parser = new Parser();
    }
    
    [Test]
    public void TestParserReturnsLiteralFalse()
    {
        var input = new List<Token> { new Token(TokenType.False) };
        var expectedOutput = new LiteralExpression(false);
        
        var result = _parser.Parse(input) as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralTrue()
    {
        var input = new List<Token> { new Token(TokenType.True) };
        var expectedOutput = new LiteralExpression(true);
        
        var result = _parser.Parse(input) as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNil()
    {
        var input = new List<Token> { new Token(TokenType.Nil) };
        var expectedOutput = new LiteralExpression(null);
        
        var result = _parser.Parse(input) as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralString()
    {
        var input = new List<Token> { new Token(TokenType.String, "Hello") };
        var expectedOutput = new LiteralExpression("Hello");
        
        var result = _parser.Parse(input) as LiteralExpression;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNumber()
    {
        var input = new List<Token> { new Token(TokenType.Number, 567) };
        var expectedOutput = new LiteralExpression(567);
        
        var result = _parser.Parse(input) as LiteralExpression;;
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsBangUnary()
    {
        var input = new List<Token> { new Token(TokenType.Bang), new Token(TokenType.True) };
        var expectedOutput = new UnaryExpression(TokenType.Bang, new LiteralExpression(true));
        
        var result = _parser.Parse(input) as UnaryExpression;
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
        
        var result = _parser.Parse(input) as UnaryExpression;
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
        
        var result = _parser.Parse(input) as BinaryExpression;
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
        
        var result = _parser.Parse(input) as BinaryExpression;
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
        
        var result = _parser.Parse(input) as BinaryExpression;
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
        
        var result = _parser.Parse(input) as BinaryExpression;
        Assert.AreEqual(result.Operator, expectedOutput.Operator);
        
        var actualLeftExpression = result.LeftExpression as LiteralExpression;
        var expectedLeftExpression = expectedOutput.LeftExpression as LiteralExpression;
        Assert.AreEqual(actualLeftExpression.Literal, expectedLeftExpression.Literal);
        
        var actualRightExpression = result.RightExpression as LiteralExpression;
        var expectedRightExpression = expectedOutput.RightExpression as LiteralExpression;
        Assert.AreEqual(actualRightExpression.Literal, expectedRightExpression.Literal);
    }
}