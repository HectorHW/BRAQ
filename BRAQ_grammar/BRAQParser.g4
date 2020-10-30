parser grammar BRAQParser;

options {tokenVocab = BRAQLexer; }

program: (stmt|block)* EOF;

block: LBRACE NEWLINE* (stmt)* RBRACE;

stmt: (var_stmt|expr_stmt) SEMICOLON;

var_stmt: VAR varname=ID (EQUALS assignee=expr)?;

expr_stmt: expr_=expr;

expr: 
grouping=group
| left=expr op=DOUBLE_STAR right=expr
| left=expr op=STAR right=expr
| left=expr op=SLASH right=expr
| left=expr op=PLUS right=expr
| left=expr op=MINUS right=expr
| left=expr op=OTHER_OP right=expr;

group: single=literal| LBRACKET expression=expr RBRACKET;

literal: STRING | NUMBER| FLOAT_NUMBER | ID;

