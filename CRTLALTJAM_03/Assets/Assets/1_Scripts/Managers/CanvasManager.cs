using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasStates
{
    InGame,
    GameOver
}

public class CanvasManager : MonoBehaviour
{
    CanvasStates currentState;

    [SerializeField] GameObject InGame, GameOver;

    private void Awake()
    {
        ChangeState(CanvasStates.InGame);
    }

    public void ChangeState(CanvasStates state)
    {
        currentState = state;

        InGame.SetActive(currentState == CanvasStates.InGame);

        GameOver.SetActive(currentState == CanvasStates.GameOver);
    }
}
