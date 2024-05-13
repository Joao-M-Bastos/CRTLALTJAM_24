using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScrpt : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager;

    bool gameOver;

    static GameManagerScrpt instance;

    public static GameManagerScrpt GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance != this)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ReestartGame();
        }
    }

    private void ReestartGame()
    {
        canvasManager.ChangeState(CanvasStates.InGame);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        Time.timeScale = 0;
        canvasManager.ChangeState(CanvasStates.GameOver);
    }
}
