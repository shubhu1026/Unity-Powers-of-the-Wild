using System.Collections.Generic;

public class PlayerStateFactory
{
    PlayerStateMachine context;
    Dictionary<string, PlayerBaseState> states = new Dictionary<string, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        states["idle"] = new PlayerIdleState(context, this);
        states["walk"] = new PlayerWalkState(context, this);
        states["run"] = new PlayerRunState(context, this);
        states["jump"] = new PlayerJumpState(context, this);
        states["grounded"] = new PlayerGroundedState(context, this);
        states["fall"] = new PlayerFallState(context, this);
    }

    public PlayerBaseState Idle(){
        return states["idle"];
    }
    public PlayerBaseState Walk(){
        return states["walk"];
    }
    public PlayerBaseState Run(){
        return states["run"];
    }
    public PlayerBaseState Jump(){
        return states["jump"];
    }
    public PlayerBaseState Grounded(){
        return states["grounded"];
    } 
    public PlayerBaseState Fall(){
        return states["fall"];
    }
}
