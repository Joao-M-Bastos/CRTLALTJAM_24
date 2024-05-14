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

        player.PlayerRB.velocity += new Vector3(0, 0, player.aceleration * player.Speed);

        ManageGravity(player);        
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

        if (player.PlayerRB.useGravity == false && player.JumpWallCooldown > 0)
            return;

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            player.aceleration = 0;

            player.aceleration -= Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime;

            if (Mathf.Abs(player.PlayerRB.velocity.z) < 1f)
            {
                player.aceleration = 0;
                player.PlayerRB.velocity = new Vector3(0, player.PlayerRB.velocity.y, 0);
            }
        }
        else
        {
            if (Mathf.Abs(player.PlayerRB.velocity.z) < 8 && Mathf.Abs(player.aceleration) < 0.1f)
            {
                player.aceleration += Input.GetAxisRaw("Horizontal") * (baseMultiplier - 2) * Time.deltaTime;
            }


            if (Mathf.Abs(player.PlayerRB.velocity.z) > 8)
            {
                player.aceleration = 0;
                if (player.IsOnGround())
                    player.aceleration -= Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime;
            }

            if (Mathf.Sign(player.PlayerRB.velocity.z) != Mathf.Sign(Input.GetAxisRaw("Horizontal")))
                player.aceleration -= Mathf.Sign(player.PlayerRB.velocity.z) * baseMultiplier * Time.deltaTime;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(player.PlayerRB.velocity.y) + Mathf.Abs(player.PlayerRB.velocity.z) > player.Speed * 0.75f)
        {
            stateManager.ChanceState(stateManager.DashState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsOnGround())
        {
            player.PlayerRB.velocity += Vector3.up * player.JumpForce;
        }

        TryChangeState(stateManager, player);
    }

    private void TryChangeState(PlayerStateManager stateManager, PlayerScript player)
    {
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
