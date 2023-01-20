using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Node : MonoBehaviour
{
    public Node next = null;

    private void Awake()
    {
        SphereCollider coll = GetComponent<SphereCollider>();

        MyGizmo Gizmo = gameObject.AddComponent<MyGizmo>();
        Gizmo.color = Color.green;

        coll.radius = 0.2f;
        coll.isTrigger = true;
    }

    IEnumerator isTriggerCheck(SphereCollider coll)
    {
        yield return new WaitForSeconds(5.0f);
        coll.isTrigger = true;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, next.transform.position, Color.blue);
    }
}