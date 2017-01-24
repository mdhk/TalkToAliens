/* ----- General ----- */
% write_csv/0: Write all sentences in csv format
write_csv :-
    % Write header
	write("sentence,obj1,locX1,locY1,dirX1,dirY1,obj2,locX2,locY2,dirX2,dirY2"),
    nl,
    !, % don't go back
    
    % Write column: sentence
    sentence([Sem1, Sem2], SentList, []),
    to_string(SentList, SentString),
    write(SentString),
    write(","),
    
    % Write column: obj1
    get_value(Sem1, obj, Obj1),
    write(Obj1),
    write(","),
    
    % Write columns: locX1, locY1
    get_value(Sem1, loc, Loc1),
    Loc1 = [LocX1, LocY1],
	write(LocX1),
    write(","),
    write(LocY1),
    write(","),
    
    % Write columns: dirX1, dirY1
    get_value(Sem1, dirA, Dir1),
    Dir1 = [DirX1, DirY1],
    write(DirX1),
    write(","),
    write(DirY1),
    write(","),
    
    % Write column: obj2
    get_value(Sem2, obj, Obj2),
    write(Obj2),
    write(","),
    
    % Write columns: locX2, locY2
    get_value(Sem2, loc, Loc2),
    Loc2 = [LocX2, LocY2],
    write(LocX2),
    write(","),
    write(LocY2),
    write(","),
    
    % Write columns: dirX2, dirY2
    get_value(Sem2, dirA, Dir2),
    Dir2 = [DirX2, DirY2],
    write(DirX2),
    write(","),
    write(DirY2),
    
    % Break line, go to next result
    nl,
    fail.

% write_csv_file/0: Same as write_csv/0, but outputs to file
write_csv_file :-
    % Create file
    open("../output/output.csv", write, Stream1),
    
    % Write header
	write(Stream1, "sentence,obj1,locX1,locY1,dirX1,dirY1,obj2,locX2,locY2,dirX2,dirY2"),
    nl(Stream1),
    
    % Close file
    close(Stream1),
    
    % Don't go back
    !,
    
    % Generate sentence
    sentence([Sem1, Sem2], SentList, []),

    % Open file (again)
    open("../output/output.csv", append, Stream2),

    % Write column: sentence
    to_string(SentList, SentString),
    write(Stream2, SentString),
    write(Stream2, ","),
    
    % Write column: obj1
    get_value(Sem1, obj, Obj1),
    write(Stream2, Obj1),
    write(Stream2, ","),
    
    % Write columns: locX1, locY1
    get_value(Sem1, loc, Loc1),
    Loc1 = [LocX1, LocY1],
	write(Stream2, LocX1),
    write(Stream2, ","),
    write(Stream2, LocY1),
    write(Stream2, ","),
    
    % Write columns: dirX1, dirY1
    get_value(Sem1, dirA, Dir1),
    Dir1 = [DirX1, DirY1],
    write(Stream2, DirX1),
    write(Stream2, ","),
    write(Stream2, DirY1),
    write(Stream2, ","),
    
    % Write column: obj2
    get_value(Sem2, obj, Obj2),
    write(Stream2, Obj2),
    write(Stream2, ","),
    
    % Write columns: locX2, locY2
    get_value(Sem2, loc, Loc2),
    Loc2 = [LocX2, LocY2],
    write(Stream2, LocX2),
    write(Stream2, ","),
    write(Stream2, LocY2),
    write(Stream2, ","),
    
    % Write columns: dirX2, dirY2
    get_value(Sem2, dirA, Dir2),
    Dir2 = [DirX2, DirY2],
    write(Stream2, DirX2),
    write(Stream2, ","),
    write(Stream2, DirY2),
    nl(Stream2),
    
    % Close file again
    close(Stream2),
    
    % Go to next result
    fail.

% to_string/2: Convert list to string
to_string(List, String) :-
    List = [],
    String = "". % stop if List is empty

to_string(List, String) :-
    List = [" "],
    to_string([], String). % remove trailing whitespace

to_string(List, String) :-
    \+ List = [" "],
    List = [H|T],
    to_string(T, String1),
    string_concat(H, String1, String). % concatenate List items
    
