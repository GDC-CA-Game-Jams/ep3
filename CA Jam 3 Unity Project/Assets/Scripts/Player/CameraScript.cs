using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Tooltip("The object to follow")]
    [SerializeField] private Transform followObject;

    [Tooltip("Offset in coordinates to apply to camera's position")]
    [SerializeField] private Vector3 followOffset;

    [Tooltip("Rotation in degrees to apply as the camera is following")]
    [SerializeField] private Vector3 followRotation;

    private void Start()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(followRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.position + followOffset;
        transform.rotation = Quaternion.Euler(followRotation);
    }
}
