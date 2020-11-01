parser grammar BRAQParser;

options {tokenVocab = BRAQLexer; }

program: (stmt*) EOF;

stmt: (containing_var=var_stmt_base
|containing_print=print_stmt_base
|containing_assign=assign_stmt_base
|containing_read=read_stmt_base) SEMICOLON;

var_stmt_base: VAR id_name=ID (EQUALS assignee=expr)?;

print_stmt_base: PRINT arg=expr;

assign_stmt_base: id_name=ID EQUALS assignee=expr;

read_stmt_base: READ arg=var_node;

expr: left=expr op=MODULUS right=expr
| left=expr op=STAR right=expr
| left=expr op=SLASH right=expr
| left=expr op=PLUS right=expr
| left=expr op=MINUS right=expr
| grouping=group
| call_exr=call
| num=literal;

group: LBRACKET containing=expr RBRACKET;

call: calee=ID AT_OPERATOR arguments=arg_list;
arg_list: single_argument=expr | LBRACKET (expressions=expr*) RBRACKET;

literal: num=NUMBER|var_node_=var_node|str=STRING;

var_node: id_name=ID;