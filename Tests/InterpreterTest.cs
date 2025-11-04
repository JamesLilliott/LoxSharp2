using LoxParser;
using LoxParser.Expressions;

namespace Tests;

public class InterpreterTest
{
    [TestCase(5)]
    [TestCase("Hello")]
    [TestCase(false)]
    [TestCase(true)]
    [Test]
    public void TestInterpreterHandlesLiteral(object testCase)
    {
        var input = new LiteralExpression(testCase);
        var expectedOutput = testCase;
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(result, expectedOutput);
    }
    
    [TestCase(5)]
    [TestCase("Hello")]
    [TestCase(false)]
    [TestCase(true)]
    [Test]
    public void TestInterpreterHandlesLiteralGrouping(object testCase)
    {
        var innerExpression = new LiteralExpression(testCase);
        var input = new GroupingExpression(innerExpression);
        var expectedOutput = testCase;
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(result, expectedOutput);
    }
    
    [TestCase(TokenType.Minus, 5, -5)]
    [TestCase(TokenType.Bang, true, false)]
    [TestCase(TokenType.Bang, null, true)]
    [Test]
    public void TestInterpreterHandlesUnaryGrouping(TokenType unaryOperator, object? testCase, object expectedOutput)
    {
        var innerLiteral = new LiteralExpression(testCase);
        var innerUnary = new UnaryExpression(unaryOperator, innerLiteral);
        var input = new GroupingExpression(innerUnary);
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(expectedOutput, result);
    }
    
    [TestCase(TokenType.Minus, 5, -5)]
    [TestCase(TokenType.Bang, true, false)]
    [TestCase(TokenType.Bang, null, true)]
    [Test]
    public void TestInterpreterHandlesLiteralUnary(TokenType unaryOperator, object? testCase, object expectedOutput)
    {
        var innerExpression = new LiteralExpression(testCase);
        var input = new UnaryExpression(unaryOperator, innerExpression);
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(result, expectedOutput);
    }
    
    [TestCase(TokenType.Minus, 5, -5)]
    [TestCase(TokenType.Bang, true, false)]
    [TestCase(TokenType.Bang, null, true)]
    [Test]
    public void TestInterpreterHandlesGroupingUnary(TokenType unaryOperator, object? testCase, object expectedOutput)
    {
        var innerLiteral = new LiteralExpression(testCase);
        var innerGrouping = new GroupingExpression(innerLiteral);
        var input = new UnaryExpression(unaryOperator, innerGrouping);
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(result, expectedOutput);
    }
    
    [TestCase(5, TokenType.Minus, 3, 2)]
    [TestCase(6, TokenType.Slash, 2, 3)]
    [TestCase(2, TokenType.Asterisk, 2, 4)]
    [TestCase(3, TokenType.Plus, 2, 5)]
    [TestCase("Hello", TokenType.Plus, " World", "Hello World")]
    [TestCase(1, TokenType.Greater, 2, false)]
    [TestCase(2, TokenType.GreaterEqual, 3, false)]
    [TestCase(3, TokenType.Less, 4, true)]
    [TestCase(4, TokenType.LessEqual, 5, true)]
    [TestCase(4, TokenType.EqualEqual, 4, true)]
    [TestCase(4, TokenType.EqualEqual, 3, false)]
    [TestCase(4, TokenType.EqualEqual, null, false)]
    [TestCase(null, TokenType.EqualEqual, null, true)]
    [TestCase(4, TokenType.BangEqual, 5, true)]
    [TestCase(null, TokenType.BangEqual, 5, true)]
    [TestCase(5, TokenType.BangEqual, 5, false)]
    [TestCase(null, TokenType.BangEqual, null, false)]
    [Test]
    public void TestInterpreterHandlesLiteralBinary(object? left, TokenType @operator, object? right, object expectedOutput)
    {
        var leftExpression = new LiteralExpression(left);
        var rightExpression = new LiteralExpression(right);
        var input = new BinaryExpression(leftExpression, @operator, rightExpression);
        
        var result = new Interpreter().Evaluate(input);
        
        Assert.AreEqual(result, expectedOutput);
    }
}