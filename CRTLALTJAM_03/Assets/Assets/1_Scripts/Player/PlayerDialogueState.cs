using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueState : PlayerBaseState
{
    public override void OnStateFinish(PlayerStateManager stateManager, PlayerScript player)
    {

    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {

    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {
        player.CancelWind();
        player.PlayerRB.velocity = Vector3.zero;
    }

    public override void OnStateUpdate(PlayerStateManager stateManager, PlayerScript player)
    {
        if(!player.isOnDialogue)
            stateManager.ChanceState(stateManager.MoveState);
    }
}
