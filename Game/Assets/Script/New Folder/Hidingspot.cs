using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidingspot : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" )
        {
            PlayerController P = other.GetComponent<PlayerController>();
            P.Anim.SetBool("Check", true);
            
            if(Input.GetKeyDown(KeyCode.H))
            {
                P.SetIndex(3);
                P.Anim.SetBool("Idle", false);
                P.Anim.SetBool("Walk", false);
                P.Anim.SetBool("Bend", false);
                P.Anim.SetBool("Hide", true);
            }
        }
    }
}
