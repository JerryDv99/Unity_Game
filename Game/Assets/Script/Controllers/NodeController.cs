using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    private Node Target;
    private Node OldTarget;

    public Node GetOldTarget() { return OldTarget; }

    private List<GameObject> CollObjects = new List<GameObject>();

    [SerializeField] private int Index;


    void Start()
    {
        Index = 1;

        Transform trans = transform.parent.transform;
        Target = trans.Find("Node_" + Index.ToString()).GetChild(0).GetComponent<Node>();
    }

    void Update()
    {


        float fDistance = Vector3.Distance(transform.position, Target.transform.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (Vector3.Distance(transform.position, hit.point) <= 1.5f &&
                !CollObjects.Contains(hit.transform.gameObject))
            {
                CollObjects.Add(hit.transform.gameObject);
                Target = NodeManager.Instance.GetNode(this.gameObject, hit);
                //Debug.Log("???????????????");
            }
            else
            {
                if (fDistance < 0.1f)
                {
                    OldTarget = Target;
                    Target = Target.next;
                }
            }
        }

        Vector3 dir = Target.transform.position - transform.position;

        transform.position += (dir.normalized * Time.deltaTime * 2.0f);

        Vector3 V1 = (Target.transform.position - transform.position).normalized;
        V1.y = 0;
        transform.LookAt(transform.position + V1);

        Debug.DrawLine(transform.position, Target.transform.position, Color.red);


        if (Target.next == null)
            CollObjects.Clear();

    }
}
