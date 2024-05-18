using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] int dialogueID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerScrpt.GetInstance().PlayDialogue(dialogueID, other.GetComponent<PlayerScript>());
            Destroy(gameObject);
        }
    }

}
