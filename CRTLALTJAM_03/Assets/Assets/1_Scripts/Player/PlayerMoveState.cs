using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (player.IsOnGround())
        {
            if (Mathf.Abs(player.PlayerRB.velocity.z) < player.Speed)
                player.PlayerRB.velocity = new Vector3(0, player.PlayerRB.velocity.y, Input.GetAxis("Horizontal") * player.Speed);
            else
                player.PlayerRB.velocity *= 0.95f;
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
            player.PlayerRB.AddForce(player.transform.forward * Input.GetAxis("Horizontal") * 4, ForceMode.VelocityChange);
            player.PlayerRB.AddForce(player.transform.up * 7, ForceMode.VelocityChange);
        }
    }
    
}
