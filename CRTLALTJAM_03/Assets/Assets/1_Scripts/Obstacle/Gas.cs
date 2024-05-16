using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : Flammable
{
    [SerializeField] Material gasMaterial, fireMaterial;

    MeshRenderer meshRenderer;

    

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
        meshRenderer.material = gasMaterial;
    }

    public override void ActiveFlame(float time)
    {
        fireActive = true;
        timeActive = baseActiveFlameTime + time;
        meshRenderer.material = fireMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out PlayerScript player) && fireActive)
        {
            player.TakeDamage(1);
        }
    }
}
