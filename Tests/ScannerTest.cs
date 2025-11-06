using LoxParser;

namespace Tests;

public class ScannerTest
{
    private Scanner _scanner;
    [SetUp]
    public void Setup()
    {
        _scanner = new Scanner();
    }
    
    [Test]
    public void TestAnEmptyScriptReturnsEOFTokens()
    {
        var result = _scanner.Scan("");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Values.Count, 1);
        Assert.AreEqual(TokenType.Eof, result.Values.First().Type);
    }
    
    [Test]
    public void TestCharacterWithoutTokenReturnsFailedResult()
    {
        var result = _scanner.Scan("VAR ~~~~");
        Assert.That(result.Failed, Is.True);
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
    [TestCase("if", TokenType.If)]
    [TestCase("IF", TokenType.If)]
    [TestCase("or", TokenType.Or)]
    [TestCase("OR", TokenType.Or)]
    [TestCase("and", TokenType.And)]
    [TestCase("AND", TokenType.And)]
    [TestCase("fun", TokenType.Fun)]
    [TestCase("FUN", TokenType.Fun)]
    [TestCase("for", TokenType.For)]
    [TestCase("FOR", TokenType.For)]
    [TestCase("nil", TokenType.Nil)]
    [TestCase("NIL", TokenType.Nil)]
    [TestCase("var", TokenType.Var)]
    [TestCase("VAR", TokenType.Var)]
    [TestCase("else", TokenType.Else)]
    [TestCase("ELSE", TokenType.Else)]
    [TestCase("this", TokenType.This)]
    [TestCase("THIS", TokenType.This)]
    [TestCase("true", TokenType.True)]
    [TestCase("TRUE", TokenType.True)]
    [TestCase("class", TokenType.Class)]
    [TestCase("CLASS", TokenType.Class)]
    [TestCase("false", TokenType.False)]
    [TestCase("FALSE", TokenType.False)]
    [TestCase("print", TokenType.Print)]
    [TestCase("PRINT", TokenType.Print)]
    [TestCase("super", TokenType.Super)]
    [TestCase("SUPER", TokenType.Super)]
    [TestCase("while", TokenType.While)]
    [TestCase("WHILE", TokenType.While)]
    [TestCase("return", TokenType.Return)]
    [TestCase("RETURN", TokenType.Return)]
    [TestCase("USERNAME", TokenType.Identifier)]
    [TestCase("userName", TokenType.Identifier)]
    [TestCase("_user_name", TokenType.Identifier)]
    public void TestTokenReturned(string input, TokenType output)
    {
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Values.First().Type, output);
    }
    
    [Test]
    [TestCase("\"\"", TokenType.String, "")]
    [TestCase("\"I\"", TokenType.String, "I")]
    [TestCase("\"I am a string\"", TokenType.String, "I am a string")]
    public void TestStringTokenReturned(string input, TokenType output, string expectedText)
    {
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(result.Values.First().Type, output);
        Assert.AreEqual(result.Values.First().Literal, expectedText);
    }
    
    [Test]
    public void TestVariableAssignment()
    {
        var input = "VAR catName = \"Monty\";";
        var expectedTokens = new List<Token>
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Equal),
            new Token(TokenType.String),
            new Token(TokenType.SemiColon)
        };
        
        var result = _scanner.Scan(input);
        Assert.That(result.Failed, Is.False);
        for (int i = 0; i < expectedTokens.Count; i++)
        {
            Assert.AreEqual(expectedTokens[i].Type, result.Values[i].Type);
        }
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
            new Token(TokenType.Identifier),
            new Token(TokenType.Eof),
        };
        
        var result = _scanner.Scan("(){},.-+;/*===!=!>=><=<\"Test\"123IF OR AND FUN FOR NIL VAR ELSE THIS TRUE CLASS FALSE PRINT SUPER WHILE RETURN userName");
        
        Assert.That(result.Failed, Is.False);
        Assert.AreEqual(expectedTokens.Length, result.Values.Count);

        for (int i = 0; i < expectedTokens.Length; i++)
        {
            Assert.AreEqual(expectedTokens[i].Type, result.Values[i].Type);
        }
    }
}