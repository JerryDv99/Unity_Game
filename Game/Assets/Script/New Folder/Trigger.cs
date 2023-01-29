using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject Obj;

    private void OnTriggerEnter(Collider other)
    {
        Animator Anim = Obj.GetComponent<Animator>();
        Anim.SetBool("Jump", true);
    }
}
