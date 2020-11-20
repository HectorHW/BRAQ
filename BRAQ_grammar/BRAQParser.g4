parser grammar BRAQParser;

options {tokenVocab = BRAQLexer; }

program: (stmt*) EOF;

block: LCURLY containing=stmt* RCURLY;

stmt: var_stmt
| expr_stmt
| containing_if=if_stmt;

if_stmt: IF cond=expr then_branch=block (ELSE else_branch=block)?;

// ID @ (params?) = expr
var_stmt: VAR id_name=ID (EQUALS assignee=expr)? SEMICOLON;
expr_stmt: expr SEMICOLON;




//expr: num=literal
//| call_exr=call
//| grouping=group
//| left=expr op=MODULUS right=expr
//| left=expr op=(STAR|SLASH) right=expr
//| left=expr op=(PLUS|MINUS) right=expr
//| left=expr op=(GR|GE|LS|LE) right=expr
//| left=expr op=(EQ|NE) right=expr
//| unary_not_op=NOT right=expr
//| left=expr op=AND right=expr
//| left=expr op=OR right=expr
//| left=expr op=XOR right=expr
//;

expr: containing=assign;

assign: id_name=ID EQUALS assignee=logical_or | assignee=logical_or;


logical_or: left=logical_or op=OR right=logical_xor | right=logical_xor;
logical_xor:  left=logical_xor op=XOR right=logical_and | right=logical_and;
logical_and:  left=logical_and op=AND right=logical_equal |  right=logical_equal;
logical_equal:  left=logical_equal op=(EQ|NE) right=logical_gr_le | right=logical_gr_le;
logical_gr_le:  left=addition op=(GR|GE|LS|LE) right=addition | right=addition;
addition:  left=addition op=(PLUS|MINUS) right=multiplication | right=multiplication;
multiplication:  left=multiplication op=(STAR|SLASH|MODULUS) (right_short_call=short_call | right_call=call | right_literal=literal) 
| (right_short_call=short_call | right_call=call | right_literal=literal);

call: calee=ID LBRACKET expr* RBRACKET;
short_call: calee=ID AT_OPERATOR (sc_arg=short_call|c_arg=call|l_arg=literal);



literal: num=NUMBER| double_num=DOUBLE_NUMBER|var_node_=var_node|str=STRING | group;

group: LBRACKET containing=expr RBRACKET;

//call: calee=ID AT_OPERATOR arguments=arg_list;
//call: calee=ID (AT_OPERATOR single_argument=call_or_literal | LBRACKET multiple_arguments=arg_list RBRACKET);

//call_or_literal: containing_call=call | containing_literal=literal;

//arg_list: expr* ;



var_node: id_name=ID;