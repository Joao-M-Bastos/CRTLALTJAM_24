using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossClap();
            //Destroy(gameObject);
        }
    }
    private void BossClap()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss.TryGetComponent(out FireBoss fireBoss))
            fireBoss.Attack();
    }
}
