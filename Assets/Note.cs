
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

-->bonus counter:
game over:
we use observer pattern for game over
we habve game over component which has a on game over 
--> working on enimeis reaction on player death

//game over and three live mehcanism
//3 live UI
//observer pattern for its death

 
 
//-Not Essential 
//scoring on jump from multiple barrels with one jump
 */
