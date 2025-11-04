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
}