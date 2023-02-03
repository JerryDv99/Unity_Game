using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour
{
    private void Start()
    {
        int Count = transform.childCount;

        for (int i = 0; i < Count; ++i)
        {
            if (i > 0)
            {
                Node FrontNode = transform.GetChild(i - 1).GetComponent<Node>();
                Node Node = transform.GetChild(i).GetComponent<Node>();

                //Node.next = transform.GetChild(0).GetComponent<Node>();
                FrontNode.next = Node;
            }
        }
    }
}
