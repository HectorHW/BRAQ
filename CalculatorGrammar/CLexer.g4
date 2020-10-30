lexer grammar CLexer;

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