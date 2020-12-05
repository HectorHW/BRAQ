lexer grammar BRAQLexer;

EQUALS: '=';

PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
MODULUS: '%';

AT_OPERATOR: '@';

GR: '>';
GE: '>=';
LS : '<';
LE: '<=';

EQ: '?=';
NE : '!=';

AND: 'and';
OR: 'or';
XOR: 'xor';

NOT: 'not';

NUMBER: [0-9]+;

DOUBLE_NUMBER: [0-9]+ '.' [0-9]+;

LBRACKET : '(';
RBRACKET : ')';

LCURLY: '{';
RCURLY: '}';

SEMICOLON: ';';
COLON: ':';
DOT: '.';

NEWLINE: ('\n'|'\r')+ -> skip;
SPACE: [ \t]+ -> skip;

VAR: 'var';
RETURN: 'return';
BREAK: 'break';
CONTINUE: 'continue';
WHILE: 'while';
FOR: 'for';
DEF: 'def';

IF: 'if';
ELSE: 'else';

STRING: '"' .*? '"';

ID: [A-Za-z_][A-Za-z0-9_]*;