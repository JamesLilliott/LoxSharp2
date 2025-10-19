namespace LoxParser;

public class Keywords
{
    public static Dictionary<string, TokenType> GetKeywords()
    {
        return new Dictionary<string, TokenType>()
        {
            { "IF", TokenType.If },
            { "OR", TokenType.Or },
            { "AND", TokenType.And },
            { "NIL", TokenType.Nil },
            { "FUN", TokenType.Fun },
            { "VAR", TokenType.Var },
            { "FOR", TokenType.For },
            { "ELSE", TokenType.Else },
            { "THIS", TokenType.This },
            { "TRUE", TokenType.True },
            { "CLASS", TokenType.Class },
            { "FALSE", TokenType.False },
            { "PRINT", TokenType.Print },
            { "SUPER", TokenType.Super },
            { "WHILE", TokenType.While },
            { "RETURN", TokenType.Return },
        };
    }
}