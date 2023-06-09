using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChatter : MonoBehaviour
    //manage the trigger collider for slowing/stopping the player and setting speech bubble active
{
    [Tooltip("The speech bubble that appears when the NPC is talking")]
    [SerializeField] private GameObject speechBubble;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player entered colision zone");
            PlayerMovement player = (PlayerMovement)other.gameObject.GetComponent("PlayerMovement");
            player.MaxSpeedMod = -10;
            player.MoveForceMod = -500;
            speechBubble.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player exited colision zone");
            PlayerMovement player = (PlayerMovement)other.gameObject.GetComponent("PlayerMovement");
            player.MaxSpeedMod = 0;
            player.MoveForceMod = 0;
            speechBubble.SetActive(false);
        }
    }
}
