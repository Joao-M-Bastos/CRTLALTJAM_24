using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour
{
    [SerializeField] float baseAttackTimeCooldown;
    Animator animator;
    float attackTimeCooldown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        attackTimeCooldown = baseAttackTimeCooldown;
    }

    private void Update()
    {
        if(attackTimeCooldown > 0)
            attackTimeCooldown -= Time.deltaTime;
    }

    public void Attack()
    {
        if (attackTimeCooldown > 0)
            return;

        attackTimeCooldown = 1;

        Flammable[] flammables = FindObjectsOfType<Flammable>();
        animator.SetTrigger("clap");

        foreach(Flammable Flammable in flammables)
        {
            Flammable.ActiveFlame(0);
        }
    }
}
