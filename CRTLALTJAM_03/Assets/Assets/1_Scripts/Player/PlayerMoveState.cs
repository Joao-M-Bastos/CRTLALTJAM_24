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
        player.DeleteWind();
    }

    public override void OnStateFixedUpdade(PlayerStateManager stateManager, PlayerScript player)
    {
        player.text.text = (int)player.PlayerRB.velocity.z + "";

        ManageAcelleration(player);

        if (player.PlayerRB.useGravity == false)
            return;

        player.PlayerRB.velocity += new Vector3(0, 0, player.aceleration * player.Speed);

        //GravityController
        if (player.PlayerRB.velocity.y < 0.5f)
        {
            player.PlayerRB.velocity += Vector3.up * Physics.gravity.y * 2f * Time.deltaTime;
        }
        else if (player.PlayerRB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            player.PlayerRB.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
        }
    }

    private void ManageAcelleration(PlayerScript player)
    {
        float baseMultiplier = 4;

        if (!player.IsOnGround())
            baseMultiplier = 3;

        if (player.PlayerRB.useGravity == false)
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
                player.transform.LookAt(player.transform.position + Vector3.forward * Input.GetAxis("Horizontal"));
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
            player.InstaciateWind();
        }

        if (Input.GetMouseButtonUp(0))
        {
            player.DeleteWind();
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsOnGround())
        {
            player.PlayerRB.velocity += Vector3.up * player.JumpForce;
        }
    }
    
}
