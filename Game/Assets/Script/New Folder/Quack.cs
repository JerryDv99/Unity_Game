using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quack : MonoBehaviour
{
    bool create = false;
    GameObject Point;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroySelf());
        if(!create)
        {
            Point = new GameObject("0");

            Point.transform.position = this.transform.position;

            Point.AddComponent<Node>();
                        
            create = true;
        }
        Node node = Point.GetComponent<Node>();
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController E = other.gameObject.GetComponent<EnemyController>();
            E.SetIndex(2);
            E.Anim.SetBool("Doubt", true);
            E.Anim.SetBool("Chase", true);
            E.gameObject.GetComponent<NodeController>().SetTarget(node);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

        yield return null;
    }
}
