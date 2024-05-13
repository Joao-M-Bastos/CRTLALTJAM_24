using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flammable : MonoBehaviour
{
    public abstract void DeactivateFlame();

    public abstract void ActiveFlame();
}
