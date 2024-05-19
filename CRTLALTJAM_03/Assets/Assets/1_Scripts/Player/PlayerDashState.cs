using System;
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
        playerCollider.center += Vector3.up * 0.5f;

        player.PlayerAnim.SetBool("dash", false);

    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        
    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {
        if (Mathf.Abs(player.PlayerRB.velocity.z) < player.Speed)
        {
            Debug.Log("a");
            player.PlayerRB.velocity = new Vector3(0, player.PlayerRB.velocity.y, player.Speed * player.transform.forward.z);
        }

        player.CancelWind();

        player.PlayerAnim.SetBool("dash", true);

        BoxCollider playerCollider = player.GetComponent<BoxCollider>();
        playerCollider.size += Vector3.up * -1f;
        playerCollider.center += Vector3.up * -0.5f;

        

        activeDashTime = 0.5f;
    }

    public override void OnStateUpdate(PlayerStateManager stateManager, PlayerScript player)
    {
        //if (IsEmpyUpside(player) && activeDashTime < 0)
        if (activeDashTime < 0 && IsEmpyUpside(player))
            stateManager.ChanceState(stateManager.MoveState);

        activeDashTime -= Time.deltaTime;

        if (player.isOnDialogue)
            stateManager.ChanceState(stateManager.DialogueState);

        if (player.jumpBufferTime > 0 && player.coyoteTimeCounter > 0)
        {
            player.coyoteTimeCounter = 0;
            player.jumpBufferTime = 0;

            player.PlayerRB.velocity += Vector3.up * player.JumpForce;
            stateManager.ChanceState(stateManager.MoveState);
        }
    }

    private bool IsEmpyUpside(PlayerScript player)
    {
        Debug.DrawLine(player.transform.position, player.transform.position + player.transform.up * 0.75f, Color.blue);

        bool empty = true;
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.up, out hit, 1.75f, player.WallMask))
        {
            empty = false;
        }
        return empty;

        
    }
}
