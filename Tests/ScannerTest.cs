using LoxParser;

namespace Tests;

public class Tests
{
    private Scanner _scanner;
    [SetUp]
    public void Setup()
    {
        _scanner = new Scanner();
    }
    
    [Test]
    public void TestAnEmptyScriptReturnsEmptyTokensResult()
    {
        var result = _scanner.Scan("");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.Count, 0);
    }

    [Test]
    public void TestJunkInputReturnsFailedResult()
    {
        var result = _scanner.Scan("Frogs");
        Assert.That(result.Failed, Is.True);
        Assert.AreEqual(result.Value.Count, 0);
    }
    
    [Test]
    [TestCase("(", TokenType.LeftParen)]
    [TestCase(")", TokenType.RightParen)]
    [TestCase("{", TokenType.LeftBrace)]
    [TestCase("}", TokenType.RightBrace)]
    [TestCase(",", TokenType.Comma)]
    [TestCase(".", TokenType.Period)]
    [TestCase("-", TokenType.Minus)]
    [TestCase("+", TokenType.Plus)]
    [TestCase(";", TokenType.SemiColon)]
    [TestCase("/", TokenType.Slash)]
    [TestCase("*", TokenType.Asterisk)]
    public void TestSingleCharacterTokenReturned(string input, TokenType output)
    {
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, output);
    }
    
    [Test]
    public void TestAllTokensReturned()
    {
        var expectedTokens = new[]
        {
            new Token(TokenType.LeftParen),
            new Token(TokenType.RightParen),
            new Token(TokenType.LeftBrace),
            new Token(TokenType.RightBrace),
            new Token(TokenType.Comma),
            new Token(TokenType.Period),
            new Token(TokenType.Minus),
            new Token(TokenType.Plus),
            new Token(TokenType.SemiColon),
            new Token(TokenType.Slash),
            new Token(TokenType.Asterisk),
        };
        
        var result = _scanner.Scan("(){},.-+;/*");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(expectedTokens.Length, result.Value.Count);

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            Assert.AreEqual(expectedTokens[i].Type, result.Value[i].Type);
        }
    }
}