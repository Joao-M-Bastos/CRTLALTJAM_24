using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] int keyID;
    [SerializeField] bool needsKey, isEntrance, isExit;

    [SerializeField] Transform putPlayerPosition;

    PlayerScript playerInside;

    private void OnTriggerStay(Collider other)
    {
        if(playerInside == null)
            other.TryGetComponent(out playerInside);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            playerInside = null;
    }

    private void Update()
    {
        if (playerInside == null)
            return;

        if (needsKey && GameManagerScrpt.GetInstance().HasKey != keyID)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isExit)
        {
            playerInside.PlayerRB.velocity = Vector3.zero;
            GameManagerScrpt.GetInstance().LoadScene(1, levelName);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (!isEntrance)
            return;

        PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        player.transform.position = putPlayerPosition.transform.position;
    }
}
