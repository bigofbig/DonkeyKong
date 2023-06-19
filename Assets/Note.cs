
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

--->sphere cast for enemy destroying
DONE also we need ground cast we same logic of idle and run ground cst
SpherCast Offset based on player facieng sphercast need to change pos based on facieng 
enmyDeath:
enmy death logic can be same dedicated script between all enemies
its better to have an inner on trigger enmy destroy logic to be seprate and better maintainable





//game over and three live mehcanism
//3 live UI
//observer pattern for its death

 
 
//-Not Essential 
//scoring on jump from multiple barrels with one jump
 */
