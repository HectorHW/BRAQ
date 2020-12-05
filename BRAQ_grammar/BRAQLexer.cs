//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/hecto/RiderProjects/BRAQ/BRAQ_grammar\BRAQLexer.g4 by ANTLR 4.8

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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class BRAQLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		EQUALS=1, PLUS=2, MINUS=3, STAR=4, SLASH=5, MODULUS=6, AT_OPERATOR=7, 
		GR=8, GE=9, LS=10, LE=11, EQ=12, NE=13, AND=14, OR=15, XOR=16, NOT=17, 
		NUMBER=18, DOUBLE_NUMBER=19, LBRACKET=20, RBRACKET=21, LCURLY=22, RCURLY=23, 
		SEMICOLON=24, COLON=25, DOT=26, NEWLINE=27, SPACE=28, VAR=29, RETURN=30, 
		BREAK=31, CONTINUE=32, WHILE=33, FOR=34, DEF=35, IF=36, ELSE=37, STRING=38, 
		ID=39;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"EQUALS", "PLUS", "MINUS", "STAR", "SLASH", "MODULUS", "AT_OPERATOR", 
		"GR", "GE", "LS", "LE", "EQ", "NE", "AND", "OR", "XOR", "NOT", "NUMBER", 
		"DOUBLE_NUMBER", "LBRACKET", "RBRACKET", "LCURLY", "RCURLY", "SEMICOLON", 
		"COLON", "DOT", "NEWLINE", "SPACE", "VAR", "RETURN", "BREAK", "CONTINUE", 
		"WHILE", "FOR", "DEF", "IF", "ELSE", "STRING", "ID"
	};


	public BRAQLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public BRAQLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'='", "'+'", "'-'", "'*'", "'/'", "'%'", "'@'", "'>'", "'>='", 
		"'<'", "'<='", "'?='", "'!='", "'and'", "'or'", "'xor'", "'not'", null, 
		null, "'('", "')'", "'{'", "'}'", "';'", "':'", "'.'", null, null, "'var'", 
		"'return'", "'break'", "'continue'", "'while'", "'for'", "'def'", "'if'", 
		"'else'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "EQUALS", "PLUS", "MINUS", "STAR", "SLASH", "MODULUS", "AT_OPERATOR", 
		"GR", "GE", "LS", "LE", "EQ", "NE", "AND", "OR", "XOR", "NOT", "NUMBER", 
		"DOUBLE_NUMBER", "LBRACKET", "RBRACKET", "LCURLY", "RCURLY", "SEMICOLON", 
		"COLON", "DOT", "NEWLINE", "SPACE", "VAR", "RETURN", "BREAK", "CONTINUE", 
		"WHILE", "FOR", "DEF", "IF", "ELSE", "STRING", "ID"
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

	public override string GrammarFileName { get { return "BRAQLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static BRAQLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', ')', '\xEA', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x3', '\x2', 
		'\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', '\x4', 
		'\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', 
		'\a', '\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', 
		'\x3', '\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', 
		'\f', '\x3', '\f', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\xE', 
		'\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', 
		'\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', '\x11', 
		'\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x12', '\x3', '\x13', '\x6', '\x13', '\x80', '\n', 
		'\x13', '\r', '\x13', '\xE', '\x13', '\x81', '\x3', '\x14', '\x6', '\x14', 
		'\x85', '\n', '\x14', '\r', '\x14', '\xE', '\x14', '\x86', '\x3', '\x14', 
		'\x3', '\x14', '\x6', '\x14', '\x8B', '\n', '\x14', '\r', '\x14', '\xE', 
		'\x14', '\x8C', '\x3', '\x15', '\x3', '\x15', '\x3', '\x16', '\x3', '\x16', 
		'\x3', '\x17', '\x3', '\x17', '\x3', '\x18', '\x3', '\x18', '\x3', '\x19', 
		'\x3', '\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x3', '\x1B', 
		'\x3', '\x1C', '\x6', '\x1C', '\x9E', '\n', '\x1C', '\r', '\x1C', '\xE', 
		'\x1C', '\x9F', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x6', '\x1D', 
		'\xA5', '\n', '\x1D', '\r', '\x1D', '\xE', '\x1D', '\xA6', '\x3', '\x1D', 
		'\x3', '\x1D', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1E', 
		'\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', 
		'\x3', '\x1F', '\x3', '\x1F', '\x3', ' ', '\x3', ' ', '\x3', ' ', '\x3', 
		' ', '\x3', ' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', 
		'!', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', 
		'\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', 
		'\x3', '#', '\x3', '#', '\x3', '#', '\x3', '#', '\x3', '$', '\x3', '$', 
		'\x3', '$', '\x3', '$', '\x3', '%', '\x3', '%', '\x3', '%', '\x3', '&', 
		'\x3', '&', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', '\'', '\x3', '\'', 
		'\a', '\'', '\xDD', '\n', '\'', '\f', '\'', '\xE', '\'', '\xE0', '\v', 
		'\'', '\x3', '\'', '\x3', '\'', '\x3', '(', '\x3', '(', '\a', '(', '\xE6', 
		'\n', '(', '\f', '(', '\xE', '(', '\xE9', '\v', '(', '\x3', '\xDE', '\x2', 
		')', '\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', 
		'\r', '\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', 
		'\r', '\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', 
		'\x12', '#', '\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', 
		'-', '\x18', '/', '\x19', '\x31', '\x1A', '\x33', '\x1B', '\x35', '\x1C', 
		'\x37', '\x1D', '\x39', '\x1E', ';', '\x1F', '=', ' ', '?', '!', '\x41', 
		'\"', '\x43', '#', '\x45', '$', 'G', '%', 'I', '&', 'K', '\'', 'M', '(', 
		'O', ')', '\x3', '\x2', '\a', '\x3', '\x2', '\x32', ';', '\x4', '\x2', 
		'\f', '\f', '\xF', '\xF', '\x4', '\x2', '\v', '\v', '\"', '\"', '\x5', 
		'\x2', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x6', '\x2', '\x32', 
		';', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x2', '\xF0', '\x2', 
		'\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', ')', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', '\x37', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', '\x2', 
		'\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x2', '\x45', 
		'\x3', '\x2', '\x2', '\x2', '\x2', 'G', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'I', '\x3', '\x2', '\x2', '\x2', '\x2', 'K', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'M', '\x3', '\x2', '\x2', '\x2', '\x2', 'O', '\x3', '\x2', '\x2', 
		'\x2', '\x3', 'Q', '\x3', '\x2', '\x2', '\x2', '\x5', 'S', '\x3', '\x2', 
		'\x2', '\x2', '\a', 'U', '\x3', '\x2', '\x2', '\x2', '\t', 'W', '\x3', 
		'\x2', '\x2', '\x2', '\v', 'Y', '\x3', '\x2', '\x2', '\x2', '\r', '[', 
		'\x3', '\x2', '\x2', '\x2', '\xF', ']', '\x3', '\x2', '\x2', '\x2', '\x11', 
		'_', '\x3', '\x2', '\x2', '\x2', '\x13', '\x61', '\x3', '\x2', '\x2', 
		'\x2', '\x15', '\x64', '\x3', '\x2', '\x2', '\x2', '\x17', '\x66', '\x3', 
		'\x2', '\x2', '\x2', '\x19', 'i', '\x3', '\x2', '\x2', '\x2', '\x1B', 
		'l', '\x3', '\x2', '\x2', '\x2', '\x1D', 'o', '\x3', '\x2', '\x2', '\x2', 
		'\x1F', 's', '\x3', '\x2', '\x2', '\x2', '!', 'v', '\x3', '\x2', '\x2', 
		'\x2', '#', 'z', '\x3', '\x2', '\x2', '\x2', '%', '\x7F', '\x3', '\x2', 
		'\x2', '\x2', '\'', '\x84', '\x3', '\x2', '\x2', '\x2', ')', '\x8E', '\x3', 
		'\x2', '\x2', '\x2', '+', '\x90', '\x3', '\x2', '\x2', '\x2', '-', '\x92', 
		'\x3', '\x2', '\x2', '\x2', '/', '\x94', '\x3', '\x2', '\x2', '\x2', '\x31', 
		'\x96', '\x3', '\x2', '\x2', '\x2', '\x33', '\x98', '\x3', '\x2', '\x2', 
		'\x2', '\x35', '\x9A', '\x3', '\x2', '\x2', '\x2', '\x37', '\x9D', '\x3', 
		'\x2', '\x2', '\x2', '\x39', '\xA4', '\x3', '\x2', '\x2', '\x2', ';', 
		'\xAA', '\x3', '\x2', '\x2', '\x2', '=', '\xAE', '\x3', '\x2', '\x2', 
		'\x2', '?', '\xB5', '\x3', '\x2', '\x2', '\x2', '\x41', '\xBB', '\x3', 
		'\x2', '\x2', '\x2', '\x43', '\xC4', '\x3', '\x2', '\x2', '\x2', '\x45', 
		'\xCA', '\x3', '\x2', '\x2', '\x2', 'G', '\xCE', '\x3', '\x2', '\x2', 
		'\x2', 'I', '\xD2', '\x3', '\x2', '\x2', '\x2', 'K', '\xD5', '\x3', '\x2', 
		'\x2', '\x2', 'M', '\xDA', '\x3', '\x2', '\x2', '\x2', 'O', '\xE3', '\x3', 
		'\x2', '\x2', '\x2', 'Q', 'R', '\a', '?', '\x2', '\x2', 'R', '\x4', '\x3', 
		'\x2', '\x2', '\x2', 'S', 'T', '\a', '-', '\x2', '\x2', 'T', '\x6', '\x3', 
		'\x2', '\x2', '\x2', 'U', 'V', '\a', '/', '\x2', '\x2', 'V', '\b', '\x3', 
		'\x2', '\x2', '\x2', 'W', 'X', '\a', ',', '\x2', '\x2', 'X', '\n', '\x3', 
		'\x2', '\x2', '\x2', 'Y', 'Z', '\a', '\x31', '\x2', '\x2', 'Z', '\f', 
		'\x3', '\x2', '\x2', '\x2', '[', '\\', '\a', '\'', '\x2', '\x2', '\\', 
		'\xE', '\x3', '\x2', '\x2', '\x2', ']', '^', '\a', '\x42', '\x2', '\x2', 
		'^', '\x10', '\x3', '\x2', '\x2', '\x2', '_', '`', '\a', '@', '\x2', '\x2', 
		'`', '\x12', '\x3', '\x2', '\x2', '\x2', '\x61', '\x62', '\a', '@', '\x2', 
		'\x2', '\x62', '\x63', '\a', '?', '\x2', '\x2', '\x63', '\x14', '\x3', 
		'\x2', '\x2', '\x2', '\x64', '\x65', '\a', '>', '\x2', '\x2', '\x65', 
		'\x16', '\x3', '\x2', '\x2', '\x2', '\x66', 'g', '\a', '>', '\x2', '\x2', 
		'g', 'h', '\a', '?', '\x2', '\x2', 'h', '\x18', '\x3', '\x2', '\x2', '\x2', 
		'i', 'j', '\a', '\x41', '\x2', '\x2', 'j', 'k', '\a', '?', '\x2', '\x2', 
		'k', '\x1A', '\x3', '\x2', '\x2', '\x2', 'l', 'm', '\a', '#', '\x2', '\x2', 
		'm', 'n', '\a', '?', '\x2', '\x2', 'n', '\x1C', '\x3', '\x2', '\x2', '\x2', 
		'o', 'p', '\a', '\x63', '\x2', '\x2', 'p', 'q', '\a', 'p', '\x2', '\x2', 
		'q', 'r', '\a', '\x66', '\x2', '\x2', 'r', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', 's', 't', '\a', 'q', '\x2', '\x2', 't', 'u', '\a', 't', '\x2', 
		'\x2', 'u', ' ', '\x3', '\x2', '\x2', '\x2', 'v', 'w', '\a', 'z', '\x2', 
		'\x2', 'w', 'x', '\a', 'q', '\x2', '\x2', 'x', 'y', '\a', 't', '\x2', 
		'\x2', 'y', '\"', '\x3', '\x2', '\x2', '\x2', 'z', '{', '\a', 'p', '\x2', 
		'\x2', '{', '|', '\a', 'q', '\x2', '\x2', '|', '}', '\a', 'v', '\x2', 
		'\x2', '}', '$', '\x3', '\x2', '\x2', '\x2', '~', '\x80', '\t', '\x2', 
		'\x2', '\x2', '\x7F', '~', '\x3', '\x2', '\x2', '\x2', '\x80', '\x81', 
		'\x3', '\x2', '\x2', '\x2', '\x81', '\x7F', '\x3', '\x2', '\x2', '\x2', 
		'\x81', '\x82', '\x3', '\x2', '\x2', '\x2', '\x82', '&', '\x3', '\x2', 
		'\x2', '\x2', '\x83', '\x85', '\t', '\x2', '\x2', '\x2', '\x84', '\x83', 
		'\x3', '\x2', '\x2', '\x2', '\x85', '\x86', '\x3', '\x2', '\x2', '\x2', 
		'\x86', '\x84', '\x3', '\x2', '\x2', '\x2', '\x86', '\x87', '\x3', '\x2', 
		'\x2', '\x2', '\x87', '\x88', '\x3', '\x2', '\x2', '\x2', '\x88', '\x8A', 
		'\a', '\x30', '\x2', '\x2', '\x89', '\x8B', '\t', '\x2', '\x2', '\x2', 
		'\x8A', '\x89', '\x3', '\x2', '\x2', '\x2', '\x8B', '\x8C', '\x3', '\x2', 
		'\x2', '\x2', '\x8C', '\x8A', '\x3', '\x2', '\x2', '\x2', '\x8C', '\x8D', 
		'\x3', '\x2', '\x2', '\x2', '\x8D', '(', '\x3', '\x2', '\x2', '\x2', '\x8E', 
		'\x8F', '\a', '*', '\x2', '\x2', '\x8F', '*', '\x3', '\x2', '\x2', '\x2', 
		'\x90', '\x91', '\a', '+', '\x2', '\x2', '\x91', ',', '\x3', '\x2', '\x2', 
		'\x2', '\x92', '\x93', '\a', '}', '\x2', '\x2', '\x93', '.', '\x3', '\x2', 
		'\x2', '\x2', '\x94', '\x95', '\a', '\x7F', '\x2', '\x2', '\x95', '\x30', 
		'\x3', '\x2', '\x2', '\x2', '\x96', '\x97', '\a', '=', '\x2', '\x2', '\x97', 
		'\x32', '\x3', '\x2', '\x2', '\x2', '\x98', '\x99', '\a', '<', '\x2', 
		'\x2', '\x99', '\x34', '\x3', '\x2', '\x2', '\x2', '\x9A', '\x9B', '\a', 
		'\x30', '\x2', '\x2', '\x9B', '\x36', '\x3', '\x2', '\x2', '\x2', '\x9C', 
		'\x9E', '\t', '\x3', '\x2', '\x2', '\x9D', '\x9C', '\x3', '\x2', '\x2', 
		'\x2', '\x9E', '\x9F', '\x3', '\x2', '\x2', '\x2', '\x9F', '\x9D', '\x3', 
		'\x2', '\x2', '\x2', '\x9F', '\xA0', '\x3', '\x2', '\x2', '\x2', '\xA0', 
		'\xA1', '\x3', '\x2', '\x2', '\x2', '\xA1', '\xA2', '\b', '\x1C', '\x2', 
		'\x2', '\xA2', '\x38', '\x3', '\x2', '\x2', '\x2', '\xA3', '\xA5', '\t', 
		'\x4', '\x2', '\x2', '\xA4', '\xA3', '\x3', '\x2', '\x2', '\x2', '\xA5', 
		'\xA6', '\x3', '\x2', '\x2', '\x2', '\xA6', '\xA4', '\x3', '\x2', '\x2', 
		'\x2', '\xA6', '\xA7', '\x3', '\x2', '\x2', '\x2', '\xA7', '\xA8', '\x3', 
		'\x2', '\x2', '\x2', '\xA8', '\xA9', '\b', '\x1D', '\x2', '\x2', '\xA9', 
		':', '\x3', '\x2', '\x2', '\x2', '\xAA', '\xAB', '\a', 'x', '\x2', '\x2', 
		'\xAB', '\xAC', '\a', '\x63', '\x2', '\x2', '\xAC', '\xAD', '\a', 't', 
		'\x2', '\x2', '\xAD', '<', '\x3', '\x2', '\x2', '\x2', '\xAE', '\xAF', 
		'\a', 't', '\x2', '\x2', '\xAF', '\xB0', '\a', 'g', '\x2', '\x2', '\xB0', 
		'\xB1', '\a', 'v', '\x2', '\x2', '\xB1', '\xB2', '\a', 'w', '\x2', '\x2', 
		'\xB2', '\xB3', '\a', 't', '\x2', '\x2', '\xB3', '\xB4', '\a', 'p', '\x2', 
		'\x2', '\xB4', '>', '\x3', '\x2', '\x2', '\x2', '\xB5', '\xB6', '\a', 
		'\x64', '\x2', '\x2', '\xB6', '\xB7', '\a', 't', '\x2', '\x2', '\xB7', 
		'\xB8', '\a', 'g', '\x2', '\x2', '\xB8', '\xB9', '\a', '\x63', '\x2', 
		'\x2', '\xB9', '\xBA', '\a', 'm', '\x2', '\x2', '\xBA', '@', '\x3', '\x2', 
		'\x2', '\x2', '\xBB', '\xBC', '\a', '\x65', '\x2', '\x2', '\xBC', '\xBD', 
		'\a', 'q', '\x2', '\x2', '\xBD', '\xBE', '\a', 'p', '\x2', '\x2', '\xBE', 
		'\xBF', '\a', 'v', '\x2', '\x2', '\xBF', '\xC0', '\a', 'k', '\x2', '\x2', 
		'\xC0', '\xC1', '\a', 'p', '\x2', '\x2', '\xC1', '\xC2', '\a', 'w', '\x2', 
		'\x2', '\xC2', '\xC3', '\a', 'g', '\x2', '\x2', '\xC3', '\x42', '\x3', 
		'\x2', '\x2', '\x2', '\xC4', '\xC5', '\a', 'y', '\x2', '\x2', '\xC5', 
		'\xC6', '\a', 'j', '\x2', '\x2', '\xC6', '\xC7', '\a', 'k', '\x2', '\x2', 
		'\xC7', '\xC8', '\a', 'n', '\x2', '\x2', '\xC8', '\xC9', '\a', 'g', '\x2', 
		'\x2', '\xC9', '\x44', '\x3', '\x2', '\x2', '\x2', '\xCA', '\xCB', '\a', 
		'h', '\x2', '\x2', '\xCB', '\xCC', '\a', 'q', '\x2', '\x2', '\xCC', '\xCD', 
		'\a', 't', '\x2', '\x2', '\xCD', '\x46', '\x3', '\x2', '\x2', '\x2', '\xCE', 
		'\xCF', '\a', '\x66', '\x2', '\x2', '\xCF', '\xD0', '\a', 'g', '\x2', 
		'\x2', '\xD0', '\xD1', '\a', 'h', '\x2', '\x2', '\xD1', 'H', '\x3', '\x2', 
		'\x2', '\x2', '\xD2', '\xD3', '\a', 'k', '\x2', '\x2', '\xD3', '\xD4', 
		'\a', 'h', '\x2', '\x2', '\xD4', 'J', '\x3', '\x2', '\x2', '\x2', '\xD5', 
		'\xD6', '\a', 'g', '\x2', '\x2', '\xD6', '\xD7', '\a', 'n', '\x2', '\x2', 
		'\xD7', '\xD8', '\a', 'u', '\x2', '\x2', '\xD8', '\xD9', '\a', 'g', '\x2', 
		'\x2', '\xD9', 'L', '\x3', '\x2', '\x2', '\x2', '\xDA', '\xDE', '\a', 
		'$', '\x2', '\x2', '\xDB', '\xDD', '\v', '\x2', '\x2', '\x2', '\xDC', 
		'\xDB', '\x3', '\x2', '\x2', '\x2', '\xDD', '\xE0', '\x3', '\x2', '\x2', 
		'\x2', '\xDE', '\xDF', '\x3', '\x2', '\x2', '\x2', '\xDE', '\xDC', '\x3', 
		'\x2', '\x2', '\x2', '\xDF', '\xE1', '\x3', '\x2', '\x2', '\x2', '\xE0', 
		'\xDE', '\x3', '\x2', '\x2', '\x2', '\xE1', '\xE2', '\a', '$', '\x2', 
		'\x2', '\xE2', 'N', '\x3', '\x2', '\x2', '\x2', '\xE3', '\xE7', '\t', 
		'\x5', '\x2', '\x2', '\xE4', '\xE6', '\t', '\x6', '\x2', '\x2', '\xE5', 
		'\xE4', '\x3', '\x2', '\x2', '\x2', '\xE6', '\xE9', '\x3', '\x2', '\x2', 
		'\x2', '\xE7', '\xE5', '\x3', '\x2', '\x2', '\x2', '\xE7', '\xE8', '\x3', 
		'\x2', '\x2', '\x2', '\xE8', 'P', '\x3', '\x2', '\x2', '\x2', '\xE9', 
		'\xE7', '\x3', '\x2', '\x2', '\x2', '\n', '\x2', '\x81', '\x86', '\x8C', 
		'\x9F', '\xA6', '\xDE', '\xE7', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
