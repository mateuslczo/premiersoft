å
/C:\owner-projects\CooperAna\BankMore\Program.cs
	namespace 	
BankMore
 
{ 
public 
class 
Program 
{ 
public 
static	 
void 
Main 
( 
string  
[  !
]! "
args# '
)' (
{ 
var		 
builder		 
=		 
WebApplication		 
.		  
CreateBuilder		  -
(		- .
args		. 2
)		2 3
;		3 4
builder 

.
 
Services 
. 
AddControllers "
(" #
)# $
;$ %
builder 

.
 
Services 
. #
AddEndpointsApiExplorer +
(+ ,
), -
;- .
builder 

.
 
Services 
. 
AddSwaggerGen !
(! "
)" #
;# $
builder 

.
 
Services 
. 
	AddScoped 
< 
DapperContext +
>+ ,
(, -
dapProvider- 8
=>9 ;
{ 
var "
oracleConnecionsString 
=  
$str! U
;U V
return 

new 
DapperContext 
( "
oracleConnecionsString 3
)3 4
;4 5
} 
) 
; 
var 
app 

= 
builder 
. 
Build 
( 
) 
; 
if 
( 
app 

.
 
Environment 
. 
IsDevelopment $
($ %
)% &
)& '
{ 
app 
. 

UseSwagger 
( 
) 
; 
app 
. 
UseSwaggerUI 
( 
) 
; 
}   
app"" 
."" 
UseHttpsRedirection"" 
("" 
)"" 
;"" 
app$$ 
.$$ 
UseAuthorization$$ 
($$ 
)$$ 
;$$ 
app'' 
.'' 
MapControllers'' 
('' 
)'' 
;'' 
app)) 
.)) 
Run)) 

())
 
))) 
;)) 
}** 
}++ 
},, 
nC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\WriteRepository\TransactionWriteRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
.. /
WriteRepository/ >
{ 
public

 
class

 $
TransacaoWriteRepository

 &
:

' ('
ITransactionWriteRepository

( C
{ 
private 	
readonly
 
DapperContext  
_context! )
;) *
public $
TransacaoWriteRepository	 !
(! "
DapperContext" /
context0 7
)7 8
{ 
_context 
= 
context 
; 
} 
public 
async	 
Task (
AddTransactionByAccountAsync 0
(0 1!
TransactionWriteModel1 F
	transacaoG P
)P Q
{ 
const 
string	 
sql 
= 
$str 5
;5 6
await 
_context	 
. 

Connection 
. 
ExecuteAsync )
() *
sql* -
,- .
	transacao/ 8
,8 9
_context: B
.B C
TransactionC N
)N O
;O P
} 
}   
}!! š
jC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\WriteRepository\AccountWriteRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
.. /
ReadRepository/ =
{ 
public		 
class		 "
AccountWriteRepository		 $
:		% &#
IAccountWriteRepository		& =
{

 
private 	
readonly
 
DapperContext  
_context! )
;) *
public "
AccountWriteRepository	 
(  
DapperContext  -
context. 5
)5 6
{ 
_context 
= 
context 
; 
} 
public 
async	 
Task 
InsertAccountAsync &
(& '
AccountWriteModel' 8
account9 @
)@ A
{ 
const 
string	 
sql 
= 
$str* 	
;**	 

await,, 
_context,,	 
.,, 

Connection,, 
.,, 
ExecuteAsync,, )
(,,) *
sql,,* -
,,,- .
account,,/ 6
,,,6 7
_context,,8 @
.,,@ A
Transaction,,A L
),,L M
;,,M N
}-- 
public// 
async//	 
Task// 
UpdateBalanceAsync// &
(//& '
Guid//' +
contaId//, 3
,//3 4
decimal//5 <
	novoSaldo//= F
)//F G
{00 
const11 
string11	 
sql11 
=11 
$str14 
;44  
await66 
_context66	 
.66 

Connection66 
.66 
ExecuteAsync66 )
(66) *
sql66* -
,66- .
new66/ 2
{77 
ContaId88 
=88 
contaId88 
,88 
	NovoSaldo99 
=99 
	novoSaldo99 
,99 
UltimaAtualizacao:: 
=:: 
DateTime::  
.::  !
UtcNow::! '
};; 
,;; 
_context;; 
.;; 
Transaction;; 
);; 
;;; 
}<< 
}== 
}>> †
lC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\ReadRepository\TransactionReadRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
.. /
ReadRepository/ =
{ 
public

 
class

 %
TransactionReadRepository

 '
:

( )&
ITransactionReadRepository

) C
{ 
private 	
readonly
 
DapperContext  
_context! )
;) *
public %
TransactionReadRepository	 "
(" #
DapperContext# 0
context1 8
)8 9
{ 
_context 
= 
context 
; 
} 
public 
async	 
Task 
< 
IEnumerable 
<   
TransactionReadModel  4
>4 5
>5 6(
GetTransactionByAccountAsync7 S
(S T
Guid 
contaId 
, 
DateTime 
? 

dataInicio %
,% &
DateTime' /
?/ 0
dataFim1 8
)8 9
{ 
var 
sql 

= 
$str !
;! "
var 

parameters 
= 
new 
DynamicParameters )
() *
)* +
;+ ,

