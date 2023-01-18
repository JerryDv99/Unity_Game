using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            EnemyController Enemy = collision.gameObject.GetComponent<EnemyController>();
            if (Enemy.GetIndex() != 4)
            {
                Enemy.SetIndex(6);
            }
            else
            {
                //int HP = Enemy.GetHP();
                //Enemy.SetHP(HP - 30);
            }
            
        }
    }
}