/* ----- Grammar -----*/
% sentence/2: Sentence
sentence([Sem1, Sem2]) -->
    noun(NSem1),
    verb(VSem),
    noun(NSem2),
    {
    	% Define property lists for the two objects
    	Sem1 = [obj:Obj1, dirR:DirR1, dirA:DirA1, loc:Loc1],
      	Sem2 = [obj:Obj2, dirR:DirR2, dirA:DirA2, loc:Loc2],
      
    	% Determine objects
      	get_value(NSem1, obj, Obj1),
      	get_value(NSem2, obj, Obj2),
      
    	% Determine relative directions
		get_value(NSem1, dirR, DirR1),
      	get_value(NSem2, dirR, DirR2),
      
		% Determine locations
    	get_value(VSem, dirR, DirRV),
      	get_value(VSem, sameSide, SameSide),
      	get_locations(DirRV, Loc1, Loc2),
      	(
        	% Objects on same side river
        	SameSide = true,
            check_sameside(Loc1, Loc2);
        	
        	% Objects not on same side river
        	SameSide = false
        ),
      	
    	% Determine absolute directions
    	get_direction(Loc1, DirR1, DirA1),
        get_direction(Loc2, DirR2, DirA2)
    }.
    
% noun/2: Noun
noun([Obj, DirR]) -->
    nroot(Obj),
    locative(DirR),
    wbound.

% verb/2: Verb
verb([DirR, sameSide:true]) -->
    vpref,
    locative(DirR),
    wbound.

verb([DirR, sameSide:false]) -->
    locative(DirR),
    wbound.
    
% nroot/2: Nominal root
nroot(obj:chair) -->
    ["wu"].

nroot(obj:rabbit) -->
    ["zu"].

nroot(obj:frog) -->
    ["du"].

nroot(obj:duck) -->
    ["lu"].

% locative/2: Locative marker
locative(dirR:towards) -->
    ["ba"].

locative(dirR:away) -->
    ["pa"].

locative(dirR:upstream) -->
    ["bo"].

locative(dirR:downstream) -->
    ["po"].

% vpref/2: Verbal prefix
vpref -->
    ["ke"].

% wbound/2: Boundary between words
wbound -->
    [" "].

/* ----- Semantics helpers ----- */
% set_value/3: Set an attribute in the template to a value
get_value(List, _, _) :-
    List = []. % case 1: list is empty

get_value(List, Attr, Val) :-
    List = [H|T],
    H = Attr1:_,
    \+ Attr1 = Attr, % case 2: attribute not found
    get_value(T, Attr, Val). % continue with rest of list

get_value(List, Attr, Val) :-
    List = [H|T],
    H = Attr:Val, % case 3: attribute found, get value
    get_value(T, Attr, Val). % continue with rest of list

% get_direction/3
get_direction(Loc1, DirR, DirA) :-
    Loc1 = [X, Y],
    (
    	% upstream
    	DirR = upstream,
        NewX is X + 1,
        DirA = [NewX, Y];
    
    	% downstream
    	DirR = downstream,
        NewX is X - 1,
        DirA = [NewX, Y];
    
    	% towards river
    	DirR = towards,
        (
        	% north of river
        	Y < 5,
            NewY is Y + 1,
            DirA = [X, NewY];
        
        	% south of river
        	Y > 4,
            NewY is Y - 1,
            DirA = [X, NewY]
        );
    	
    	% away from river
    	DirR = away,
        (
        	% north of river
        	Y < 5,
            NewY is Y - 1,
            DirA = [X, NewY];
        
        	% south of river
        	Y > 4,
            NewY is Y + 1,
            DirA = [X, NewY]
        )
    ).

get_locations(DirR, Loc1, Loc2) :-
    % Location = [X, Y]
    Loc1 = [Loc1X, Loc1Y],
    Loc2 = [Loc2X, Loc2Y],
    
    % 1 =< X =< 4, 1 <= Y <= 8
    between(1, 4, Loc1X),
    between(1, 8, Loc1Y),
    between(1, 4, Loc2X),
    between(1, 8, Loc2Y),
    
    % Calculate distances from river
    River1 is Loc1Y - 4.5,
    River2 is Loc2Y - 4.5,
    abs(River1, River1Abs),
    abs(River2, River2Abs),
    
    % Constrain positions based on DirR
    (
    	% upstream
    	DirR = upstream,
        Loc1X > Loc2X;
    
    	% downstream
    	DirR = downstream,
        Loc1X < Loc2X;
    
    	% towards river
    	DirR = towards,
        River1Abs < River2Abs;
        
    	% away from river
    	DirR = away,
        River1Abs > River2Abs
    ).

check_sameside(Loc1, Loc2) :-
    Loc1 = [_, Loc1Y],
    Loc2 = [_, Loc2Y],
    (
    	% Both north of river
    	Loc1Y < 4.5,
        Loc2Y < 4.5;
    	
    	% Both south of river
    	Loc1Y > 4.5,
        Loc2Y > 4.5
    ).
