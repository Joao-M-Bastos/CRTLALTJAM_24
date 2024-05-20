using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (GameManagerScrpt.GetInstance().IsGameOver)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().ResetValues();
                GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
            }
            return;
        }

        Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }
}
