using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPath : MonoBehaviour
{
    public GameObject pathGO;

    Transform targetPathNode;
    int pathNodeIndex = 0;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        pathGO = GameObject.Find("Path");
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPathNode == null)
        {
            GetNextPathNode();
            if (targetPathNode == null)
            {
                // We've run out of path!
                RestartPath();
                return;
            }
        }
        Vector3 dir = targetPathNode.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // We reached the node
            targetPathNode = null;
        }
        else
        {
            // Move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
        }

    }

    void GetNextPathNode()
    {
        if (pathNodeIndex < pathGO.transform.childCount)
        {
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            RestartPath();
        }
    }

    void RestartPath()
        //start on the path again once you get to the end
    {
        pathNodeIndex = 0;
        Debug.Log("restarting...");
    }
}
