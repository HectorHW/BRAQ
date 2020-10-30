parser grammar CParser;

options {tokenVocab = CLexer; }

program: (stmt*) EOF;

stmt: containing=expr SEMICOLON;

expr: left=expr op=MODULUS right=expr
| left=expr op=STAR right=expr
| left=expr op=SLASH right=expr
| left=expr op=PLUS right=expr
| left=expr op=MINUS right=expr
| grouping=group
| num=literal;

group: LBRACKET containing=expr RBRACKET;
literal: NUMBER;
