using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManagerScrpt.GetInstance().AddKey();

        Destroy(gameObject);
    }
}
