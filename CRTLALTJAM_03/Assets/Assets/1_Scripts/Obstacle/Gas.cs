using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : Flammable
{
    [SerializeField] Material gasMaterial, fireMaterial;

    BoxCollider gasCollider;
    MeshRenderer meshRenderer;

    float timeActive = 1;
    bool fireActive;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        gasCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        DeactivateFlame();
    }

    private void Update()
    {
        if (timeActive > 0 && fireActive)
        {
            timeActive -= Time.deltaTime;
        }else if(timeActive <= 0 && fireActive)
        {
            DeactivateFlame();
        }
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        gasCollider.enabled = false;
        meshRenderer.material = gasMaterial;
    }

    public override void ActiveFlame()
    {
        fireActive = true;
        timeActive = 1;
        gasCollider.enabled = true;
        meshRenderer.material = fireMaterial;
    }
}
