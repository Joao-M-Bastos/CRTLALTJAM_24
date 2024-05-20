using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int levelId;
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

        if (needsKey && !GameManagerScrpt.GetInstance().HasKey)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isEntrance)
        {
            playerInside.PlayerRB.velocity = Vector3.zero;
            GameManagerScrpt.GetInstance().LoadScene(levelId);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (!isExit)
            return;

        PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        player.transform.position = putPlayerPosition.transform.position;
    }
}
