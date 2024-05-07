using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    float activeDashTime = 0.5f;
    bool actived;

    public override void OnStateFinish(PlayerStateManager stateManager, PlayerScript player)
    {
        if (!actived)
            return;

        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        playerCollider.size += Vector3.up * 0.5f;
        playerCollider.center += Vector3.up * 0.5f;

        player.PlayerAnim.SetBool("dash", false);

    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        
    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {

        if (Mathf.Abs(player.PlayerRB.velocity.y) + Mathf.Abs(player.PlayerRB.velocity.z) < player.Speed * 0.75f)
        {
            actived = false;
            stateManager.ChanceState(stateManager.MoveState);
            return;
        }

        player.PlayerAnim.SetBool("dash", true);

        actived = true;

        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        playerCollider.size += Vector3.up * -0.5f;
        playerCollider.center += Vector3.up * -0.5f;

        

        activeDashTime = 0.5f;

        //Vector3 dir = player.PlayerRB.velocity.normalized;
        //player.PlayerRB.velocity = dir * player.Speed * 1.3f;
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
