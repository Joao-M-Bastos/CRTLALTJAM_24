using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    float activeDashTime = 0.5f;

    public override void OnStateFinish(PlayerStateManager stateManager, PlayerScript player)
    {
        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        playerCollider.size += Vector3.up * 1f;
        playerCollider.center += Vector3.up * 1f;

        player.PlayerAnim.SetBool("dash", false);

    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        
    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {
        player.CancelWind();

        player.PlayerAnim.SetBool("dash", true);

        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        playerCollider.size += Vector3.up * -1f;
        playerCollider.center += Vector3.up * -1f;

        activeDashTime = 0.5f;
    }

    public override void OnStateUpdate(PlayerStateManager stateManager, PlayerScript player)
    {
        if (activeDashTime < 0)
            stateManager.ChanceState(stateManager.MoveState);

        activeDashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && player.IsOnGround())
        {
            player.PlayerRB.velocity += Vector3.up * player.JumpForce;
            stateManager.ChanceState(stateManager.MoveState);
        }
    }
}
