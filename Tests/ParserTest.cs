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
        var input = new[] { new Token(TokenType.False) };
        var expectedOutput = new LiteralExpressions(false);
        
        var result = _parser.Parse(input);
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralTrue()
    {
        var input = new[] { new Token(TokenType.True) };
        var expectedOutput = new LiteralExpressions(true);
        
        var result = _parser.Parse(input);
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNil()
    {
        var input = new[] { new Token(TokenType.Nil) };
        var expectedOutput = new LiteralExpressions(null);
        
        var result = _parser.Parse(input);
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralString()
    {
        var input = new[] { new Token(TokenType.String, "Hello") };
        var expectedOutput = new LiteralExpressions("Hello");
        
        var result = _parser.Parse(input);
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
    
    [Test]
    public void TestParserReturnsLiteralNumber()
    {
        var input = new[] { new Token(TokenType.Number, 567) };
        var expectedOutput = new LiteralExpressions(567);
        
        var result = _parser.Parse(input);
        Assert.AreEqual(result.Literal, expectedOutput.Literal);
    }
}