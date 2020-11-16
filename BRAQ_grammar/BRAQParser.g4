parser grammar BRAQParser;

options {tokenVocab = BRAQLexer; }

program: (stmt*) EOF;

block: LCURLY containing=stmt* RCURLY;

stmt: ((containing_var=var_stmt_base
|containing_print=print_stmt_base
|containing_assign=assign_stmt_base
|containing_read=read_stmt_base) SEMICOLON)
| containing_if=if_stmt;

if_stmt: IF cond=expr then_branch=block (ELSE else_branch=block)?;

// ID @ (params?) = expr
var_stmt_base: VAR id_name=ID (EQUALS assignee=expr)?;

print_stmt_base: PRINT arg=expr;

assign_stmt_base: id_name=ID EQUALS assignee=expr;

read_stmt_base: READ arg=var_node;

expr: num=literal
| call_exr=call
| grouping=group
| left=expr op=MODULUS right=expr
| left=expr op=(STAR|SLASH) right=expr
| left=expr op=(PLUS|MINUS) right=expr
| left=expr op=(GR|GE|LS|LE) right=expr
| left=expr op=(EQ|NE) right=expr
| unary_not_op=NOT right=expr
| left=expr op=AND right=expr
| left=expr op=OR right=expr
| left=expr op=XOR right=expr
;



group: LBRACKET containing=expr RBRACKET;

//call: calee=ID AT_OPERATOR arguments=arg_list;
call: calee=ID (AT_OPERATOR single_argument=call_or_literal | LBRACKET multiple_arguments=arg_list RBRACKET);

call_or_literal: containing_call=call | containing_literal=literal;

arg_list: expr* ;

literal: num=NUMBER| double_num=DOUBLE_NUMBER|var_node_=var_node|str=STRING;

var_node: id_name=ID;