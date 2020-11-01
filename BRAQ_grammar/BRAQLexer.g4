lexer grammar BRAQLexer;

EQUALS: '=';

PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
MODULUS: '%';

NUMBER: [0-9]+;

LBRACKET : '(';
RBRACKET : ')';

SEMICOLON: ';';

NEWLINE: ('\n'|'\r')+ -> skip;
SPACE: [ \t]+ -> skip;

VAR: 'var';
PRINT: 'print';
READ: 'read';

STRING: '"' .*? '"';

ID: [A-Za-z_][A-Za-z0-9_]*;