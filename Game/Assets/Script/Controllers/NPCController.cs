using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject TargetPoint;

    public Animator Anim;

    [SerializeField] GameObject Gift;

    void Start()
    {
        Anim = transform.GetComponent<Animator>();
        Gift.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Anim.GetBool("Run"))
        {
            Vector3 dir = TargetPoint.transform.position - transform.position;

            this.transform.position += (dir.normalized * Time.deltaTime * 2.0f);

            float fDistance = Vector3.Distance(transform.position, TargetPoint.transform.position);

            this.transform.LookAt(TargetPoint.transform);

            if (fDistance < 0.2f)
            {
                Anim.SetBool("Run", false);
                Anim.SetBool("Clap", true);
            }
        }

        if (Anim.GetBool("Hold"))
            Gift.SetActive(true);
    }
}
