using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrpt : MonoBehaviour
{
    Transform playerTranform;

    [SerializeField] Vector3 offset;
    private void Awake()
    {
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTranform.position + offset;
    }

    private void FixedUpdate()
    {
        transform.position = playerTranform.position + offset;
    }
}
