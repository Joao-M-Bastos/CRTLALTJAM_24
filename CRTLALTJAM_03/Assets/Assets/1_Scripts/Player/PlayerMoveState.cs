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

        if (player.PlayerRB.useGravity == false)
            return;

        player.PlayerRB.velocity += new Vector3(0, 0, player.Aceleration * player.Speed);

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
