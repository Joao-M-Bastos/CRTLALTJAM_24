using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerScript player;

    PlayerBaseState currentState;
    PlayerBaseState moveState = new PlayerMoveState();

    public PlayerBaseState MoveState => moveState;

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

    public void ChanceState(PlayerBaseState newState)
    {
        if(currentState != null)
            currentState.OnStateFinish(this, player);

        currentState = newState;

        currentState.OnStateStart(this, player);
    }
}
