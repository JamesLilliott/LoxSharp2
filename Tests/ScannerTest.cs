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
    [TestCase("=", TokenType.Equal)]
    [TestCase("==", TokenType.EqualEqual)]
    [TestCase("!", TokenType.Bang)]
    [TestCase("!=", TokenType.BangEqual)]
    [TestCase(">", TokenType.Greater)]
    [TestCase(">=", TokenType.GreaterEqual)]
    [TestCase("<", TokenType.Less)]
    [TestCase("<=", TokenType.LessEqual)]
    [TestCase("\"I am a string\"", TokenType.String)]
    [TestCase("\"\"", TokenType.String)]
    [TestCase("123", TokenType.Number)]
    [TestCase("IF", TokenType.If)]
    [TestCase("OR", TokenType.Or)]
    [TestCase("AND", TokenType.And)]
    [TestCase("FUN", TokenType.Fun)]
    [TestCase("FOR", TokenType.For)]
    [TestCase("NIL", TokenType.Nil)]
    [TestCase("VAR", TokenType.Var)]
    [TestCase("ELSE", TokenType.Else)]
    [TestCase("THIS", TokenType.This)]
    [TestCase("TRUE", TokenType.True)]
    [TestCase("CLASS", TokenType.Class)]
    [TestCase("FALSE", TokenType.False)]
    [TestCase("PRINT", TokenType.Print)]
    [TestCase("SUPER", TokenType.Super)]
    [TestCase("WHILE", TokenType.While)]
    [TestCase("RETURN", TokenType.Return)]
    public void TestTokenReturned(string input, TokenType output)
    {
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, output);
    }
    
    [Test]
    [TestCase("\"\"", TokenType.String, "")]
    [TestCase("\"I\"", TokenType.String, "I")]
    [TestCase("\"I am a string\"", TokenType.String, "I am a string")]
    public void TestStringTokenReturned(string input, TokenType output, string expectedText)
    {
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Value.First().Type, output);
        Assert.AreEqual(result.Value.First().Literal, expectedText);
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
            new Token(TokenType.EqualEqual),
            new Token(TokenType.Equal),
            new Token(TokenType.BangEqual),
            new Token(TokenType.Bang),
            new Token(TokenType.GreaterEqual),
            new Token(TokenType.Greater),
            new Token(TokenType.LessEqual),
            new Token(TokenType.Less),
            new Token(TokenType.String),
            new Token(TokenType.Number),
            new Token(TokenType.If),
            new Token(TokenType.Or),
            new Token(TokenType.And),
            new Token(TokenType.Fun),
            new Token(TokenType.For),
            new Token(TokenType.Nil),
            new Token(TokenType.Var),
            new Token(TokenType.Else),
            new Token(TokenType.This),
            new Token(TokenType.True),
            new Token(TokenType.Class),
            new Token(TokenType.False),
            new Token(TokenType.Print),
            new Token(TokenType.Super),
            new Token(TokenType.While),
            new Token(TokenType.Return),
        };
        
        var result = _scanner.Scan("(){},.-+;/*===!=!>=><=<\"Test\"123IFORANDFUNFORNILVARELSETHISTRUECLASSFALSEPRINTSUPERWHILERETURN");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(expectedTokens.Length, result.Value.Count);

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            Assert.AreEqual(expectedTokens[i].Type, result.Value[i].Type);
        }
    }
}