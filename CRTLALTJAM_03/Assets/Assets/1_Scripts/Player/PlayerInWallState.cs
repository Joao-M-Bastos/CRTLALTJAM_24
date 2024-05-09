using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInWallState : PlayerBaseState
{
    public override void OnStateFinish(PlayerStateManager stateManager, PlayerScript player)
    {

    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        player.transform.LookAt(player.transform.position + Vector3.forward * Input.GetAxisRaw("Horizontal"));

        player.PlayerRB.velocity -= Vector3.up * Physics.gravity.y * 0.5f * Time.deltaTime;
    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {
        player.PlayerRB.velocity *= 0.5f;
    }

    public override void OnStateUpdate(PlayerStateManager stateManager, PlayerScript player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.StartCharging();
        }

        if (Input.GetMouseButton(0))
        {
            player.ChargeWind();
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.ReleaseWind();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(player.PlayerRB.velocity.y) + Mathf.Abs(player.PlayerRB.velocity.z) < player.Speed * 0.75f)
        {
            stateManager.ChanceState(stateManager.DashState);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetWallJumpCooldown();
            player.PlayerRB.velocity = Vector3.up * player.JumpForce * 0.7f;
            player.PlayerRB.velocity += -player.transform.forward * player.JumpForce / 1.5f;
        }

        TryChangeState(stateManager, player);
    }

    private void TryChangeState(PlayerStateManager stateManager, PlayerScript player)
    {
        Debug.DrawLine(player.transform.position, player.transform.position + player.transform.forward * 0.75f, Color.red);
        RaycastHit hit;
        if (!Physics.Raycast(player.transform.position, player.transform.forward, out hit, 0.75f, player.WallMask)
            || !player.PlayerRB.useGravity || player.IsOnGround())
        {
            stateManager.ChanceState(stateManager.MoveState);
        }
    }
}
