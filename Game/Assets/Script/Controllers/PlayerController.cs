using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public GameObject fpscamera;

    public Animator Anim;

    const int Idle = 0;
    const int Walk = 1;
    const int Run = 2;
    const int Bend = 3;
    const int Hide = 4;
    const int Fight = 5;

    int Index;
    float Speed;

    private void Awake()
    {
        camera = transform.GetChild(1).gameObject.GetComponent<Camera>();
        Anim = transform.GetComponent<Animator>();
    }
    void Start()
    {
        Index = Idle;
        Speed = 0.0f;
        
        fpscamera.SetActive(false);
    }

    
    void Update()
    {
        switch(Index)
        {
            case Idle:
                Speed = 0.0f;
                break;
            case Walk:
                Speed = 3.0f;
                break;
            case Run:
                Speed = 7.0f;
                break;
            case Bend:
                Speed = 1.5f;
                break;
            case Hide:
                Speed = 0.0f;
                break;
            case Fight:
                Speed = 0.0f;
                break;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            Index = Walk;

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            Index = Idle;

        if (Input.mousePosition.x < 300)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (90 * Time.deltaTime), 0.0f);
        else if (Input.mousePosition.x > 1620)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (90 * Time.deltaTime), 0.0f);
        else if (Input.mousePosition.x < 700)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y - (45 * Time.deltaTime), 0.0f);
        else if (Input.mousePosition.x > 1220)
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + (45 * Time.deltaTime), 0.0f);

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (fpscamera.activeInHierarchy == false)
                fpscamera.SetActive(true);
            else
                fpscamera.SetActive(false);
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
        Debug.Log(Input.mousePosition);
        

        

        

        





    }
}
