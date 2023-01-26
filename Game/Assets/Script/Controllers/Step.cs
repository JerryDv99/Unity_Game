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
            EnemyController E = other.gameObject.GetComponent<EnemyController>();
            

            if (!create && E.GetIndex() != 5)
            {
                E.SetIndex(2);
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
