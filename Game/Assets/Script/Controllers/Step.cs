using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    bool create = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().SetIndex(1);
            if (!create)
            {
                GameObject Point = new GameObject("0");

                Point.transform.position = this.transform.position;
                Point.AddComponent<Node>();

                Node node = Point.GetComponent<Node>();
                other.GetComponent<NodeController>().SetTarget(node);

                create = true;
            }
            
        }
    }
}
