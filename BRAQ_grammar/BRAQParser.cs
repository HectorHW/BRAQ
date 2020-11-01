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

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class BRAQParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		EQUALS=1, PLUS=2, MINUS=3, STAR=4, SLASH=5, MODULUS=6, NUMBER=7, LBRACKET=8, 
		RBRACKET=9, SEMICOLON=10, NEWLINE=11, SPACE=12, VAR=13, PRINT=14, READ=15, 
		STRING=16, ID=17;
	public const int
		RULE_program = 0, RULE_stmt = 1, RULE_var_stmt_base = 2, RULE_print_stmt_base = 3, 
		RULE_assign_stmt_base = 4, RULE_read_stmt_base = 5, RULE_expr = 6, RULE_group = 7, 
		RULE_literal = 8, RULE_var_node = 9;
	public static readonly string[] ruleNames = {
		"program", "stmt", "var_stmt_base", "print_stmt_base", "assign_stmt_base", 
		"read_stmt_base", "expr", "group", "literal", "var_node"
	};

	private static readonly string[] _LiteralNames = {
		null, "'='", "'+'", "'-'", "'*'", "'/'", "'%'", null, "'('", "')'", "';'", 
		null, null, "'var'", "'print'", "'read'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "EQUALS", "PLUS", "MINUS", "STAR", "SLASH", "MODULUS", "NUMBER", 
		"LBRACKET", "RBRACKET", "SEMICOLON", "NEWLINE", "SPACE", "VAR", "PRINT", 
		"READ", "STRING", "ID"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "BRAQParser.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static BRAQParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public BRAQParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public BRAQParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class ProgramContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(BRAQParser.Eof, 0); }
		public StmtContext[] stmt() {
			return GetRuleContexts<StmtContext>();
		}
		public StmtContext stmt(int i) {
			return GetRuleContext<StmtContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterProgram(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitProgram(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgram(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 23;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << VAR) | (1L << PRINT) | (1L << READ) | (1L << ID))) != 0)) {
				{
				{
				State = 20; stmt();
				}
				}
				State = 25;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
			State = 26; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StmtContext : ParserRuleContext {
		public Var_stmt_baseContext containing_var;
		public Print_stmt_baseContext containing_print;
		public Assign_stmt_baseContext containing_assign;
		public Read_stmt_baseContext containing_read;
		public ITerminalNode SEMICOLON() { return GetToken(BRAQParser.SEMICOLON, 0); }
		public Var_stmt_baseContext var_stmt_base() {
			return GetRuleContext<Var_stmt_baseContext>(0);
		}
		public Print_stmt_baseContext print_stmt_base() {
			return GetRuleContext<Print_stmt_baseContext>(0);
		}
		public Assign_stmt_baseContext assign_stmt_base() {
			return GetRuleContext<Assign_stmt_baseContext>(0);
		}
		public Read_stmt_baseContext read_stmt_base() {
			return GetRuleContext<Read_stmt_baseContext>(0);
		}
		public StmtContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_stmt; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterStmt(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitStmt(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStmt(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StmtContext stmt() {
		StmtContext _localctx = new StmtContext(Context, State);
		EnterRule(_localctx, 2, RULE_stmt);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 32;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case VAR:
				{
				State = 28; _localctx.containing_var = var_stmt_base();
				}
				break;
			case PRINT:
				{
				State = 29; _localctx.containing_print = print_stmt_base();
				}
				break;
			case ID:
				{
				State = 30; _localctx.containing_assign = assign_stmt_base();
				}
				break;
			case READ:
				{
				State = 31; _localctx.containing_read = read_stmt_base();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			State = 34; Match(SEMICOLON);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Var_stmt_baseContext : ParserRuleContext {
		public IToken id_name;
		public ExprContext assignee;
		public ITerminalNode VAR() { return GetToken(BRAQParser.VAR, 0); }
		public ITerminalNode ID() { return GetToken(BRAQParser.ID, 0); }
		public ITerminalNode EQUALS() { return GetToken(BRAQParser.EQUALS, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public Var_stmt_baseContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_var_stmt_base; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterVar_stmt_base(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitVar_stmt_base(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVar_stmt_base(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Var_stmt_baseContext var_stmt_base() {
		Var_stmt_baseContext _localctx = new Var_stmt_baseContext(Context, State);
		EnterRule(_localctx, 4, RULE_var_stmt_base);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 36; Match(VAR);
			State = 37; _localctx.id_name = Match(ID);
			State = 40;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			if (_la==EQUALS) {
				{
				State = 38; Match(EQUALS);
				State = 39; _localctx.assignee = expr(0);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Print_stmt_baseContext : ParserRuleContext {
		public ExprContext arg;
		public ITerminalNode PRINT() { return GetToken(BRAQParser.PRINT, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public Print_stmt_baseContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_print_stmt_base; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterPrint_stmt_base(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitPrint_stmt_base(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPrint_stmt_base(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Print_stmt_baseContext print_stmt_base() {
		Print_stmt_baseContext _localctx = new Print_stmt_baseContext(Context, State);
		EnterRule(_localctx, 6, RULE_print_stmt_base);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 42; Match(PRINT);
			State = 43; _localctx.arg = expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Assign_stmt_baseContext : ParserRuleContext {
		public IToken id_name;
		public ExprContext assignee;
		public ITerminalNode EQUALS() { return GetToken(BRAQParser.EQUALS, 0); }
		public ITerminalNode ID() { return GetToken(BRAQParser.ID, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public Assign_stmt_baseContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_assign_stmt_base; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterAssign_stmt_base(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitAssign_stmt_base(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAssign_stmt_base(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Assign_stmt_baseContext assign_stmt_base() {
		Assign_stmt_baseContext _localctx = new Assign_stmt_baseContext(Context, State);
		EnterRule(_localctx, 8, RULE_assign_stmt_base);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 45; _localctx.id_name = Match(ID);
			State = 46; Match(EQUALS);
			State = 47; _localctx.assignee = expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Read_stmt_baseContext : ParserRuleContext {
		public Var_nodeContext arg;
		public ITerminalNode READ() { return GetToken(BRAQParser.READ, 0); }
		public Var_nodeContext var_node() {
			return GetRuleContext<Var_nodeContext>(0);
		}
		public Read_stmt_baseContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_read_stmt_base; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterRead_stmt_base(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitRead_stmt_base(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRead_stmt_base(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Read_stmt_baseContext read_stmt_base() {
		Read_stmt_baseContext _localctx = new Read_stmt_baseContext(Context, State);
		EnterRule(_localctx, 10, RULE_read_stmt_base);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 49; Match(READ);
			State = 50; _localctx.arg = var_node();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExprContext : ParserRuleContext {
		public ExprContext left;
		public GroupContext grouping;
		public LiteralContext num;
		public IToken op;
		public ExprContext right;
		public GroupContext group() {
			return GetRuleContext<GroupContext>(0);
		}
		public LiteralContext literal() {
			return GetRuleContext<LiteralContext>(0);
		}
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ITerminalNode MODULUS() { return GetToken(BRAQParser.MODULUS, 0); }
		public ITerminalNode STAR() { return GetToken(BRAQParser.STAR, 0); }
		public ITerminalNode SLASH() { return GetToken(BRAQParser.SLASH, 0); }
		public ITerminalNode PLUS() { return GetToken(BRAQParser.PLUS, 0); }
		public ITerminalNode MINUS() { return GetToken(BRAQParser.MINUS, 0); }
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterExpr(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitExpr(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpr(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		return expr(0);
	}

	private ExprContext expr(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExprContext _localctx = new ExprContext(Context, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 12;
		EnterRecursionRule(_localctx, 12, RULE_expr, _p);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 55;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case LBRACKET:
				{
				State = 53; _localctx.grouping = group();
				}
				break;
			case NUMBER:
			case STRING:
			case ID:
				{
				State = 54; _localctx.num = literal();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 74;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,5,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 72;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,4,Context) ) {
					case 1:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 57;
						if (!(Precpred(Context, 7))) throw new FailedPredicateException(this, "Precpred(Context, 7)");
						State = 58; _localctx.op = Match(MODULUS);
						State = 59; _localctx.right = expr(8);
						}
						break;
					case 2:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 60;
						if (!(Precpred(Context, 6))) throw new FailedPredicateException(this, "Precpred(Context, 6)");
						State = 61; _localctx.op = Match(STAR);
						State = 62; _localctx.right = expr(7);
						}
						break;
					case 3:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 63;
						if (!(Precpred(Context, 5))) throw new FailedPredicateException(this, "Precpred(Context, 5)");
						State = 64; _localctx.op = Match(SLASH);
						State = 65; _localctx.right = expr(6);
						}
						break;
					case 4:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 66;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 67; _localctx.op = Match(PLUS);
						State = 68; _localctx.right = expr(5);
						}
						break;
					case 5:
						{
						_localctx = new ExprContext(_parentctx, _parentState);
						_localctx.left = _prevctx;
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 69;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 70; _localctx.op = Match(MINUS);
						State = 71; _localctx.right = expr(4);
						}
						break;
					}
					} 
				}
				State = 76;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,5,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class GroupContext : ParserRuleContext {
		public ExprContext containing;
		public ITerminalNode LBRACKET() { return GetToken(BRAQParser.LBRACKET, 0); }
		public ITerminalNode RBRACKET() { return GetToken(BRAQParser.RBRACKET, 0); }
		public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public GroupContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_group; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterGroup(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitGroup(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitGroup(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public GroupContext group() {
		GroupContext _localctx = new GroupContext(Context, State);
		EnterRule(_localctx, 14, RULE_group);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 77; Match(LBRACKET);
			State = 78; _localctx.containing = expr(0);
			State = 79; Match(RBRACKET);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class LiteralContext : ParserRuleContext {
		public IToken num;
		public Var_nodeContext var_node_;
		public IToken str;
		public ITerminalNode NUMBER() { return GetToken(BRAQParser.NUMBER, 0); }
		public Var_nodeContext var_node() {
			return GetRuleContext<Var_nodeContext>(0);
		}
		public ITerminalNode STRING() { return GetToken(BRAQParser.STRING, 0); }
		public LiteralContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_literal; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterLiteral(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitLiteral(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLiteral(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LiteralContext literal() {
		LiteralContext _localctx = new LiteralContext(Context, State);
		EnterRule(_localctx, 16, RULE_literal);
		try {
			State = 84;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case NUMBER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 81; _localctx.num = Match(NUMBER);
				}
				break;
			case ID:
				EnterOuterAlt(_localctx, 2);
				{
				State = 82; _localctx.var_node_ = var_node();
				}
				break;
			case STRING:
				EnterOuterAlt(_localctx, 3);
				{
				State = 83; _localctx.str = Match(STRING);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Var_nodeContext : ParserRuleContext {
		public IToken id_name;
		public ITerminalNode ID() { return GetToken(BRAQParser.ID, 0); }
		public Var_nodeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_var_node; } }
		public override void EnterRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.EnterVar_node(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IBRAQParserListener typedListener = listener as IBRAQParserListener;
			if (typedListener != null) typedListener.ExitVar_node(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IBRAQParserVisitor<TResult> typedVisitor = visitor as IBRAQParserVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVar_node(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Var_nodeContext var_node() {
		Var_nodeContext _localctx = new Var_nodeContext(Context, State);
		EnterRule(_localctx, 18, RULE_var_node);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 86; _localctx.id_name = Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 6: return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 7);
		case 1: return Precpred(Context, 6);
		case 2: return Precpred(Context, 5);
		case 3: return Precpred(Context, 4);
		case 4: return Precpred(Context, 3);
		}
		return true;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\x13', '[', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', '\t', '\b', 
		'\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', '\t', '\v', 
		'\x3', '\x2', '\a', '\x2', '\x18', '\n', '\x2', '\f', '\x2', '\xE', '\x2', 
		'\x1B', '\v', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x5', '\x3', '#', '\n', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x5', '\x4', '+', '\n', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', '\x3', '\b', 
		'\x5', '\b', ':', '\n', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', 
		'\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\b', '\a', '\b', 'K', '\n', '\b', '\f', '\b', '\xE', '\b', 'N', '\v', 
		'\b', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\n', 
		'\x3', '\n', '\x3', '\n', '\x5', '\n', 'W', '\n', '\n', '\x3', '\v', '\x3', 
		'\v', '\x3', '\v', '\x2', '\x3', '\xE', '\f', '\x2', '\x4', '\x6', '\b', 
		'\n', '\f', '\xE', '\x10', '\x12', '\x14', '\x2', '\x2', '\x2', ']', '\x2', 
		'\x19', '\x3', '\x2', '\x2', '\x2', '\x4', '\"', '\x3', '\x2', '\x2', 
		'\x2', '\x6', '&', '\x3', '\x2', '\x2', '\x2', '\b', ',', '\x3', '\x2', 
		'\x2', '\x2', '\n', '/', '\x3', '\x2', '\x2', '\x2', '\f', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\xE', '\x39', '\x3', '\x2', '\x2', '\x2', '\x10', 
		'O', '\x3', '\x2', '\x2', '\x2', '\x12', 'V', '\x3', '\x2', '\x2', '\x2', 
		'\x14', 'X', '\x3', '\x2', '\x2', '\x2', '\x16', '\x18', '\x5', '\x4', 
		'\x3', '\x2', '\x17', '\x16', '\x3', '\x2', '\x2', '\x2', '\x18', '\x1B', 
		'\x3', '\x2', '\x2', '\x2', '\x19', '\x17', '\x3', '\x2', '\x2', '\x2', 
		'\x19', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x1A', '\x1C', '\x3', '\x2', 
		'\x2', '\x2', '\x1B', '\x19', '\x3', '\x2', '\x2', '\x2', '\x1C', '\x1D', 
		'\a', '\x2', '\x2', '\x3', '\x1D', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\x1E', '#', '\x5', '\x6', '\x4', '\x2', '\x1F', '#', '\x5', '\b', '\x5', 
		'\x2', ' ', '#', '\x5', '\n', '\x6', '\x2', '!', '#', '\x5', '\f', '\a', 
		'\x2', '\"', '\x1E', '\x3', '\x2', '\x2', '\x2', '\"', '\x1F', '\x3', 
		'\x2', '\x2', '\x2', '\"', ' ', '\x3', '\x2', '\x2', '\x2', '\"', '!', 
		'\x3', '\x2', '\x2', '\x2', '#', '$', '\x3', '\x2', '\x2', '\x2', '$', 
		'%', '\a', '\f', '\x2', '\x2', '%', '\x5', '\x3', '\x2', '\x2', '\x2', 
		'&', '\'', '\a', '\xF', '\x2', '\x2', '\'', '*', '\a', '\x13', '\x2', 
		'\x2', '(', ')', '\a', '\x3', '\x2', '\x2', ')', '+', '\x5', '\xE', '\b', 
		'\x2', '*', '(', '\x3', '\x2', '\x2', '\x2', '*', '+', '\x3', '\x2', '\x2', 
		'\x2', '+', '\a', '\x3', '\x2', '\x2', '\x2', ',', '-', '\a', '\x10', 
		'\x2', '\x2', '-', '.', '\x5', '\xE', '\b', '\x2', '.', '\t', '\x3', '\x2', 
		'\x2', '\x2', '/', '\x30', '\a', '\x13', '\x2', '\x2', '\x30', '\x31', 
		'\a', '\x3', '\x2', '\x2', '\x31', '\x32', '\x5', '\xE', '\b', '\x2', 
		'\x32', '\v', '\x3', '\x2', '\x2', '\x2', '\x33', '\x34', '\a', '\x11', 
		'\x2', '\x2', '\x34', '\x35', '\x5', '\x14', '\v', '\x2', '\x35', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\x36', '\x37', '\b', '\b', '\x1', '\x2', 
		'\x37', ':', '\x5', '\x10', '\t', '\x2', '\x38', ':', '\x5', '\x12', '\n', 
		'\x2', '\x39', '\x36', '\x3', '\x2', '\x2', '\x2', '\x39', '\x38', '\x3', 
		'\x2', '\x2', '\x2', ':', 'L', '\x3', '\x2', '\x2', '\x2', ';', '<', '\f', 
		'\t', '\x2', '\x2', '<', '=', '\a', '\b', '\x2', '\x2', '=', 'K', '\x5', 
		'\xE', '\b', '\n', '>', '?', '\f', '\b', '\x2', '\x2', '?', '@', '\a', 
		'\x6', '\x2', '\x2', '@', 'K', '\x5', '\xE', '\b', '\t', '\x41', '\x42', 
		'\f', '\a', '\x2', '\x2', '\x42', '\x43', '\a', '\a', '\x2', '\x2', '\x43', 
		'K', '\x5', '\xE', '\b', '\b', '\x44', '\x45', '\f', '\x6', '\x2', '\x2', 
		'\x45', '\x46', '\a', '\x4', '\x2', '\x2', '\x46', 'K', '\x5', '\xE', 
		'\b', '\a', 'G', 'H', '\f', '\x5', '\x2', '\x2', 'H', 'I', '\a', '\x5', 
		'\x2', '\x2', 'I', 'K', '\x5', '\xE', '\b', '\x6', 'J', ';', '\x3', '\x2', 
		'\x2', '\x2', 'J', '>', '\x3', '\x2', '\x2', '\x2', 'J', '\x41', '\x3', 
		'\x2', '\x2', '\x2', 'J', '\x44', '\x3', '\x2', '\x2', '\x2', 'J', 'G', 
		'\x3', '\x2', '\x2', '\x2', 'K', 'N', '\x3', '\x2', '\x2', '\x2', 'L', 
		'J', '\x3', '\x2', '\x2', '\x2', 'L', 'M', '\x3', '\x2', '\x2', '\x2', 
		'M', '\xF', '\x3', '\x2', '\x2', '\x2', 'N', 'L', '\x3', '\x2', '\x2', 
		'\x2', 'O', 'P', '\a', '\n', '\x2', '\x2', 'P', 'Q', '\x5', '\xE', '\b', 
		'\x2', 'Q', 'R', '\a', '\v', '\x2', '\x2', 'R', '\x11', '\x3', '\x2', 
		'\x2', '\x2', 'S', 'W', '\a', '\t', '\x2', '\x2', 'T', 'W', '\x5', '\x14', 
		'\v', '\x2', 'U', 'W', '\a', '\x12', '\x2', '\x2', 'V', 'S', '\x3', '\x2', 
		'\x2', '\x2', 'V', 'T', '\x3', '\x2', '\x2', '\x2', 'V', 'U', '\x3', '\x2', 
		'\x2', '\x2', 'W', '\x13', '\x3', '\x2', '\x2', '\x2', 'X', 'Y', '\a', 
		'\x13', '\x2', '\x2', 'Y', '\x15', '\x3', '\x2', '\x2', '\x2', '\t', '\x19', 
		'\"', '*', '\x39', 'J', 'L', 'V',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
