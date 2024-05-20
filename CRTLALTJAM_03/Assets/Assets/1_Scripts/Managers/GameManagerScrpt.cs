using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScrpt : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager;

    bool gameOver, hasKey;

    static GameManagerScrpt instance;

    public bool HasKey => hasKey;
    public bool IsGameOver => gameOver;

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
            {
                ReestartGame();
            }
        }

    }

    public void LoadScene(int id, string name = "")
    {
        if (name == "")
            SceneManager.LoadScene(id);
        else
            SceneManager.LoadScene(name);
    }

    private void ReestartGame()
    {
        LoadScene(0);
        gameOver = false;
        Time.timeScale = 1;
        canvasManager.ChangeState(CanvasStates.InGame);

    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        Time.timeScale = 0;
        canvasManager.ChangeState(CanvasStates.GameOver);
    }

    public void OnWillRenderObject()
    {
        hasKey = true;
    }

    public void PlayDialogue(int dialogueID, PlayerScript player)
    {
        canvasManager.PlayDialogue(dialogueID, player); 
    }
}
