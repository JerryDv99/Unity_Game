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
                Anim.SetBool("Idle", true);
                break;
            case 1:
                Index = Walk;
                Anim.SetBool("Walk", true);
                break;
            case 2:
                Index = Sleep;
                Anim.SetBool("Sleep", true);
                break;
        }
        HP = 100;
    }

    void Update()
    {       

        int cntLoop = 0;
        int cntLoop2 = 0;
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("¶óÀÌÆ®ÈÅ"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.65f && Anim.GetBool("Attack") && cntLoop2 < normalizedTime)
            {
                PlayerController P = Target.GetComponent<PlayerController>();
                Anim.SetBool("Attack", false);
                P.SetHP(P.GetHP() - 25);
                if (Index == Fight)
                    StartCoroutine(Punch());
                P.Anim.SetTrigger("Hit");
                cntLoop2 += 1;
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

        if (Target != null && (Index != Die && Index != Stun))
        {
            Index = Fight;
            Anim.SetBool("Fight", true);
            transform.LookAt(Target.transform);
            Target.GetComponent<PlayerController>().SetTarget(this.gameObject);
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
        yield return new WaitForSeconds(2.0f);
       
        Anim.SetBool("Attack", true);            
        
        yield return null;
    }
}