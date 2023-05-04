using System.Collections;
using System.Collections.Generic;
using Services;

using UnityEngine;

public class StickyGiver : MonoBehaviour
{
    [SerializeField]
    private TaskSO sticky;

    [SerializeField]
    private bool given = false;

    [Tooltip("The speech bubble that appears when the NPC is talking")]
    [SerializeField] private GameObject speechBubble;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !given)
        {
            ServiceLocator.Instance.Get<TaskManager>().AssignTask(sticky);
            speechBubble.SetActive(true);
            given = true;

            //stun the player- keep them from moving
            //code copied from NPCChatter
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
            speechBubble.SetActive(false);

            //allow player to move
            //code copied from NPCChatter
            PlayerMovement player = (PlayerMovement)other.gameObject.GetComponent("PlayerMovement");
            player.MaxSpeedMod = 0;
            player.MoveForceMod = 0;
        }
    }
}
