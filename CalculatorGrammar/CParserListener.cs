//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/hecto/RiderProjects/BRAQ/CalculatorGrammar\CParser.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface ICParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] CParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] CParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmt([NotNull] CParser.StmtContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmt([NotNull] CParser.StmtContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] CParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] CParser.ExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGroup([NotNull] CParser.GroupContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGroup([NotNull] CParser.GroupContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] CParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] CParser.LiteralContext context);
}
