using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

public class PlayerMoveState : PlayerBaseState
{
    public override void OnStateFinish(PlayerStateManager stateManager, PlayerScript player)
    {
        
    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        if (player.PlayerRB.useGravity == false || player.JumpWallCooldown > 0)
            return;

        ManageAcelleration(player);

        player.PlayerRB.velocity += new Vector3(0, 0, player.InternalAceleration * player.Aceleration);

        Debug.Log(player.PlayerRB.velocity.z + " : " + player.InternalAceleration * player.Aceleration);

        ManageGravity(player);        

        player.text.text = player.Aceleration.ToString();
    }

    private void ManageGravity(PlayerScript player)
    {
        if (player.PlayerRB.velocity.y < 2f)
        {
            player.PlayerRB.velocity += Vector3.up * Physics.gravity.y * 2f * Time.deltaTime;
        }
        else if (player.PlayerRB.velocity.y > 1 && !Input.GetKey(KeyCode.Space))
        {
            player.PlayerRB.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }
    }

    private void ManageAcelleration(PlayerScript player)
    {
        float baseMultiplier = 4;

        if (!player.IsOnGround())
            baseMultiplier = 3;

        //Zera a aceleração para ser recalculada
        

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            player.SetInternalAceleration(0);

            if (Mathf.Abs(player.PlayerRB.velocity.z) > 0.9f)
                player.ChangeInternalAceleration( -1 *Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime);
            else    
                player.PlayerRB.velocity = new Vector3(0, player.PlayerRB.velocity.y, 0);
        }
        else
        {
            if (Mathf.Abs(player.PlayerRB.velocity.z) < player.Aceleration)
            {
                player.ChangeInternalAceleration(Input.GetAxisRaw("Horizontal") * (baseMultiplier - 2) * Time.deltaTime);
            }


            if (Mathf.Abs(player.PlayerRB.velocity.z) > player.Aceleration)
            {
                if (player.IsOnGround())
                    player.ChangeInternalAceleration(-1 * Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime);
            }

            if (Mathf.Sign(player.PlayerRB.velocity.z) != Mathf.Sign(Input.GetAxisRaw("Horizontal")))
            {
                player.ChangeInternalAceleration(-1 * Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime);
            }
        }
    }

    public override void OnStateStart(PlayerStateManager stateManager, PlayerScript player)
    {

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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateManager.ChanceState(stateManager.DashState);
        }

        if (player.jumpBufferTime > 0 && player.coyoteTimeCounter > 0)
        {
            player.jumpBufferTime = 0;

            player.coyoteTimeCounter = 0;
            player.PlayerRB.velocity += Vector3.up * player.JumpForce;
        }

        TryChangeState(stateManager, player);
    }

    private void TryChangeState(PlayerStateManager stateManager, PlayerScript player)
    {
        if(player.isOnDialogue)
            stateManager.ChanceState(stateManager.DialogueState);

        Debug.DrawLine(player.transform.position, player.transform.position + player.transform.forward * 0.75f, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(player.transform.position, player.transform.forward, out hit, 0.75f, player.WallMask))
        {
            if(Input.GetAxisRaw("Horizontal") == player.transform.forward.z || player.JumpWallCooldown > 0)
            if (player.PlayerRB.velocity.y < 1 && player.PlayerRB.useGravity && !player.IsOnGround())
                        stateManager.ChanceState(stateManager.WallState);
        }
    }
}
