using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public GameObject pPrefabWalk = null;

    private ObjectPool pPool;

    private Queue<GameObject> pWalkQueue = new Queue<GameObject>();


    public Camera cam;
    public GameObject fpscamera;

    public Animator Anim;

    const int Idle = 0;
    const int Walk = 1;
    const int Bend = 2;
    const int Hide = 3;
    const int Fight = 4;

    int Index;
    float Speed;

    private void Awake()
    {
        cam = transform.GetChild(1).gameObject.GetComponent<Camera>();
        Anim = transform.GetComponent<Animator>();
    }
    void Start()
    {
        Index = Idle;
        Speed = 0.0f;
        
        fpscamera.SetActive(false);

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
                Anim.SetBool("Hide", true);
                Speed = 0.0f;
                break;
            case Fight:
                Anim.SetBool("Fight", true);
                Speed = 0.0f;
                break;
        }

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

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (fpscamera.activeInHierarchy == false)
                fpscamera.SetActive(true);
            else
                fpscamera.SetActive(false);
        }

        if (Input.mousePosition.x < 300)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (90 * Time.deltaTime), 0.0f);
        if (Input.mousePosition.x > 1620)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (90 * Time.deltaTime), 0.0f);
        if (Input.mousePosition.x < 700)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (45 * Time.deltaTime), 0.0f);
        if (Input.mousePosition.x > 1220)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (45 * Time.deltaTime), 0.0f);


        int cntLoop = 0;
        int cntLoop2 = 0;
       
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("�޹�"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);
                        
            if (currentState >= 0.95f && normalizedTime > cntLoop)
            {
                GameObject walk = this.pPool.GetQueue(this.transform);
                this.pWalkQueue.Enqueue(walk);
                this.StartCoroutine(DeleteWalk());
                cntLoop += 1;
            }
        }
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("������"))
        {
            float normalizedTime = Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float currentState = normalizedTime - Mathf.Floor(normalizedTime);

            if (currentState >= 0.95f && normalizedTime > cntLoop2)
            {
                GameObject walk = this.pPool.GetQueue(this.transform);
                this.pWalkQueue.Enqueue(walk);
                this.StartCoroutine(DeleteWalk());
                cntLoop2 += 1;
            }
        }
        /*
        
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        oPosition.x -= transform.position.x - camera.transform.position.x;


        //ī�޶� �ո鿡�� �ڷ� ���� �ֱ� ������, ���콺 position�� z�� ������ 
        //���� ������Ʈ�� ī�޶���� z���� ���̸� �Է½������ �մϴ�.        
        mPosition.z = oPosition.z - camera.transform.position.z;

        //ȭ���� �ȼ����� ��ȭ�Ǵ� ���콺�� ��ǥ�� ����Ƽ�� ��ǥ�� ��ȭ�� ��� �մϴ�.
        //�׷���, ��ġ�� ã�ư� �� �ְڽ��ϴ�.
        Vector3 target = camera.ScreenToWorldPoint(mPosition);

        //������ ��ũź��Ʈ(arctan, ��ź��Ʈ)�� ���� ������Ʈ�� ��ǥ�� ���콺 ����Ʈ�� ��ǥ��
        //�̿��Ͽ� ������ ���� ��, ���Ϸ�(Euler)ȸ�� �Լ��� ����Ͽ� ���� ������Ʈ�� ȸ����Ű��
        //����, �� ���� �Ÿ����� ���� �� ���Ϸ� ȸ���Լ��� �����ŵ�ϴ�.

        //�켱 �� ���� �Ÿ��� ����Ͽ�, dy, dx�� ������ �Ӵϴ�.
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;

        //������ ȸ�� �Լ��� 0���� 180 �Ǵ� 0���� -180�� ������ �Է� �޴µ� ���Ͽ�
        //(���� 270�� ���� ���� �Էµ� ���� ���������ϴ�.) ��ũź��Ʈ Atan2()�Լ��� ��� ���� 
        //���� ��(180���� ����(3.141592654...)��)���� ��µǹǷ�
        //���� ���� ������ ��ȭ�ϱ� ���� Rad2Deg�� �����־�� ������ �˴ϴ�.
        float rotateDegree = Mathf.Atan2(dx, dy) * Mathf.Rad2Deg;

        //������ ������ ���Ϸ� ȸ�� �Լ��� �����Ͽ� z���� �������� ���� ������Ʈ�� ȸ����ŵ�ϴ�.
        transform.rotation = Quaternion.Euler(0.0f, rotateDegree, 0.0f);
        */


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
}
