using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodeManager : SingletonManager<NodeManager>
{   
    [Range(-100.0f, 100.0f)]
    private float Height;

    private Vector3 StartNode;
    private Vector3 EndNode;

    static public LayerMask Mask;

    private List<Vector3> Points = new List<Vector3>();

    private void Start()
    {
        Mask = ~(1 << 9);
        /*
        List<float> floating = new List<float>();

        // 난수 10개 추출
        for(int i = 0; i < 10; ++i)
        {
            while(true)
            {
                float fTemp = Random.Range(1, 100);

                if (!floating.Contains(fTemp))
                {
                    floating.Add(fTemp);
                    break;
                }
            }
        }

        // 람다식으로 정렬 ( n1 < n2 ) 오름차순
        floating.Sort((t, d) => (tag.CompareTo(d)));

        for (int i = 0; i < 10; ++i)
            Debug.Log(floating[i]);
        */

        Height = 0.0f;

        StartNode = new Vector3(-70.0f, 0.0f, 0.0f);
        EndNode = new Vector3(-60.0f, 0.0f, 0.0f);

        BezierList();
    }

    void Update()
    {
        for (int i = 0; i < Points.Count - 1; ++i)
            Debug.DrawLine(Points[i], Points[i + 1], Color.green);
    }

    public void BezierList()
    {
        Vector3 temp, dest;

        Height = Vector3.Distance(StartNode, EndNode);

        temp = new Vector3(StartNode.x, StartNode.y, StartNode.z + Height);
        dest = new Vector3(EndNode.x, EndNode.y, EndNode.z + Height);

        Vector3[] Nodes = new Vector3[5];

        float ratio = 0.0f;

        Points.Clear();

        Points.Add(StartNode);
        while (ratio < 1.0f)
        {
            ratio += Time.deltaTime;

            Nodes[0] = Vector3.Lerp(StartNode, temp, ratio);
            Nodes[1] = Vector3.Lerp(temp, dest, ratio);
            Nodes[2] = Vector3.Lerp(dest, EndNode, ratio);
            Nodes[3] = Vector3.Lerp(Nodes[0], Nodes[1], ratio);
            Nodes[4] = Vector3.Lerp(Nodes[1], Nodes[2], ratio);

            Points.Add(
                Vector3.Lerp(Nodes[3], Nodes[4], ratio));

        }
        Points.Add(EndNode);
    }

    // 모든 노드의 거리의 합을 반환하는 함수
    public static float NodeResult(List<Node> nodes)
    {
        // 노드가 100개 이상이면 계산을 하지 않는다
        if (nodes.Count >= 100)
            return 0.0f;

        float fDistance = .0f;

        // 모든 노드를 확인하면서 합을 구한다
        for (int i = 0; i < nodes.Count - 1; ++i)
        {
            fDistance = Vector3.Distance(
                nodes[i].transform.position,
                nodes[i + 1].transform.position);
        }

        // 누적된 합 반환
        return fDistance;
    }

    // 레이의 높이보다 낮은 버텍스를 확인하여 반환하는 함수
    public static List<Vector3> GetVertices(GameObject Object)
    {
        MeshFilter filter = Object.transform.GetComponent<MeshFilter>();

        List<Vector3> VertexList = new List<Vector3>();

        if (filter != null)
        {
            // MeshFilter의 mesh 탐색
            Mesh mesh = filter.mesh;

            if (mesh != null)
            {
                /*
                Vector3[] vertices = new Vector3[mesh.vertices.Length];

                for (int i = 0; i < mesh.vertices.Length; ++i)
                    vertices[i] = mesh.vertices[i];
                */

                // 조건 1. 모든 Vertex를 확인
                // 조건 2. 중복된 Vertex 배제
                for (int i = 0; i < mesh.vertices.Length; ++i)
                {
                    // 조건에 맞는 모든 Vertex를 VertexList에 담는다
                    if (!VertexList.Contains(mesh.vertices[i]) && 0.0f > mesh.vertices[i].y)
                        VertexList.Add(mesh.vertices[i]);
                }
            }
        }


        // VertexList 반환
        return VertexList;
    }
    /*
    public Node GetNode(GameObject Object, RaycastHit hit)
    {
        // 현재 목표지점을 받아온다.
        TestController test = Object.GetComponent<TestController>();
        Node front = test.GetOldTarget();

        // 다음 목표지점을 받아온다
        Node end = front.next;

        // 바닥지점에 있는 버텍스 리스트
        List<Vector3> Vertices = GetVertices(hit.transform.gameObject);

        // 출발지점, 중간지점, 도착지점 까지의 거리를 모두 별도로 보관할 변수
        float[] frontDistance = new float[Vertices.Count];
        float[] middleDistance = new float[Vertices.Count];
        float[] backDistance = new float[Vertices.Count];

        for (int i = 0; i < Vertices.Count; ++i)
        {
            frontDistance[i] = 0.0f;
            middleDistance[i] = 0.0f;
            backDistance[i] = 0.0f;
        }
        // 중간지점을 확인
        Vector3 middle = Vector3.Lerp(front.transform.position, end.transform.position, 0.3f);

        // 모든 버텍스의 위치와 거리를 확인
        for (int i = 0; i < Vertices.Count; ++i)
        {
            frontDistance[i] += Vector3.Distance(front.transform.position, Vertices[i]);
            middleDistance[i] += Vector3.Distance(middle, Vertices[i]);
            //backDistance[i] += Vector3.Distance(back, Vertices[i]);
            backDistance[i] += 0.0f;
        }

        // 거리를 저장하기 위한 공간
        float fResult = frontDistance[0] + middleDistance[0] + backDistance[0];
        int index = 0;

        for (int i = 1; i < Vertices.Count; ++i)
        {
            if (fResult < frontDistance[i] + middleDistance[i] + backDistance[i])
            {
                fResult = frontDistance[i] + middleDistance[i] + backDistance[i];
                index = i;
            }
        }





        //=


        // Vertex 저장 공간
        List<Vector3> VertexList = new List<Vector3>();

        // 
        Vector3[] BottomPoint = new Vector3[Vertices.Count];

        for (int i = 0; i < BottomPoint.Length; ++i)
        {
            BottomPoint[i] = new Vector3(
                Vertices[i].x,// * hit.transform.lossyScale.x,
                0.1f,
                Vertices[i].z);// * hit.transform.lossyScale.z);

            Matrix4x4 RotationMatrix;
            Matrix4x4 PositionMatrix;
            Matrix4x4 ScaleMatrix;

            PositionMatrix = MathManager.Translate(hit.transform.position);

            Vector3 eulerAngles = hit.transform.eulerAngles * Mathf.Deg2Rad;

            RotationMatrix = MathManager.RotationX(eulerAngles.x)
                * MathManager.RotationY(eulerAngles.y)
                * MathManager.RotationZ(eulerAngles.z);

            ScaleMatrix = MathManager.Scale(hit.transform.lossyScale * 1.5f);

            Matrix4x4 Matrix = PositionMatrix * RotationMatrix * ScaleMatrix;

            VertexList.Add(Matrix.MultiplyPoint(BottomPoint[i]));
        }

        VertexList.Sort((temp, dest) => Vector3.Distance(hit.point, temp).CompareTo(
            Vector3.Distance(hit.transform.position, dest)));

        // 빈 노드 생성
        Node node = front;

        {
            GameObject Obj = new GameObject("zero" + (0).ToString());
            Obj.transform.SetParent(front.transform);
            node.next = Obj.AddComponent<Node>();
            node = node.next;
            node.transform.position = new Vector3(Object.transform.position.x, 0.6f, Object.transform.position.z);
        }

        for (int i = 0; i < 2; ++i)
        {
            GameObject CurrentObject = new GameObject("zero" + (i + 1).ToString());
            CurrentObject.transform.SetParent(front.transform);

            node.next = CurrentObject.AddComponent<Node>();
            node = node.next;

            node.transform.position = new Vector3(VertexList[i].x, 0.6f, VertexList[i].z);
        }

        node.next = end;

        return front;
    }*/
}
