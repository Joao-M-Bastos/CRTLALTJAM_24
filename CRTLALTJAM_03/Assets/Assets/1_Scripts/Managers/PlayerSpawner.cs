using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Destroy(GameObject.FindGameObjectWithTag("Player").GetComponentInParent<GameManagerScrpt>().gameObject);
        

        Instantiate(PlayerPrefab, transform.position, transform.rotation);
    }
}
