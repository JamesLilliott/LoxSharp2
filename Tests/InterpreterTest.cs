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
}