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

LBRACKET : '(';
RBRACKET : ')';

LCURLY: '{';
RCURLY: '}';

SEMICOLON: ';';

NEWLINE: ('\n'|'\r')+ -> skip;
SPACE: [ \t]+ -> skip;

VAR: 'var';
PRINT: 'print';
READ: 'read';

IF: 'if';
ELSE: 'else';

STRING: '"' .*? '"';

ID: [A-Za-z_][A-Za-z0-9_]*;