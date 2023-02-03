using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NodeController : MonoBehaviour
{
    [SerializeField] private Node Target;
    [SerializeField] private Node OldTarget;

    public Node GetOldTarget() { return OldTarget; }

    public void SetTarget(Node _node)
    {
        OldTarget = Target;
        Target = _node;
    }
       
    void Start()
    {
        Transform trans = transform.parent.transform;        
        Target = trans.Find("NodeList").GetChild(0).GetComponent<Node>();
    }

    void Update()
    {

        EnemyController E = this.transform.GetComponent<EnemyController>();
        if (E.GetIndex() == 1 || E.GetIndex() == 2)
        {
            Vector3 dir = Target.transform.position - transform.position;

            this.transform.position += (dir.normalized * Time.deltaTime * 3.0f);

            float fDistance = Vector3.Distance(transform.position, Target.transform.position);
                        

            if (fDistance < 0.2f)
            {
                if (Target.next == null)
                {
                    E.SetIndex(0);
                    E.Anim.SetBool("Chase", false);
                    E.Anim.SetBool("Doubt", false);
                    E.Anim.SetBool("Walk", false);
                }
                OldTarget = Target;
                Target = Target.next;
            }

            
            if(fDistance > 0.2f)
            {
                Vector3 V1 = (Target.transform.position - transform.position).normalized;
                V1.y = 0;

                transform.LookAt(transform.position + V1);
            }
            

        }
    }
}
