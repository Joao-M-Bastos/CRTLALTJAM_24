using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
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
        DeactivateGas();
    }

    private void Update()
    {
        if (timeActive > 0 && fireActive)
        {
            timeActive -= Time.deltaTime;
        }else if(timeActive <= 0 && fireActive)
        {
            DeactivateGas();
        }
    }

    public void DeactivateGas()
    {
        fireActive = false;
        gasCollider.enabled = false;
        meshRenderer.material = gasMaterial;
    }

    public void ActiveGas()
    {
        fireActive = true;
        timeActive = 1;
        gasCollider.enabled = true;
        meshRenderer.material = fireMaterial;
    }
}
