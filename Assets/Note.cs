
/*
//Essential
-hammer idle:

related to playerscirpt:
DONE timer we need an timer for hammerstate time limitation
DONE its usualy start switched after player land so we need upper priority if its hammer time if true we rapidly siwthc to hammer idle from idle state
related to otherStates:
DONE calling switch to hammer state can be called on jumpstate->Land or On idleState->at the begining

hammer idle inner functions:
DONE cathces a and d button to swith between hammer run and idle
DONE idle hammer anim

DONE sphere cast for enemy destroying
DONE also we need ground cast we same logic of idle and run ground cst
SpherCast Offset based on player facieng sphercast need to change pos based on facieng 
enmyDeath:
enmy death logic can be same dedicated script between all enemies
common behavior on enimies death :
timefreeze 
destroy anim
adding score 
calling score indicator
description: time gets freezed some seconds 
DONE death logic shloud be called from player not with barrel or flame through inteface
DONE does eneimes death has diffrent score?
DONE changing hammer damagwer range offset based on mario facing !

DONE bonus counter:
DONE game over:
DONE we use observer pattern for game over
DONE we habve game over component which has a on game over 
DONE working on enimeis reaction on player death


DONE game over and three live mehcanism
DOne 3 live UI
--> SOUNDS , climb sound, hammer time ,enemy hammered,barrel roll

DONE Reaching haj khanom,bonusCounter, OnWin Game setup, mario anim and rotate , heart activiation, enemies disable , donkey kong stop, pauline anim Stop
DONE mario bug , when it comes at top of ladder idle dont play , when it comes to down of ladder idle anim plays , its probably a mechanism about reaching top of ladder 
DONE Start Menu 
 
//-Not Essential 
//scoring on jump from multiple barrels with one jump
 */
