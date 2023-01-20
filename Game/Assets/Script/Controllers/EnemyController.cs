using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int Index;

    [Range(0, 3)] [SerializeField] private int Index2;

    public GameObject Target;

    public Animator Anim;

    const int Idle = 0;
    const int Walk = 1;
    const int Doubt = 2;
    const int Fight = 4;
    const int Sleep = 5;
    const int Stun = 6;
    const int Die = 7;

    [SerializeField] int HP;


    private void Awake()
    {
        Anim = transform.GetComponent<Animator>();
    }

    void Start()
    {
        switch(Index2)
        {
            case 0:
                Index = Idle;
                break;
            case 1:
                Index = Walk;
                break;
            case 2:
                Index = Sleep;
                break;
        }
        HP = 100;
    }

    void Update()
    {       

        int cntLoop = 0;
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("¶óÀÌÆ®ÈÅ"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.55f && Anim.GetBool("Attack"))
            {
                PlayerController P = Target.GetComponent<PlayerController>();
                Anim.SetBool("Attack", false);
                P.SetHP(P.GetHP() - 30);
                if (Index == Fight)
                    StartCoroutine(Punch());
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("»ç¸Á1"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.75f && cntLoop < normalizedTime)
            {                
                Anim.SetBool("Stun", false);
                cntLoop += 1;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            }
        }
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("»ç¸Á2"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.75f && Index == Die)
            {
                Index = 8;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            }
        }

        if (HP <= 0)
        {
            Index = Die;
            Anim.SetBool("Die", true);
        }

        if (Target != null && Index != Die)
        {
            Index = Fight;
            Anim.SetBool("Fight", true);
            transform.LookAt(Target.transform);
            StartCoroutine(Punch());
        }
    }

    public void SetTarget(GameObject _Obj)
    {
        Target = _Obj;
    }

    public GameObject GetTarget()
    {
        return Target;
    }

    public void SetIndex(int _Index)
    {
        Index = _Index;
    }

    public int GetIndex()
    {
        return Index;
    }

    public void SetHP(int _HP)
    {
        HP = _HP;
    }

    public int GetHP()
    {
        return HP;
    }

    IEnumerator Punch()
    {
        yield return new WaitForSeconds(3.0f);
       
        Anim.SetBool("Attack", true);            
        
        yield return null;
    }
}