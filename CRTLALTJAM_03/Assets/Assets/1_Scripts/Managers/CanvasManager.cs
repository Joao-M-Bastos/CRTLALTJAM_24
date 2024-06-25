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

    [SerializeField] GameObject InGame, GameOver, dialogueBox;

    [SerializeField] Dialogue[] dialogues;
    [SerializeField] GameObject[] hearts, breath;

    PlayerScript playerScript;

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

    public void PlayDialogue(int dialogueID, PlayerScript player)
    {
        playerScript = player;

        playerScript.isOnDialogue = true;

        ChangeState(CanvasStates.InGame);
        dialogueBox.SetActive(true);
        dialogues[dialogueID].StartDialogue(this);
    }

    public void StopDialogue()
    {
        dialogueBox.SetActive(false);
        playerScript.isOnDialogue = false;
    }

    public void SetLifeUI(int value)
    {
        if (value > hearts.Length)
            return;

        foreach (GameObject h in hearts)
            h.SetActive(false);

        for (int i = 0; i < value; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void SetBreathUI(int value)
    {

        if (value > breath.Length)
            return;

        foreach (GameObject h in breath)
            h.SetActive(false);

        for (int i = breath.Length -1; i >= value; i--)
        {
            breath[i].GetComponent<Animator>().SetTrigger("Charge");
        }
    }

    public void PlayBreathUI(int value)
    {
        if (value > breath.Length)
            return;

        breath[value].GetComponent<Animator>().SetTrigger("ReCharge");
    }
}
