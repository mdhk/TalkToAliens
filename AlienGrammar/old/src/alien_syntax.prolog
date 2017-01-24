/* ----- General ----- */
write_all :-
    sentence(X, []),
    write(X),
    nl,
    fail.


/* ----- Grammar -----*/
% sentence/2: Sentence
sentence -->
    noun,
    verb,
    noun.
    
% noun/2: Noun
noun -->
    nroot,
    locative,
    wbound.

% verb/2: Verb
verb -->
    vpref,
    locative,
    wbound.
    
% nroot/2: Nominal root
nroot -->
    [wu]. % meaning: chair

nroot -->
    [zu]. % meaning: rabbit

% locative/2: Locative marker
locative -->
    [ba]. % meaning: towards river

locative -->
    [pa]. % meaning: away from river

locative -->
    [bo]. % meaning: upstream

locative -->
    [po]. % meaning: downstream

% vpref/2: Verbal prefix
vpref -->
    [ke].

% wbound/2: Boundary between words
wbound -->
    [#].