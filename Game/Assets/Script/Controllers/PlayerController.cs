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


        //카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z축 정보에 
        //게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 합니다.        
        mPosition.z = oPosition.z - camera.transform.position.z;

        //화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 합니다.
        //그래야, 위치를 찾아갈 수 있겠습니다.
        Vector3 target = camera.ScreenToWorldPoint(mPosition);

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
