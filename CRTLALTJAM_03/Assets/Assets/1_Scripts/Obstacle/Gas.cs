using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : Flammable
{
    [SerializeField] Material gasMaterial, fireMaterial;

    BoxCollider gasCollider;
    MeshRenderer meshRenderer;

    

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
        CanDeactivateFlame();
    }

    public bool IsFireActive()
    {
        return fireActive;
    }

    public override void DeactivateFlame()
    {
        fireActive = false;
        gasCollider.enabled = false;
        meshRenderer.material = gasMaterial;
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        gasCollider.enabled = true;
        meshRenderer.material = fireMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerScript player) && fireActive)
        {
            player.TakeDamage(1);
            player.TakeKnockback(transform.forward);
        }
    }
}
