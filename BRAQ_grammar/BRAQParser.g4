parser grammar BRAQParser;

options {tokenVocab = BRAQLexer; }

program: (function_def_stmt*) EOF;

block: LCURLY containing=stmt* RCURLY | single_stmt=stmt;

stmt: var_stmt
| expr_stmt
| containing_if=if_stmt
| while_loop_stmt
| break_stmt
| continue_stmt
| return_stmt
| containing_def=function_def_stmt;

if_stmt: IF cond=expr then_branch=block (ELSE else_branch=block)?;
while_loop_stmt: WHILE (cond=logical_or)? body=block;

function_def_stmt: DEF id_name=ID (LBRACKET typed_id* RBRACKET)? (COLON of_type=ID)? function_body=block;

typed_id: id_name=ID COLON type_name=ID;

break_stmt: BREAK SEMICOLON;
continue_stmt: CONTINUE SEMICOLON;

return_stmt: RETURN (return_value=expr)? SEMICOLON;

// ID @ (params?) = expr
var_stmt: VAR id_name=ID (EQUALS assignee=expr)? SEMICOLON;
expr_stmt: containing=assign SEMICOLON;




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



assign: id_name=ID EQUALS assignee=expr | assignee=expr; //does not return value

expr: containing=logical_or; // returns value

logical_or: left=logical_or op=OR right=logical_xor | right=logical_xor;
logical_xor:  left=logical_xor op=XOR right=logical_and | right=logical_and;
logical_and:  left=logical_and op=AND right=logical_equal |  right=logical_equal;
logical_equal:  left=logical_equal op=(EQ|NE) right=logical_gr_le | right=logical_gr_le;
logical_gr_le:  left=addition op=(GR|GE|LS|LE) right=addition | right=addition;
addition:  left=addition op=(PLUS|MINUS) right=multiplication | right=multiplication;
multiplication:  left=multiplication op=(STAR|SLASH|MODULUS) right=unary_not_neg 
| right=unary_not_neg;
unary_not_neg: op=(NOT|MINUS) (right_short_call=short_call | right_call=call | right_literal=literal) 
| (right_short_call=short_call | right_call=call | right_literal=literal);
call: calee=ID LBRACKET expr* RBRACKET;
short_call: calee=ID AT_OPERATOR (sc_arg=short_call|c_arg=call|l_arg=literal);



literal: num=NUMBER| double_num=DOUBLE_NUMBER|var_node_=var_node|str=STRING | containing_group=group;

group: LBRACKET containing=expr RBRACKET;

//call: calee=ID AT_OPERATOR arguments=arg_list;
//call: calee=ID (AT_OPERATOR single_argument=call_or_literal | LBRACKET multiple_arguments=arg_list RBRACKET);

//call_or_literal: containing_call=call | containing_literal=literal;

//arg_list: expr* ;



var_node: id_name=ID;