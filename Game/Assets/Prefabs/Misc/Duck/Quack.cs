using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController E = other.gameObject.GetComponent<EnemyController>();
            E.SetIndex(2);
            E.Anim.SetBool("Doubt", true);
            E.Anim.SetBool("Chase", true);
        }
    }
}
