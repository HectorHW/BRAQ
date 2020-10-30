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

namespace BRAQ_grammar {
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
		COMMENT=1, SHORT_EQUALS=2, EQUALS=3, LBRACKET=4, RBRACKET=5, LSQBRACKET=6, 
		RSQBRACKET=7, LBRACE=8, RBRACE=9, SEMICOLON=10, DOUBLE_STAR=11, PLUS=12, 
		MINUS=13, STAR=14, SLASH=15, DEF=16, VAR=17, WHILE=18, NUMBER=19, FLOAT_NUMBER=20, 
		ID=21, OTHER_OP=22, STRING=23, WHITESPACE=24, NEWLINE=25;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"COMMENT", "SHORT_EQUALS", "EQUALS", "LBRACKET", "RBRACKET", "LSQBRACKET", 
		"RSQBRACKET", "LBRACE", "RBRACE", "SEMICOLON", "DOUBLE_STAR", "PLUS", 
		"MINUS", "STAR", "SLASH", "DEF", "VAR", "WHILE", "NUMBER", "FLOAT_NUMBER", 
		"ID", "OTHER_OP", "STRING", "WHITESPACE", "NEWLINE"
	};


	public BRAQLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public BRAQLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, "'='", "'('", "')'", "'['", "']'", "'{'", "'}'", "';'", 
		"'**'", "'+'", "'-'", "'*'", "'/'", "'def'", "'var'", "'while'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "COMMENT", "SHORT_EQUALS", "EQUALS", "LBRACKET", "RBRACKET", "LSQBRACKET", 
		"RSQBRACKET", "LBRACE", "RBRACE", "SEMICOLON", "DOUBLE_STAR", "PLUS", 
		"MINUS", "STAR", "SLASH", "DEF", "VAR", "WHILE", "NUMBER", "FLOAT_NUMBER", 
		"ID", "OTHER_OP", "STRING", "WHITESPACE", "NEWLINE"
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
		'\x5964', '\x2', '\x1B', '\xA3', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x3', 
		'\x2', '\x3', '\x2', '\a', '\x2', '\x38', '\n', '\x2', '\f', '\x2', '\xE', 
		'\x2', ';', '\v', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x5', 
		'\x3', '\x45', '\n', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', 
		'\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', '\x3', '\t', '\x3', 
		'\t', '\x3', '\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', 
		'\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x3', 
		'\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', '\x3', 
		'\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', 
		'\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\x6', 
		'\x14', 's', '\n', '\x14', '\r', '\x14', '\xE', '\x14', 't', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x15', '\x6', '\x15', 'z', '\n', '\x15', '\r', 
		'\x15', '\xE', '\x15', '{', '\x3', '\x16', '\x3', '\x16', '\a', '\x16', 
		'\x80', '\n', '\x16', '\f', '\x16', '\xE', '\x16', '\x83', '\v', '\x16', 
		'\x3', '\x17', '\x6', '\x17', '\x86', '\n', '\x17', '\r', '\x17', '\xE', 
		'\x17', '\x87', '\x3', '\x18', '\x3', '\x18', '\a', '\x18', '\x8C', '\n', 
		'\x18', '\f', '\x18', '\xE', '\x18', '\x8F', '\v', '\x18', '\x3', '\x18', 
		'\x3', '\x18', '\x3', '\x19', '\x6', '\x19', '\x94', '\n', '\x19', '\r', 
		'\x19', '\xE', '\x19', '\x95', '\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', 
		'\x5', '\x1A', '\x9B', '\n', '\x1A', '\x3', '\x1A', '\x6', '\x1A', '\x9E', 
		'\n', '\x1A', '\r', '\x1A', '\xE', '\x1A', '\x9F', '\x3', '\x1A', '\x3', 
		'\x1A', '\x4', '\x39', '\x8D', '\x2', '\x1B', '\x3', '\x3', '\x5', '\x4', 
		'\a', '\x5', '\t', '\x6', '\v', '\a', '\r', '\b', '\xF', '\t', '\x11', 
		'\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', '\x19', '\xE', '\x1B', 
		'\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', '#', '\x13', '%', 
		'\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', '\x18', '/', '\x19', 
		'\x31', '\x1A', '\x33', '\x1B', '\x3', '\x2', '\a', '\x3', '\x2', '\x32', 
		';', '\x5', '\x2', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x6', '\x2', 
		'\x32', ';', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\v', '\x2', '(', 
		'(', ',', '-', '/', '/', '<', '<', '>', '@', '\x42', '\x42', '^', '^', 
		'`', '`', '~', '~', '\x4', '\x2', '\v', '\v', '\"', '\"', '\x2', '\xB0', 
		'\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x3', '\x35', '\x3', '\x2', '\x2', '\x2', '\x5', 
		'\x44', '\x3', '\x2', '\x2', '\x2', '\a', 'H', '\x3', '\x2', '\x2', '\x2', 
		'\t', 'J', '\x3', '\x2', '\x2', '\x2', '\v', 'L', '\x3', '\x2', '\x2', 
		'\x2', '\r', 'N', '\x3', '\x2', '\x2', '\x2', '\xF', 'P', '\x3', '\x2', 
		'\x2', '\x2', '\x11', 'R', '\x3', '\x2', '\x2', '\x2', '\x13', 'T', '\x3', 
		'\x2', '\x2', '\x2', '\x15', 'V', '\x3', '\x2', '\x2', '\x2', '\x17', 
		'X', '\x3', '\x2', '\x2', '\x2', '\x19', '[', '\x3', '\x2', '\x2', '\x2', 
		'\x1B', ']', '\x3', '\x2', '\x2', '\x2', '\x1D', '_', '\x3', '\x2', '\x2', 
		'\x2', '\x1F', '\x61', '\x3', '\x2', '\x2', '\x2', '!', '\x63', '\x3', 
		'\x2', '\x2', '\x2', '#', 'g', '\x3', '\x2', '\x2', '\x2', '%', 'k', '\x3', 
		'\x2', '\x2', '\x2', '\'', 'r', '\x3', '\x2', '\x2', '\x2', ')', 'v', 
		'\x3', '\x2', '\x2', '\x2', '+', '}', '\x3', '\x2', '\x2', '\x2', '-', 
		'\x85', '\x3', '\x2', '\x2', '\x2', '/', '\x89', '\x3', '\x2', '\x2', 
		'\x2', '\x31', '\x93', '\x3', '\x2', '\x2', '\x2', '\x33', '\x9D', '\x3', 
		'\x2', '\x2', '\x2', '\x35', '\x39', '\a', '^', '\x2', '\x2', '\x36', 
		'\x38', '\v', '\x2', '\x2', '\x2', '\x37', '\x36', '\x3', '\x2', '\x2', 
		'\x2', '\x38', ';', '\x3', '\x2', '\x2', '\x2', '\x39', ':', '\x3', '\x2', 
		'\x2', '\x2', '\x39', '\x37', '\x3', '\x2', '\x2', '\x2', ':', '<', '\x3', 
		'\x2', '\x2', '\x2', ';', '\x39', '\x3', '\x2', '\x2', '\x2', '<', '=', 
		'\b', '\x2', '\x2', '\x2', '=', '\x4', '\x3', '\x2', '\x2', '\x2', '>', 
		'\x45', '\x5', '\x17', '\f', '\x2', '?', '\x45', '\x5', '\x19', '\r', 
		'\x2', '@', '\x45', '\x5', '\x1B', '\xE', '\x2', '\x41', '\x45', '\x5', 
		'\x1D', '\xF', '\x2', '\x42', '\x45', '\x5', '\x1F', '\x10', '\x2', '\x43', 
		'\x45', '\x5', '-', '\x17', '\x2', '\x44', '>', '\x3', '\x2', '\x2', '\x2', 
		'\x44', '?', '\x3', '\x2', '\x2', '\x2', '\x44', '@', '\x3', '\x2', '\x2', 
		'\x2', '\x44', '\x41', '\x3', '\x2', '\x2', '\x2', '\x44', '\x42', '\x3', 
		'\x2', '\x2', '\x2', '\x44', '\x43', '\x3', '\x2', '\x2', '\x2', '\x45', 
		'\x46', '\x3', '\x2', '\x2', '\x2', '\x46', 'G', '\x5', '\a', '\x4', '\x2', 
		'G', '\x6', '\x3', '\x2', '\x2', '\x2', 'H', 'I', '\a', '?', '\x2', '\x2', 
		'I', '\b', '\x3', '\x2', '\x2', '\x2', 'J', 'K', '\a', '*', '\x2', '\x2', 
		'K', '\n', '\x3', '\x2', '\x2', '\x2', 'L', 'M', '\a', '+', '\x2', '\x2', 
		'M', '\f', '\x3', '\x2', '\x2', '\x2', 'N', 'O', '\a', ']', '\x2', '\x2', 
		'O', '\xE', '\x3', '\x2', '\x2', '\x2', 'P', 'Q', '\a', '_', '\x2', '\x2', 
		'Q', '\x10', '\x3', '\x2', '\x2', '\x2', 'R', 'S', '\a', '}', '\x2', '\x2', 
		'S', '\x12', '\x3', '\x2', '\x2', '\x2', 'T', 'U', '\a', '\x7F', '\x2', 
		'\x2', 'U', '\x14', '\x3', '\x2', '\x2', '\x2', 'V', 'W', '\a', '=', '\x2', 
		'\x2', 'W', '\x16', '\x3', '\x2', '\x2', '\x2', 'X', 'Y', '\a', ',', '\x2', 
		'\x2', 'Y', 'Z', '\a', ',', '\x2', '\x2', 'Z', '\x18', '\x3', '\x2', '\x2', 
		'\x2', '[', '\\', '\a', '-', '\x2', '\x2', '\\', '\x1A', '\x3', '\x2', 
		'\x2', '\x2', ']', '^', '\a', '/', '\x2', '\x2', '^', '\x1C', '\x3', '\x2', 
		'\x2', '\x2', '_', '`', '\a', ',', '\x2', '\x2', '`', '\x1E', '\x3', '\x2', 
		'\x2', '\x2', '\x61', '\x62', '\a', '\x31', '\x2', '\x2', '\x62', ' ', 
		'\x3', '\x2', '\x2', '\x2', '\x63', '\x64', '\a', '\x66', '\x2', '\x2', 
		'\x64', '\x65', '\a', 'g', '\x2', '\x2', '\x65', '\x66', '\a', 'h', '\x2', 
		'\x2', '\x66', '\"', '\x3', '\x2', '\x2', '\x2', 'g', 'h', '\a', 'x', 
		'\x2', '\x2', 'h', 'i', '\a', '\x63', '\x2', '\x2', 'i', 'j', '\a', 't', 
		'\x2', '\x2', 'j', '$', '\x3', '\x2', '\x2', '\x2', 'k', 'l', '\a', 'y', 
		'\x2', '\x2', 'l', 'm', '\a', 'j', '\x2', '\x2', 'm', 'n', '\a', 'k', 
		'\x2', '\x2', 'n', 'o', '\a', 'n', '\x2', '\x2', 'o', 'p', '\a', 'g', 
		'\x2', '\x2', 'p', '&', '\x3', '\x2', '\x2', '\x2', 'q', 's', '\t', '\x2', 
		'\x2', '\x2', 'r', 'q', '\x3', '\x2', '\x2', '\x2', 's', 't', '\x3', '\x2', 
		'\x2', '\x2', 't', 'r', '\x3', '\x2', '\x2', '\x2', 't', 'u', '\x3', '\x2', 
		'\x2', '\x2', 'u', '(', '\x3', '\x2', '\x2', '\x2', 'v', 'w', '\t', '\x2', 
		'\x2', '\x2', 'w', 'y', '\a', '\x30', '\x2', '\x2', 'x', 'z', '\t', '\x2', 
		'\x2', '\x2', 'y', 'x', '\x3', '\x2', '\x2', '\x2', 'z', '{', '\x3', '\x2', 
		'\x2', '\x2', '{', 'y', '\x3', '\x2', '\x2', '\x2', '{', '|', '\x3', '\x2', 
		'\x2', '\x2', '|', '*', '\x3', '\x2', '\x2', '\x2', '}', '\x81', '\t', 
		'\x3', '\x2', '\x2', '~', '\x80', '\t', '\x4', '\x2', '\x2', '\x7F', '~', 
		'\x3', '\x2', '\x2', '\x2', '\x80', '\x83', '\x3', '\x2', '\x2', '\x2', 
		'\x81', '\x7F', '\x3', '\x2', '\x2', '\x2', '\x81', '\x82', '\x3', '\x2', 
		'\x2', '\x2', '\x82', ',', '\x3', '\x2', '\x2', '\x2', '\x83', '\x81', 
		'\x3', '\x2', '\x2', '\x2', '\x84', '\x86', '\t', '\x5', '\x2', '\x2', 
		'\x85', '\x84', '\x3', '\x2', '\x2', '\x2', '\x86', '\x87', '\x3', '\x2', 
		'\x2', '\x2', '\x87', '\x85', '\x3', '\x2', '\x2', '\x2', '\x87', '\x88', 
		'\x3', '\x2', '\x2', '\x2', '\x88', '.', '\x3', '\x2', '\x2', '\x2', '\x89', 
		'\x8D', '\a', '$', '\x2', '\x2', '\x8A', '\x8C', '\v', '\x2', '\x2', '\x2', 
		'\x8B', '\x8A', '\x3', '\x2', '\x2', '\x2', '\x8C', '\x8F', '\x3', '\x2', 
		'\x2', '\x2', '\x8D', '\x8E', '\x3', '\x2', '\x2', '\x2', '\x8D', '\x8B', 
		'\x3', '\x2', '\x2', '\x2', '\x8E', '\x90', '\x3', '\x2', '\x2', '\x2', 
		'\x8F', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x90', '\x91', '\a', '$', 
		'\x2', '\x2', '\x91', '\x30', '\x3', '\x2', '\x2', '\x2', '\x92', '\x94', 
		'\t', '\x6', '\x2', '\x2', '\x93', '\x92', '\x3', '\x2', '\x2', '\x2', 
		'\x94', '\x95', '\x3', '\x2', '\x2', '\x2', '\x95', '\x93', '\x3', '\x2', 
		'\x2', '\x2', '\x95', '\x96', '\x3', '\x2', '\x2', '\x2', '\x96', '\x97', 
		'\x3', '\x2', '\x2', '\x2', '\x97', '\x98', '\b', '\x19', '\x2', '\x2', 
		'\x98', '\x32', '\x3', '\x2', '\x2', '\x2', '\x99', '\x9B', '\a', '\xF', 
		'\x2', '\x2', '\x9A', '\x99', '\x3', '\x2', '\x2', '\x2', '\x9A', '\x9B', 
		'\x3', '\x2', '\x2', '\x2', '\x9B', '\x9C', '\x3', '\x2', '\x2', '\x2', 
		'\x9C', '\x9E', '\a', '\f', '\x2', '\x2', '\x9D', '\x9A', '\x3', '\x2', 
		'\x2', '\x2', '\x9E', '\x9F', '\x3', '\x2', '\x2', '\x2', '\x9F', '\x9D', 
		'\x3', '\x2', '\x2', '\x2', '\x9F', '\xA0', '\x3', '\x2', '\x2', '\x2', 
		'\xA0', '\xA1', '\x3', '\x2', '\x2', '\x2', '\xA1', '\xA2', '\b', '\x1A', 
		'\x2', '\x2', '\xA2', '\x34', '\x3', '\x2', '\x2', '\x2', '\r', '\x2', 
		'\x39', '\x44', 't', '{', '\x81', '\x87', '\x8D', '\x95', '\x9A', '\x9F', 
		'\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace BRAQ_grammar
