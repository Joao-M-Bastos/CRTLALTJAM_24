using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWindPointRotation : MonoBehaviour
{
    
    Vector3 pos;
    public float offset = 3f;


    void Update()
    {
        pos = Input.mousePosition;
        pos.z = offset;
        transform.LookAt(Camera.main.ScreenToWorldPoint(pos));
    }
}

