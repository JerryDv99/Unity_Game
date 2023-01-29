using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject pPrefabWalk = null;
    public GameObject Target = null;

    private ObjectPool pPool;

    private Queue<GameObject> pWalkQueue = new Queue<GameObject>();

    private MyFieldOfView View;

    public Camera cam;
    public GameObject fpscamera;
    public GameObject tpscamera;
    public GameObject Actioncamera;
    public GameObject Hidecamera;

    [SerializeField] GameObject Present;

    public Animator Anim;

    const int Idle = 0;
    const int Walk = 1;
    const int Bend = 2;
    const int Hide = 3;
    const int Fight = 4;
    const int Die = 5;
    const int Gift = 6;

    [SerializeField] int HP;
    int Index;
    float Speed;

    private void Awake()
    {
        cam = transform.GetChild(1).gameObject.GetComponent<Camera>();
        Anim = transform.GetComponent<Animator>();
        View = transform.GetComponent<MyFieldOfView>();
    }
    void Start()
    {
        Index = Idle;
        Speed = 0.0f;
        HP = 100;
        fpscamera.SetActive(false);
        tpscamera.SetActive(false);
        Actioncamera.SetActive(false);
        Hidecamera.SetActive(false);
        Present.SetActive(false);

        GameObject obj = new GameObject();
        obj.transform.SetParent(this.transform);
        obj.name = typeof(ObjectPool).Name;
        this.pPool = obj.AddComponent<ObjectPool>();
        this.pPool.Initialize(this.pPrefabWalk);
    }


    void Update()
    {
        switch (Index)
        {
            case Idle:
                Speed = 0.0f;
                Anim.SetBool("Idle", true);
                break;
            case Walk:
                Anim.SetBool("Walk", true);
                Speed = 3.0f;
                break;
            case Bend:
                Anim.SetBool("Bend", true);
                Speed = 1.5f;
                break;
            case Hide:
                Hidecamera.SetActive(true);
                Anim.SetBool("Hide", true);
                Speed = 0.0f;
                break;
            case Fight:
                Anim.SetBool("Fight", true);
                Speed = 0.0f;
                break;
            case Die:
                Anim.SetBool("Die", true);
                Speed = 0.0f;
                break;
        }

        if(HP <= 0)
        {
            Anim.SetBool("Die", true);
            Index = Die;
        }

        if(Index != Fight && Index != Die && !Anim.GetBool("Hide"))
        {
            if (Input.mousePosition.x < 300)
                transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (90 * Time.deltaTime), 0.0f);
            if (Input.mousePosition.x > 1620)
                transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (90 * Time.deltaTime), 0.0f);
            if (Input.mousePosition.x < 700)
                transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (45 * Time.deltaTime), 0.0f);
            if (Input.mousePosition.x > 1220)
                transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (45 * Time.deltaTime), 0.0f);

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
                || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                Index = Walk;
                Anim.SetBool("Idle", false);
            }

            if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
                Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                Index = Idle;
                Anim.SetBool("Walk", false);
            }
        }

        if (Index == Fight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Anim.SetBool("Attack", true);
        }
        if (HP <= 0)
            Index = Die;


        int cntLoop = 0;
        int cntLoop2 = 0;
       
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("왼발"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);
                        
            if (currentState >= 0.9f && normalizedTime > cntLoop)
            {
                GameObject walk = this.pPool.GetQueue(this.transform);
                this.pWalkQueue.Enqueue(walk);
                this.StartCoroutine(DeleteWalk());
                cntLoop += 1;
            }
        }
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("오른발"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);
                        
            if (currentState >= 0.9f && normalizedTime > cntLoop2)
            {
                GameObject walk = this.pPool.GetQueue(this.transform);
                this.pWalkQueue.Enqueue(walk);
                this.StartCoroutine(DeleteWalk());
                cntLoop2 += 1;
            }
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("무기 공격"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.65f && Anim.GetBool("Attack"))
            {
                EnemyController E = Target.GetComponent<EnemyController>();
                Anim.SetBool("Attack", false);
                E.SetHP(E.GetHP() - 50);
                E.Anim.SetTrigger("Hit");
            }
            
        }        
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("무기 내려찍기"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState <= 0.7f)
                Actioncamera.SetActive(true);
            if (currentState >= 0.75f)
            {
                EnemyController E = Target.GetComponent<EnemyController>();
                E.Anim.SetBool("Stun", true);
                Actioncamera.SetActive(false);
                Anim.SetBool("Attack2", false);
            }
            
        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("선물 주기"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            Actioncamera.SetActive(true);

            NPCController N = Target.GetComponent<NPCController>();
            if (currentState < 0.1f)
                Present.SetActive(true);
            if(currentState >= 0.75f)
            {
                Present.SetActive(false);
                N.Anim.SetBool("Hold", true);                
            }
        }

        if (Target)
        {
            if (Target.tag == "Enemy")
            {
                EnemyController E = Target.GetComponent<EnemyController>();
                if (E.GetTarget() == this.gameObject)
                {
                    Index = Fight;
                    Anim.SetBool("Walk", false);
                    Anim.SetBool("Fight", true);
                    if (!E.Anim.GetBool("Stun") && !E.Anim.GetBool("Die"))
                        transform.LookAt(Target.transform);
                }
                if (E.GetTarget() == null)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Anim.SetBool("Attack2", true);

                    }
                }
                if (E.GetHP() <= 0)
                {
                    Target = null;
                    Index = Idle;
                    Anim.SetBool("Fight", false);
                }
            }

            if(Target.tag == "NPC")
            {
                Index = Gift;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (fpscamera.activeInHierarchy == false)
                fpscamera.SetActive(true);
            else
                fpscamera.SetActive(false);                        
        }

        if(Anim.GetBool("Hide") && Input.GetKeyDown(KeyCode.X))
        {
            Index = Idle;
            Anim.SetBool("Hide", false);
            Anim.SetBool("Idle", true);
            Hidecamera.SetActive(false);
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - 180, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.F))
            Anim.SetBool("Check", false);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Anim.GetBool("Bend"))
        {
            Index = Bend;
            tpscamera.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Anim.GetBool("Bend"))
        {
            Index = Idle;
            Anim.SetBool("Bend", false);
            tpscamera.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.G) && Index == Gift)
            Anim.SetBool("Gift", true);


        if (Anim.GetBool("Hide"))
        {
            Vector3 mPosition = Input.mousePosition;
            Vector3 oPosition = transform.position;

            oPosition.x -= transform.position.x - Hidecamera.transform.position.x;


            //카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z축 정보에 
            //게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 합니다.        
            mPosition.z = oPosition.z - Hidecamera.transform.position.z;

            //화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 합니다.
            //그래야, 위치를 찾아갈 수 있겠습니다.
            Vector3 target = Hidecamera.GetComponent<Camera>().ScreenToWorldPoint(mPosition);

            //다음은 아크탄젠트(arctan, 역탄젠트)로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를
            //이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를 회전시키기
            //위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킵니다.

            //우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.
            float dy = target.y - oPosition.y;
            float dx = target.x - oPosition.x;

            //오릴러 회전 함수를 0에서 180 또는 0에서 -180의 각도를 입력 받는데 반하여
            //(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크탄젠트 Atan2()함수의 결과 값은 
            //라디안 값(180도가 파이(3.141592654...)로)으로 출력되므로
            //라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 됩니다.
            float rotateDegree = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;

            //구해진 각도를 오일러 회전 함수에 적용하여 z축을 기준으로 게임 오브젝트를 회전시킵니다.
            transform.rotation = Quaternion.Euler(0.0f, rotateDegree, 0.0f);
        }

        float axis = Input.GetAxis("Horizontal");
        float vertice = Input.GetAxis("Vertical");

        Vector3 move = vertice * Speed * Vector3.forward * Time.deltaTime;
        Vector3 moveAmout = axis * Speed * Vector3.right * Time.deltaTime;

        transform.Translate(move);
        transform.Translate(moveAmout);
    }

    IEnumerator DeleteWalk()
    {
        yield return new WaitForSeconds(1.0f);

        if (this.pWalkQueue.Count != 0)
        {
            this.pPool.InsertQueue(this.pWalkQueue.Dequeue());

            yield return null;
        }
        yield return null;
    }
    
    public void SetTarget(GameObject _Obj)
    {
        Target = _Obj;
    }
    public void SetIndex(int _index)
    {
        Index = _index;
    }

    public void SetHP(int _HP)
    {
        HP = _HP;
    }
    public int GetIndex()
    {
        return Index;
    }
    public int GetHP()
    {
        return HP;
    }
}
