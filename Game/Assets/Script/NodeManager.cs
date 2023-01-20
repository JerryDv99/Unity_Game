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

        // ���� 10�� ����
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

        // ���ٽ����� ���� ( n1 < n2 ) ��������
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

    // ��� ����� �Ÿ��� ���� ��ȯ�ϴ� �Լ�
    public static float NodeResult(List<Node> nodes)
    {
        // ��尡 100�� �̻��̸� ����� ���� �ʴ´�
        if (nodes.Count >= 100)
            return 0.0f;

        float fDistance = .0f;

        // ��� ��带 Ȯ���ϸ鼭 ���� ���Ѵ�
        for (int i = 0; i < nodes.Count - 1; ++i)
        {
            fDistance = Vector3.Distance(
                nodes[i].transform.position,
                nodes[i + 1].transform.position);
        }

        // ������ �� ��ȯ
        return fDistance;
    }

    // ������ ���̺��� ���� ���ؽ��� Ȯ���Ͽ� ��ȯ�ϴ� �Լ�
    public static List<Vector3> GetVertices(GameObject Object)
    {
        MeshFilter filter = Object.transform.GetComponent<MeshFilter>();

        List<Vector3> VertexList = new List<Vector3>();

        if (filter != null)
        {
            // MeshFilter�� mesh Ž��
            Mesh mesh = filter.mesh;

            if (mesh != null)
            {
                /*
                Vector3[] vertices = new Vector3[mesh.vertices.Length];

                for (int i = 0; i < mesh.vertices.Length; ++i)
                    vertices[i] = mesh.vertices[i];
                */

                // ���� 1. ��� Vertex�� Ȯ��
                // ���� 2. �ߺ��� Vertex ����
                for (int i = 0; i < mesh.vertices.Length; ++i)
                {
                    // ���ǿ� �´� ��� Vertex�� VertexList�� ��´�
                    if (!VertexList.Contains(mesh.vertices[i]) && 0.0f > mesh.vertices[i].y)
                        VertexList.Add(mesh.vertices[i]);
                }
            }
        }


        // VertexList ��ȯ
        return VertexList;
    }
    /*
    public Node GetNode(GameObject Object, RaycastHit hit)
    {
        // ���� ��ǥ������ �޾ƿ´�.
        TestController test = Object.GetComponent<TestController>();
        Node front = test.GetOldTarget();

        // ���� ��ǥ������ �޾ƿ´�
        Node end = front.next;

        // �ٴ������� �ִ� ���ؽ� ����Ʈ
        List<Vector3> Vertices = GetVertices(hit.transform.gameObject);

        // �������, �߰�����, �������� ������ �Ÿ��� ��� ������ ������ ����
        float[] frontDistance = new float[Vertices.Count];
        float[] middleDistance = new float[Vertices.Count];
        float[] backDistance = new float[Vertices.Count];

        for (int i = 0; i < Vertices.Count; ++i)
        {
            frontDistance[i] = 0.0f;
            middleDistance[i] = 0.0f;
            backDistance[i] = 0.0f;
        }
        // �߰������� Ȯ��
        Vector3 middle = Vector3.Lerp(front.transform.position, end.transform.position, 0.3f);

        // ��� ���ؽ��� ��ġ�� �Ÿ��� Ȯ��
        for (int i = 0; i < Vertices.Count; ++i)
        {
            frontDistance[i] += Vector3.Distance(front.transform.position, Vertices[i]);
            middleDistance[i] += Vector3.Distance(middle, Vertices[i]);
            //backDistance[i] += Vector3.Distance(back, Vertices[i]);
            backDistance[i] += 0.0f;
        }

        // �Ÿ��� �����ϱ� ���� ����
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


        // Vertex ���� ����
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

        // �� ��� ����
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
