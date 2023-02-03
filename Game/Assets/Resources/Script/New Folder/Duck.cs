using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public GameObject Quack;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
            Instantiate(Quack, this.transform.position, Quaternion.identity);        
    }

}
