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
        if (!result.Failed && result.Value.Count == 0)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }

    [Test]
    public void TestJunkInputReturnsFailedResult()
    {
        var result = _scanner.Scan("Frogs");
        if (result.Failed)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void TestLeftParenReturnsToken()
    {
        var result = _scanner.Scan("(");
        if (!result.Failed && result.Value.Count == 1 && result.Value.First().Type == TokenType.LeftParen)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void TestRighParenReturnsToken()
    {
        var result = _scanner.Scan(")");
        if (!result.Failed && result.Value.Count == 1 && result.Value.First().Type == TokenType.RightParen)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void TestAllTokensReturned()
    {
        var result = _scanner.Scan("()");
        if (result.Failed)
        {
            Assert.Fail();
            return;
        }

        var expectedTokens = new[] { new Token(TokenType.LeftParen), new Token(TokenType.RightParen) };

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            if (expectedTokens[i].Type != result.Value[i].Type)
            {
                Assert.Fail();
                return;
            }
        }
        
        Assert.Pass();
    }
}