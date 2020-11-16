//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/hecto/RiderProjects/BRAQ/BRAQ_grammar\BRAQParser.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="BRAQParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IBRAQParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] BRAQParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] BRAQParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] BRAQParser.StmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.if_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf_stmt([NotNull] BRAQParser.If_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.var_stmt_base"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar_stmt_base([NotNull] BRAQParser.Var_stmt_baseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.print_stmt_base"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrint_stmt_base([NotNull] BRAQParser.Print_stmt_baseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.assign_stmt_base"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssign_stmt_base([NotNull] BRAQParser.Assign_stmt_baseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.read_stmt_base"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRead_stmt_base([NotNull] BRAQParser.Read_stmt_baseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] BRAQParser.ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroup([NotNull] BRAQParser.GroupContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall([NotNull] BRAQParser.CallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.call_or_literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCall_or_literal([NotNull] BRAQParser.Call_or_literalContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.arg_list"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArg_list([NotNull] BRAQParser.Arg_listContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] BRAQParser.LiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BRAQParser.var_node"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar_node([NotNull] BRAQParser.Var_nodeContext context);
}
