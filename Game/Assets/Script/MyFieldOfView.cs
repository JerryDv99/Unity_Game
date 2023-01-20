using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyFieldOfView : MonoBehaviour
{
    [Header("시야 각도")]
    [Range(0.1f, 180.0f)]
    public float Angle;
    
    [Range(20, 200)]
    public int Count;

    [Range(1.0f, 50.0f)]
    public float Radius;

    [SerializeField] private LayerMask Mask;
    [SerializeField] private LayerMask TargetMask;

    [HideInInspector] public List<Vector3> ViewList = new List<Vector3>();
    [HideInInspector] public List<Transform> TargetList = new List<Transform>();

    private MeshFilter meshFilter;
    private Mesh mesh;


    private void Awake()
    {
        GameObject Obj = new GameObject("View");

        // 생성된 게임 오브젝트를 하위 객체로 지정
        Obj.transform.parent = this.transform;
        Obj.transform.position = transform.position;
        MeshRenderer renderer = Obj.AddComponent<MeshRenderer>();

        meshFilter = Obj.AddComponent<MeshFilter>();

        // Resources에서 Material을 들고옴
        Material material = Resources.Load("Materials/Radar") as Material;

        Resources.Load("Materials/Radar");

        renderer.material = material;

        mesh = new Mesh();
        mesh.name = "mesh";

        meshFilter.mesh = mesh;
    }

    private void Start()
    {
        Angle = 30.0f;
        Count = 25;
        Radius = 3.0f;
    }

    private void Update()
    {
        // 부채꼴 시야
        FiledOfView();

        // 중심점으로부터 Radius 만큼의 거리를 확인하여 TargetMask를 모두 찾는다
        Collider[] CollObj = Physics.OverlapSphere(transform.position, Radius, TargetMask);

        TargetList.Clear();

        foreach (Collider coll in CollObj)
        {
            // TargetMask의 모든 Target과의 방향을 구함
            Vector3 Direction = (coll.transform.position - transform.position).normalized;

            // 시야 범위에 들어온 Target을 걸러낸다 
            if (Vector3.Angle(transform.forward, Direction) < Angle)
            {
                // 거리
                float fDistance = Vector3.Distance(transform.position, coll.transform.position);

                // Mask에 맞은 객체를 걸러낸다
                if (!Physics.Raycast(transform.position, Direction, fDistance, Mask))
                    TargetList.Add(coll.transform); // 최종적으로 남은 객체를 추가
            }
        }

        if(this.tag == "Enemy")
        {
            if(TargetList.Count != 0)
            {
                if (TargetList.Find(x => x.transform).gameObject.tag == "Player")
                    this.transform.GetComponent<EnemyController>().SetTarget(TargetList.Find(x => x.transform).gameObject);
            }            
        }

        if(this.tag == "Player")
        {
            if (TargetList.Count != 0)
            {
                if (TargetList.Find(x => x.transform).gameObject.tag == "Enemy")
                    this.transform.GetComponent<PlayerController>().SetTarget(TargetList.Find(x => x.transform).gameObject);
            }            
        }
        
        
    }

    public void FiledOfView()
    {
        float fAngle = (-Angle);

        ViewList.Clear();
        for (int i = 0; i < Count; ++i)
        {
            Vector3 point = new Vector3(
                Mathf.Sin(fAngle * Mathf.Deg2Rad),
                0.0f,
                Mathf.Cos(fAngle * Mathf.Deg2Rad)).normalized;

            Ray ray = new Ray(transform.position, point);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Radius, Mask))
                ViewList.Add(hit.point - transform.position);
            else
                ViewList.Add(point * Radius);

            fAngle += (Angle * 2) / (Count - 1);
        }

        // 삼각형을 그리기 위한 좌표 ( + 1 = 시작점을 위한 공간)
        Vector3[] vertices = new Vector3[ViewList.Count + 1];
        vertices[0] = Vector3.zero;
        int[] triangles = new int[((ViewList.Count - 1) * 3)];


        for (int i = 0; i < ViewList.Count; ++i)
            vertices[i + 1] = ViewList[i];

        for (int i = 0; i < ViewList.Count - 1; ++i)
        {
            for (int j = 0; j < 3; ++j)
                //triangles[i * 3 + j] = (((i % 3) * (j % 3) + j) == 0 ? 0 : i + j);
                triangles[i * 3 + j] = (((i * 3 + j) % 3) == 0 ? 0 : i + j);
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }    
}
[CustomEditor(typeof(MyFieldOfView))]
public class MyFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        MyFieldOfView Target = (MyFieldOfView)target;


        foreach (Transform Info in Target.TargetList)
        {
            Debug.DrawLine(
                Target.transform.position,
                Info.transform.position, Color.blue);
        }

        Handles.color = Color.red;


        Handles.DrawWireArc(
            Target.transform.position,
            Target.transform.up,
            Target.transform.forward,
            360.0f,
            Target.Radius);

        /*
        foreach (Vector3 point in Target.ViewList)
        {
            Debug.DrawLine(Target.transform.position, point + Target.transform.position, Color.blue);
        }
        */

        Vector3 LeftPoint = new Vector3(
           Mathf.Sin(-Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(-Target.Angle * Mathf.Deg2Rad));

        Handles.DrawLine(Target.transform.position, Target.transform.position + LeftPoint * Target.Radius);

        Vector3 RightPoint = new Vector3(
           Mathf.Sin(Target.Angle * Mathf.Deg2Rad),
           0.0f,
           Mathf.Cos(Target.Angle * Mathf.Deg2Rad));

        Handles.DrawLine(Target.transform.position, Target.transform.position + RightPoint * Target.Radius);
    }
}