lexer grammar BRAQLexer;


COMMENT: '\\' .*? -> skip;

SHORT_EQUALS: (DOUBLE_STAR|PLUS|MINUS|STAR|SLASH|OTHER_OP) EQUALS;

EQUALS: '=';

LBRACKET: '(';
RBRACKET: ')';
LSQBRACKET: '[';
RSQBRACKET: ']';
LBRACE: '{';
RBRACE: '}';

SEMICOLON: ';';

DOUBLE_STAR: '**';
PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';

DEF: 'def';
VAR: 'var';
WHILE: 'while';

NUMBER: [0-9]+;
FLOAT_NUMBER: [0-9] '.' [0-9]+;

ID: [_a-zA-Z][_a-zA-Z0-9]*;

OTHER_OP: [+\-=*\\|&@><^:]+;

STRING: '"' .*? '"';

WHITESPACE: (' '|'\t')+ -> skip;
NEWLINE: ('\r'?'\n')+ -> skip;