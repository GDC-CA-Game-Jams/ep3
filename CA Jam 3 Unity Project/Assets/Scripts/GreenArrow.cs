using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenArrow : MonoBehaviour
{
    [SerializeField] GameObject itemBase;
    [SerializeField] GameObject greenArrow;

    public float upDisplacement = 4f;

    // Start is called before the first frame update
    void Start()
    {
        greenArrow.transform.SetPositionAndRotation(itemBase.transform.position + Vector3.up * upDisplacement, Quaternion.LookRotation(Vector3.right));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
