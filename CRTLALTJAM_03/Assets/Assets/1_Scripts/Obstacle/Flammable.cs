using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flammable : MonoBehaviour
{
    [SerializeField] protected float baseActiveFlameTime;
    protected float timeActive;
    protected bool fireActive;

    private void Awake()
    {
        timeActive = baseActiveFlameTime;
    }

    protected void CanDeactivateFlame()
    {
        if (timeActive > 0 && fireActive)
        {
            timeActive -= Time.deltaTime;
        }
        else if (timeActive <= 0 && fireActive)
        {
            DeactivateFlame();
        }
    }

    public abstract void DeactivateFlame();

    public abstract void ActiveFlame(float time);
}
