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
}