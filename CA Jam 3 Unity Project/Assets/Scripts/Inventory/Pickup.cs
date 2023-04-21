using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Pickup : MonoBehaviour
{
    //Note: the user of this callback is responsible for destroying the game object
    public UnityEvent<GameObject> OnPickUp;

    private void OnTriggerEnter(Collider other) {
        OnPickUp?.Invoke(other.gameObject);
    }
}
