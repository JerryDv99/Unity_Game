using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int Index;

    public Animator Anim;

    const int Idle = 0;
    const int Walk = 1;
    const int Doubt = 2;
    const int Chase = 3;
    const int Fight = 4;
    const int Sleep = 5;
    const int Stun = 6;
    const int Die = 7;



    private void Awake()
    {
        Anim = transform.GetComponent<Animator>();
    }

    void Start()
    {
        Index = Idle;
    }

    void Update()
    {
       switch(Index)
        {
            case Stun:
                Anim.SetBool("Stun", true);
                break;
        }

        int cntLoop = 0;
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("»ç¸Á1"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.75f && cntLoop < normalizedTime)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                Index = 8;
                Anim.SetBool("Stun", false);
                cntLoop += 1;
            }

        }
    }

    

    public void SetIndex(int _Index)
    {
        Index = _Index;
    }

    public int GetIndex()
    {
        return Index;
    }
}