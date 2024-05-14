using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPointRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_pos.x = 20f; //The distance between the camera and object
        Vector3 object_pos = transform.position;
        mouse_pos.z = mouse_pos.z - object_pos.z;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(-angle, 0, 0));
    }
}
