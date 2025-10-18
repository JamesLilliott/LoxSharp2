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
    public void TestLeftParenReturnsToken()
    {
        var result = _scanner.Scan("(");
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, TokenType.LeftParen);
    }
    
    [Test]
    public void TestRightParenReturnsToken()
    {
        var result = _scanner.Scan(")");
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, TokenType.RightParen);
    }
    
    [Test]
    public void TestLeftBraceReturnsToken()
    {
        var result = _scanner.Scan("{");
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, TokenType.LeftBrace);
    }
    
    [Test]
    public void TestRightBraceReturnsToken()
    {
        var result = _scanner.Scan("}");
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, TokenType.RightBrace);
    }
    
    [Test]
    public void TestAllTokensReturned()
    {
        var expectedTokens = new[] { new Token(TokenType.LeftParen), new Token(TokenType.RightParen), new Token(TokenType.LeftBrace), new Token(TokenType.RightBrace) };
        
        var result = _scanner.Scan("(){}");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(expectedTokens.Length, result.Value.Count);

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            Assert.AreEqual(expectedTokens[i].Type, result.Value[i].Type);
        }
    }
}