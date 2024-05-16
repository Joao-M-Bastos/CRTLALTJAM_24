using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGas : Flammable
{

    [SerializeField] Material gasMaterial, fireMaterial;

    MeshRenderer meshRenderer;
    [SerializeField] float growScale;


    bool clapped;
    float timeInsideGas;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.forward * Time.deltaTime * growScale;
        transform.Translate((Vector3.forward / 2) * Time.deltaTime * growScale);

        if (timeInsideGas > 1 && !clapped)
            BossClap();

        CanDeactivateFlame();
    }

    public bool IsFireActive()
    {
        return fireActive;
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        meshRenderer.material = gasMaterial;
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        meshRenderer.material = fireMaterial;
    }

    private void BossClap()
    {
        clapped = true;
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss.TryGetComponent(out FireBoss fireBoss))
            fireBoss.Attack();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeInsideGas = 0;
            clapped = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            timeInsideGas += Time.deltaTime;

        if (other.TryGetComponent(out PlayerScript player) && fireActive)
        {
            player.TakeDamage(1);
        }
    }
}