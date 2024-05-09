using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerScript player;

    PlayerBaseState currentState;
    PlayerBaseState moveState = new PlayerMoveState();
    PlayerBaseState dashState = new PlayerDashState();
    PlayerBaseState wallState = new PlayerInWallState();

    public PlayerBaseState MoveState => moveState;
    public PlayerBaseState DashState => dashState;
    public PlayerBaseState WallState => wallState;

    public void Awake()
    {
        player = GetComponent<PlayerScript>();
    }

    void Start()
    {
        ChanceState(moveState);
    }

    public void FixedUpdate()
    {
        currentState.OnStateFixedUpdade(this, player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this, player);
    }

    public bool CheckCurrentState(PlayerBaseState stateToCheck)
    {
        return stateToCheck == currentState;
    }

    public void ChanceState(PlayerBaseState newState)
    {
        if(currentState != null)
            currentState.OnStateFinish(this, player);

        currentState = newState;

        currentState.OnStateStart(this, player);
    }
}
