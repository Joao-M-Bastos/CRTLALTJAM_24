using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void OnStateStart(PlayerStateManager stateManager, PlayerScript player);

    public abstract void OnStateFinish(PlayerStateManager stateManager, PlayerScript player);

    public abstract void OnStateUpdate(PlayerStateManager stateManager, PlayerScript player);

    public abstract void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player);
}
