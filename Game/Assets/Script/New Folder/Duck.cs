using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public GameObject Quack;
    bool b;

    private void Start()
    {
        b = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (b == false)
        {
            Instantiate(Quack, this.transform.position, Quaternion.identity);
            b = true;
        }
    }
}