parameters 
. 
Add 
( 
$str 
, 
contaId $
)$ %
;% &
if 
( 

dataInicio 
. 
HasValue 
) 
{ 
sql   
+=   

$str   .
;  . /

parameters!! 
.!! 
Add!! 
(!! 
$str!! 
,!!  

dataInicio!!! +
.!!+ ,
Value!!, 1
)!!1 2
;!!2 3
}"" 
if$$ 
($$ 
dataFim$$ 
.$$ 
HasValue$$ 
)$$ 
{%% 
sql&& 
+=&& 

$str&& +
;&&+ ,

parameters'' 
.'' 
Add'' 
('' 
$str'' 
,'' 
dataFim'' %
.''% &
Value''& +
)''+ ,
;'', -
}(( 
sql** 
+=** 	
$str**
 (
;**( )
return,, 	
await,,
 
_context,, 
.,, 

Connection,, #
.,,# $

QueryAsync,,$ .
<,,. / 
TransactionReadModel,,/ C
>,,C D
(,,D E
sql,,E H
,,,H I

parameters,,J T
),,T U
;,,U V
}-- 
}// 
}00 ‹
hC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\ReadRepository\AccountReadRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
.. /
ReadRepository/ =
{ 
public		 
class		 !
AccountReadRepository		 #
:		$ %"
IAccountReadRepository		% ;
{

 
private 	
readonly
 
DapperContext  
_context! )
;) *
public !
AccountReadRepository	 
( 
DapperContext ,
context- 4
)4 5
{ 
_context 
= 
context 
; 
} 
public 
async	 
Task 
< 
AccountReadModel $
>$ %
GetAccountByIdAsync& 9
(9 :
Guid: >
contaId? F
)F G
{ 
const 
string	 
sql 
= 
$str 
;  
return 	
await
 
_context 
. 

Connection #
.# $$
QueryFirstOrDefaultAsync$ <
<< =
AccountReadModel= M
>M N
(N O
sql 
, 
new	 
{ 
ContaId 
= 
contaId  
}! "
)" #
;# $
} 
} 
}FF Ä@
ZC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\GenericWriteRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
{ 
public		 
abstract		 
class		 "
GenericWriteRepository		 -
<		- .
TEntity		. 5
,		5 6
TKey		7 ;
>		; <
:		= >#
IGenericWriteRepository		? V
<		V W
TEntity		W ^
,		^ _
TKey		` d
>		d e
where

 
TEntity

 
:

 
class

 
{ 
	protected 
readonly 
IDbConnection (
_connection) 4
;4 5
	protected 
readonly 
IDbTransaction )
_transaction* 6
;6 7
	protected 
readonly 
string !

_tableName" ,
;, -
	protected 
readonly 
string !

_keyColumn" ,
;, -
	protected "
GenericWriteRepository (
(( )
DapperContext) 6
context7 >
,> ?
string@ F
	tableNameG P
,P Q
stringR X
	keyColumnY b
=c d
$stre i
)i j
{ 	
_connection 
= 
context !
.! "

Connection" ,
;, -
_transaction 
= 
context "
." #
Transaction# .
;. /

_tableName 
= 
	tableName "
;" #

_keyColumn 
= 
	keyColumn "
;" #
} 	
public 
async 
Task 
< 
TEntity !
>! "
InsertAsync# .
(. /
TEntity/ 6
entity7 =
)= >
{ 	
var 

properties 
= 
typeof #
(# $
TEntity$ +
)+ ,
., -
GetProperties- :
(: ;
); <
. 
Where 
( 
p 
=> 
p  !
.! "
Name" &
!=' )

_keyColumn* 4
||5 7
!8 9
p9 :
.: ;
GetGetMethod; G
(G H
)H I
.I J
	IsVirtualJ S
)S T
. 
ToArray 
( 
) 
; 
var   
columnNames   
=   
string   $
.  $ %
Join  % )
(  ) *
$str  * .
,  . /

properties  0 :
.  : ;
Select  ; A
(  A B
p  B C
=>  D F
p  G H
.  H I
Name  I M
)  M N
)  N O
;  O P
var!! 
parameterNames!! 
=!!  
string!!! '
.!!' (
Join!!( ,
(!!, -
$str!!- 1
,!!1 2

properties!!3 =
.!!= >
Select!!> D
(!!D E
p!!E F
=>!!G I
$"!!J L
$str!!L M
{!!M N
p!!N O
.!!O P
Name!!P T
}!!T U
"!!U V
)!!V W
)!!W X
;!!X Y
var"" 
sql"" 
="" 
$@""" 
$str"# 
{## 

_tableName## 
}## 
$str## !
{##! "
columnNames##" -
}##- .
$str#$. 
{$$ 
parameterNames$$ 
}$$ 
$str$% 
{%% 

_keyColumn%% 
}%% 
$str%% $
{%%$ %

_keyColumn%%% /
}%%/ 0
$str%%0 6
"%%6 7
;%%7 8
var'' 

parameters'' 
='' 
new''  
DynamicParameters''! 2
(''2 3
entity''3 9
)''9 :
;'': ;

parameters(( 
.(( 
Add(( 
((( 
$"(( 
{(( 

_keyColumn(( (
}((( )
$str(() /
"((/ 0
,((0 1
dbType((2 8
:((8 9
DbType((: @
.((@ A
Int32((A F
,((F G
	direction((H Q
:((Q R
ParameterDirection((S e
.((e f
Output((f l
)((l m
;((m n
var** 
insertedEntity** 
=**  
await**! &
_connection**' 2
.**2 3$
QueryFirstOrDefaultAsync**3 K
<**K L
TEntity**L S
>**S T
(**T U
sql**U X
,**X Y
entity**Z `
,**` a
_transaction**b n
)**n o
;**o p
return,, 
insertedEntity,, !
??,," $
entity,,% +
;,,+ ,
}-- 	
public// 
async// 
Task// 
<// 
TEntity// !
>//! "
UpdateAsync//# .
(//. /
TEntity/// 6
entity//7 =
)//= >
{00 	
var11 

properties11 
=11 
typeof11 #
(11# $
TEntity11$ +
)11+ ,
.11, -
GetProperties11- :
(11: ;
)11; <
.22 
Where22 
(22 
p22 
=>22 
p22 
.22 
Name22 "
!=22# %

_keyColumn22& 0
&&221 3
!224 5
p225 6
.226 7
GetGetMethod227 C
(22C D
)22D E
.22E F
	IsVirtual22F O
)22O P
.33 
ToArray33 
(33 
)33 
;33 
var55 
	setClause55 
=55 
string55 "
.55" #
Join55# '
(55' (
$str55( ,
,55, -

properties55. 8
.558 9
Select559 ?
(55? @
p55@ A
=>55B D
$"55E G
{55G H
p55H I
.55I J
Name55J N
}55N O
$str55O S
{55S T
p55T U
.55U V
Name55V Z
}55Z [
"55[ \
)55\ ]
)55] ^
;55^ _
var77 
sql77 
=77 
$"77 
$str77 
{77  

_tableName77  *
}77* +
$str77+ 0
{770 1
	setClause771 :
}77: ;
$str77; B
{77B C

_keyColumn77C M
}77M N
$str77N R
{77R S

_keyColumn77S ]
}77] ^
"77^ _
;77_ `
var99 
rowsAffected99 
=99 
await99 $
_connection99% 0
.990 1
ExecuteAsync991 =
(99= >
sql99> A
,99A B
entity99C I
,99I J
_transaction99K W
)99W X
;99X Y
if;; 
(;; 
rowsAffected;; 
==;; 
$num;;  !
);;! "
{<< 
return>> 
default>> 
;>> 
}?? 
returnAA 
entityAA 
;AA 
}BB 	
publicDD 
asyncDD 
TaskDD 
<DD 
boolDD 
>DD 
DeleteAsyncDD  +
(DD+ ,
TKeyDD, 0
idDD1 3
)DD3 4
{EE 	
varFF 
sqlFF 
=FF 
$"FF 
$strFF $
{FF$ %

_tableNameFF% /
}FF/ 0
$strFF0 7
{FF7 8

_keyColumnFF8 B
}FFB C
$strFFC I
"FFI J
;FFJ K
varGG 
rowsAffectedGG 
=GG 
awaitGG $
_connectionGG% 0
.GG0 1
ExecuteAsyncGG1 =
(GG= >
sqlGG> A
,GGA B
newGGC F
{GGG H
idGGI K
}GGL M
,GGM N
_transactionGGO [
)GG[ \
;GG\ ]
ifII 
(II 
rowsAffectedII 
==II 
$numII  !
)II! "
returnJJ 
falseJJ 
;JJ 
returnKK 
trueKK 
;KK 
}LL 	
}MM 
}NN ö 
YC:\owner-projects\CooperAna\BankMore\Infrastructure\Repositories\GenericReadRepository.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
Repositories" .
{ 
public		 
abstract		 
class		 !
GenericReadRepository		 ,
<		, -
TEntity		- 4
,		4 5
TKey		6 :
>		: ;
:		< =
IReadRepository		> M
<		M N
TEntity		N U
,		U V
TKey		W [
>		[ \
where

 
TEntity

 
:

 
class

 
{ 
	protected 
readonly 
IDbConnection (
_connection) 4
;4 5
	protected 
readonly 
IDbTransaction )
_transaction* 6
;6 7
	protected 
readonly 
string !

_tableName" ,
;, -
	protected 
readonly 
string !

_keyColumn" ,
;, -
	protected !
GenericReadRepository '
(' (
DapperContext( 5
context6 =
,= >
string? E
	tableNameF O
,O P
stringQ W
	keyColumnX a
=b c
$strd h
)h i
{ 	
_connection 
= 
context !
.! "

Connection" ,
;, -
_transaction 
= 
context "
." #
Transaction# .
;. /

_tableName 
= 
	tableName "
;" #

_keyColumn 
= 
	keyColumn "
;" #
} 	
public 
Task 
< 
TEntity 
> 
GetRecordByIdAsync /
(/ 0
TKey0 4
id5 7
)7 8
{ 	
var 
sql 
= 
$" 
$str &
{& '

_tableName' 1
}1 2
$str2 9
{9 :

_keyColumn: D
}D E
$strE K
"K L
;L M
return 
_connection 
. $
QueryFirstOrDefaultAsync 7
<7 8
TEntity8 ?
>? @
(@ A
sqlA D
,D E
newF I
{J K
idL N
}O P
,P Q
_transactionR ^
)^ _
;_ `
} 	
public   
Task   
<   
IEnumerable   
<    
TEntity    '
>  ' (
>  ( )
GetAllRecordAsync  * ;
(  ; <
)  < =
{!! 	
var"" 
sql"" 
="" 
$""" 
$str"" &
{""& '

_tableName""' 1
}""1 2
"""2 3
;""3 4
return## 
_connection## 
.## 

QueryAsync## )
<##) *
TEntity##* 1
>##1 2
(##2 3
sql##3 6
,##6 7
transaction##8 C
:##C D
_transaction##E Q
)##Q R
;##R S
}$$ 	
public&& 
async&& 
Task&& 
<&& 
bool&& 
>&& 
HasRecordAsync&&  .
(&&. /
TKey&&/ 3
id&&4 6
)&&6 7
{'' 	
var(( 
sql(( 
=(( 
$"(( 
$str(( -
{((- .

_tableName((. 8
}((8 9
$str((9 @
{((@ A

_keyColumn((A K
}((K L
$str((L R
"((R S
;((S T
var)) 
count)) 
=)) 
await)) 
_connection)) )
.))) *
ExecuteScalarAsync))* <
<))< =
int))= @
>))@ A
())A B
sql))B E
,))E F
new))G J
{))K L
id))M O
}))P Q
,))Q R
_transaction))S _
)))_ `
;))` a
return** 
count** 
>** 
$num** 
;** 
}++ 	
},, 
}-- š
pC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IWriteRepository\IAccountWriteRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
.2 3
IWriteRepository3 C
{ 
public 
	interface #
IAccountWriteRepository )
{		 
Task 
InsertAccountAsync 
( 
AccountWriteModel +
account, 3
)3 4
;4 5
Task 
UpdateBalanceAsync 
( 
Guid 
contaId &
,& '
decimal( /
	novoSaldo0 9
)9 :
;: ;
} 
} æ
rC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IReadRepository\ITransactionReadRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
.2 3
IReadRepository3 B
{ 
public 
	interface &
ITransactionReadRepository ,
{ 
Task 
< 
IEnumerable 
<  
TransactionReadModel '
>' (
>( )(
GetTransactionByAccountAsync* F
(F G
GuidG K
contaIdL S
,S T
DateTimeU ]
?] ^

dataInicio_ i
,i j
DateTimek s
?s t
dataFimu |
)| }
;} ~
}		 
}

 £

[C:\owner-projects\CooperAna\BankMore\Infrastructure\EventSourcing\Models\EventStoreModel.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
EventSourcing" /
./ 0
Models0 6
{ 
public 

class 
EventStoreModel  
{ 
public 
string 
Id 
{ 
get 
; 
set  #
;# $
}% &
public 
string 
AggregateId !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
	EventType 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
string		 
	EventData		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
int

 
Version

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 
DateTime 

OccurredOn "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} ì7
OC:\owner-projects\CooperAna\BankMore\Infrastructure\EventSourcing\EventStore.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
EventSourcing" /
{ 
public

 
class

 

EventStore

 
:

 
IEventStore

 &
{ 
private 
readonly 
DapperContext &
_context' /
;/ 0
public 

EventStore 
( 
DapperContext '
context( /
)/ 0
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
AppendEventsAsync +
(+ ,
Guid, 0
aggregateId1 <
,< =
IEnumerable> I
<I J
IEventDomainJ V
>V W
eventsX ^
,^ _
int` c
expectedVersiond s
)s t
{ 	
const 
string 
sql 
= 
$str V
;V W
var 
version 
= 
expectedVersion )
+* +
$num, -
;- .
foreach 
( 
var 
evento 
in  "
events# )
)) *
{ 
var 
	eventData 
= 
JsonSerializer  .
.. /
	Serialize/ 8
(8 9
evento9 ?
,? @
eventoA G
.G H
GetTypeH O
(O P
)P Q
)Q R
;R S
var 

parameters 
=  
new! $
{   
Id!! 
=!! 
evento!! 
.!!  
EventId!!  '
,!!' (
AggregateId"" 
=""  !
aggregateId""" -
,""- .
evento## 
.## 
	EventType## $
,##$ %
	EventData$$ 
=$$ 
	eventData$$  )
,$$) *
Version%% 
=%% 
version%% %
++%%% '
,%%' (
evento&& 
.&& 

OccurredOn&& %
}'' 
;'' 
await)) 
_context)) 
.)) 

Connection)) )
.))) *
ExecuteAsync))* 6
())6 7
sql))7 :
,)): ;

parameters))< F
,))F G
_context))H P
.))P Q
Transaction))Q \
)))\ ]
;))] ^
}** 
}++ 	
public-- 
async-- 
Task-- 
<-- 
IEnumerable-- %
<--% &
IEventDomain--& 2
>--2 3
>--3 4
GetEventsAsync--5 C
(--C D
Guid--D H
aggregateId--I T
)--T U
{.. 	
const// 
string// 
sql// 
=// 
$str/3 5
;335 6
var55 
events55 
=55 
await55 
_context55 '
.55' (

Connection55( 2
.552 3

QueryAsync553 =
<55= >
EventStoreModel55> M
>55M N
(55N O
sql55O R
,55R S
new55T W
{55X Y
AggregateId55Z e
=55f g
aggregateId55h s
}55t u
)55u v
;55v w
return77 
events77 
.77 
Select77  
(77  !
ToDomainEvent77! .
)77. /
;77/ 0
}88 	
public:: 
async:: 
Task:: 
<:: 
IEnumerable:: %
<::% &
IEventDomain::& 2
>::2 3
>::3 4 
GetEventsByTypeAsync::5 I
(::I J
string::J P
	eventType::Q Z
)::Z [
{;; 	
const== 
string== 
sql== 
=== 
$str=A 5
;AA5 6
varCC 
eventsCC 
=CC 
awaitCC 
_contextCC '
.CC' (

ConnectionCC( 2
.CC2 3

QueryAsyncCC3 =
<CC= >
EventStoreModelCC> M
>CCM N
(CCN O
sqlCCO R
,CCR S
newCCT W
{CCX Y
	EventTypeCCZ c
=CCd e
	eventTypeCCf o
}CCp q
)CCq r
;CCr s
varEE 
domainEventsEE 
=EE 
newEE "
ListEE# '
<EE' (
IEventDomainEE( 4
>EE4 5
(EE5 6
)EE6 7
;EE7 8
foreachGG 
(GG 
varGG 
eventStoreModelGG (
inGG) +
eventsGG, 2
)GG2 3
{HH 
varII 
typeII 
=II 
TypeII 
.II  
GetTypeII  '
(II' (
$"II( *
$strII* A
{IIA B
eventStoreModelIIB Q
.IIQ R
	EventTypeIIR [
}II[ \
$strII\ m
"IIm n
)IIn o
;IIo p
ifJJ 
(JJ 
typeJJ 
==JJ 
nullJJ  
)JJ  !
throwKK 
newKK %
InvalidOperationExceptionKK 7
(KK7 8
$"KK8 :
$strKK: Y
{KKY Z
eventStoreModelKKZ i
.KKi j
	EventTypeKKj s
}KKs t
"KKt u
)KKu v
;KKv w
varMM 
domainEventMM 
=MM  !
(MM" #
IEventDomainMM# /
)MM/ 0
JsonSerializerMM0 >
.MM> ?
DeserializeMM? J
(MMJ K
eventStoreModelMMK Z
.MMZ [
	EventDataMM[ d
,MMd e
typeMMf j
)MMj k
;MMk l
domainEventsNN 
.NN 
AddNN  
(NN  !
domainEventNN! ,
)NN, -
;NN- .
}OO 
returnQQ 
domainEventsQQ 
;QQ  
}RR 	
privateUU 
IEventDomainUU 
ToDomainEventUU *
(UU* +
EventStoreModelUU+ :

eventStoreUU; E
)UUE F
{VV 	
varWW 
	eventTypeWW 
=WW 
TypeWW  
.WW  !
GetTypeWW! (
(WW( )
$"WW) +
$strWW+ B
{WWB C

eventStoreWWC M
.WWM N
	EventTypeWWN W
}WWW X
$strWWX i
"WWi j
)WWj k
;WWk l
ifYY 
(YY 
	eventTypeYY 
==YY 
nullYY !
)YY! "
throwZZ 
newZZ %
InvalidOperationExceptionZZ 3
(ZZ3 4
$"ZZ4 6
$strZZ6 U
{ZZU V

eventStoreZZV `
.ZZ` a
	EventTypeZZa j
}ZZj k
"ZZk l
)ZZl m
;ZZm n
return\\ 
(\\ 
IEventDomain\\  
)\\  !
JsonSerializer\\! /
.\\/ 0
Deserialize\\0 ;
(\\; <

eventStore\\< F
.\\F G
	EventData\\G P
,\\P Q
	eventType\\R [
)\\[ \
!\\\ ]
;\\] ^
}]] 	
}^^ 
}`` ”
nC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IReadRepository\IAccountReadRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
.2 3
IReadRepository3 B
{ 
public 
	interface "
IAccountReadRepository (
{ 
Task 
< 
AccountReadModel 
? 
> 
GetAccountByIdAsync -
(- .
Guid. 2
contaId3 :
): ;
;; <
} 
} Ñ
RC:\owner-projects\CooperAna\BankMore\Infrastructure\ConfigContext\DapperContext.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
ConfigContext" /
{ 
public 
class 
DapperContext 
: 
IDisposable (
{ 
private		 	
readonly		
 
OracleConnection		 #
_connection		$ /
;		/ 0
private

 	
readonly


 
OracleTransaction

 $
_transaction

% 1
;

1 2
public 
DapperContext	 
( 
string 
connectionString .
). /
{ 
_connection 
= 
new 
OracleConnection %
(% &
connectionString& 6
)6 7
;7 8
_connection 
. 
Open 
( 
) 
; 
_transaction 
= 
_connection 
. 
BeginTransaction .
(. /
)/ 0
;0 1
} 
public 
OracleConnection	 

Connection $
=>% '
_connection( 3
;3 4
public 
OracleTransaction	 
Transaction &
=>' )
_transaction* 6
;6 7
public 
void	 
Commit 
( 
) 
{ 
_transaction 
. 
Commit 
( 
) 
; 
} 
public 
void	 
Rollback 
( 
) 
{ 
_transaction 
. 
Rollback 
( 
) 
; 
} 
public   
void  	 
Dispose   
(   
)   
{!! 
_transaction"" 
?"" 
."" 
Dispose"" 
("" 
)"" 
;"" 
_connection## 
?## 
.## 
Dispose## 
(## 
)## 
;## 
}$$ 
}%% 
}&& ó
tC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IWriteRepository\ITransactionWriteRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
.2 3
IWriteRepository3 C
{ 
public 
	interface '
ITransactionWriteRepository -
{ 
Task (
AddTransactionByAccountAsync #
(# $!
TransactionWriteModel$ 9
transaction: E
)E F
;F G
} 
} ÷
ZC:\owner-projects\CooperAna\BankMore\Infrastructure\ConfigContext\DatabaseConfiguration.cs
	namespace 	
BankMore
 
. 
Infrastructure !
.! "
ConfigContext" /
{ 
public		 
class		 !
DatabaseConfiguration		 #
{

 
public 
static	 
void 

Initialize 
(  
string  &
connectionString' 7
)7 8
{ 	
using 
var 

connection  
=! "
new# &
OracleConnection' 7
(7 8
connectionString8 H
)H I
;I J

connection 
. 
Open 
( 
) 
; 
const 
string !
createEventStoreTable .
=/ 0
$str1 
; 
const"" 
string"" &
createContasReadModelTable"" 3
=""4 5
$str",6 
;,, 
const.. 
string.. *
createTransacoesReadModelTable.. 7
=..8 9
$str.8: 
;88 

connection:: 
.:: 
Execute:: 
(:: !
createEventStoreTable:: 4
)::4 5
;::5 6

connection;; 
.;; 
Execute;; 
(;; &
createContasReadModelTable;; 9
);;9 :
;;;: ;

connection<< 
.<< 
Execute<< 
(<< *
createTransacoesReadModelTable<< =
)<<= >
;<<> ?
}== 	
}>> 
}?? ¸
_C:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IGenericWriteRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
{ 
public 

	interface #
IGenericWriteRepository ,
<, -
TEntity- 4
,4 5
TKey6 :
>: ;
where< A
TEntityB I
:J K
classL Q
{		 
Task 
< 
TEntity 
> 
InsertAsync !
(! "
TEntity" )
entity* 0
)0 1
;1 2
Task 
< 
TEntity 
> 
UpdateAsync !
(! "
TEntity" )
entity* 0
)0 1
;1 2
Task 
< 
bool 
> 
DeleteAsync 
( 
TKey #
id$ &
)& '
;' (
} 
} Ç
_C:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IRepositories\IGenericReadyRepository.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IRepositories% 2
{ 
public 

	interface 
IReadRepository $
<$ %
TEntity% ,
,, -
TKey. 2
>2 3
where4 9
TEntity: A
:B C
classD I
{		 
Task 
< 
TEntity 
> 
GetRecordByIdAsync (
(( )
TKey) -
id. 0
)0 1
;1 2
Task 
< 
IEnumerable 
< 
TEntity  
>  !
>! "
GetAllRecordAsync# 4
(4 5
)5 6
;6 7
Task 
< 
bool 
> 
HasRecordAsync !
(! "
TKey" &
id' )
)) *
;* +
} 
} Ž	
MC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IEvents\IEventStore.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IEvents% ,
{ 
public 

	interface 
IEventStore  
{ 
Task 
AppendEventsAsync 
( 
Guid #
aggregateId$ /
,/ 0
IEnumerable1 <
<< =
IEventDomain= I
>I J
eventsK Q
,Q R
intS V
expectedVersionW f
)f g
;g h
Task 
< 
IEnumerable 
< 
IEventDomain %
>% &
>& '
GetEventsAsync( 6
(6 7
Guid7 ;
aggregateId< G
)G H
;H I
Task 
< 
IEnumerable 
< 
IEventDomain %
>% &
>& ' 
GetEventsByTypeAsync( <
(< =
string= C
	eventTypeD M
)M N
;N O
}		 
}

  
NC:\owner-projects\CooperAna\BankMore\Domain\Interfaces\IEvents\IEventDomain.cs
	namespace 	
BankMore
 
. 
Domain 
. 

Interfaces $
.$ %
IEvents% ,
{ 
public 

	interface 
IEventDomain !
{ 
Guid 
EventId 
{ 
get 
; 
} 
DateTime 

OccurredOn 
{ 
get !
;! "
}# $
string 
	EventType 
{ 
get 
; 
}  !
}		 
}

 þ
\C:\owner-projects\CooperAna\BankMore\Application\Models\WriteModels\TransactionWriteModel.cs
	namespace 	
BankMore
 
. 
Application 
. 
Models %
.% &
WriteModels& 1
{ 
public 
class !
TransactionWriteModel #
{ 
} 
} Á

XC:\owner-projects\CooperAna\BankMore\Application\Models\WriteModels\AccountWriteModel.cs
	namespace 	
BankMore
 
. 
Application 
. 
Models %
.% &
WriteModels& 1
{ 
public 
class 
AccountWriteModel 
{ 
public		 
string			 
IdContaCorrente		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public 
int	 
Numero 
{ 
get 
; 
set 
; 
}  !
public 
string	 
Nome 
{ 
get 
; 
set 
;  
}! "
public 
bool	 
Ativo 
{ 
get 
; 
set 
; 
}  !
=" #
false$ )
;) *
public 
string	 
Senha 
{ 
get 
; 
set  
;  !
}" #
public 
string	 
Salt 
{ 
get 
; 
set 
;  
}! "
} 
} ‹
ZC:\owner-projects\CooperAna\BankMore\Application\Models\ReadModels\TransactionReadModel.cs
	namespace 	
BankMore
 
. 
Application 
. 
Models %
.% &

ReadModels& 0
{ 
public 
class  
TransactionReadModel "
{ 
public 
Guid	 
Id 
{ 
get 
; 
set 
; 
} 
public 
Guid	 
ContaId 
{ 
get 
; 
set  
;  !
}" #
public

 
string

	 
TipoTransacao

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
=

, -
string

. 4
.

4 5
Empty

5 :
;

: ;
public 
decimal	 
Valor 
{ 
get 
; 
set !
;! "
}# $
public 
decimal	 
SaldoAnterior 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
decimal	 

SaldoAtual 
{ 
get !
;! "
set# &
;& '
}( )
public 
string	 
	Descricao 
{ 
get 
;  
set! $
;$ %
}& '
=( )
string* 0
.0 1
Empty1 6
;6 7
public 
DateTime	 
DataTransacao 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} Û
VC:\owner-projects\CooperAna\BankMore\Application\Models\ReadModels\AccountReadModel.cs
	namespace 	
BankMore
 
. 
Application 
. 
Models %
.% &

ReadModels& 0
{ 
public 
class 
AccountReadModel 
{ 
public		 
string			 
IdContaCorrente		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public 
int	 
Numero 
{ 
get 
; 
set 
; 
}  !
public 
string	 
Nome 
{ 
get 
; 
set 
;  
}! "
public 
bool	 
Ativo 
{ 
get 
; 
set 
; 
}  !
} 
} 