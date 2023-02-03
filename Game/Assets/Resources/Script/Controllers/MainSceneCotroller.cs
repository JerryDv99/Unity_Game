using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCotroller : MonoBehaviour
{
    [SerializeField] Image MCursor;
    public int Index;
    Vector3 V;

    [SerializeField] Text T1;
    [SerializeField] Text T2;

    void Start()
    {
        Index = 0;
        V = MCursor.transform.position;
        Cursor.visible = false;
    }

    
    void Update()
    {
        if(Index == 0)
        {
            T1.color = Color.yellow;
            T1.fontStyle = FontStyle.Bold;
            T1.fontSize = 65;
            T2.color = Color.black;
            T2.fontStyle = FontStyle.Normal;
            T2.fontSize = 60;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MCursor.transform.position = new Vector3(V.x, V.y - 171, V.z);
                Index++;
            }            
        }

        if(Index == 1)
        {
            T1.color = Color.black;
            T1.fontStyle = FontStyle.Normal;
            T1.fontSize = 60;
            T2.color = Color.yellow;
            T2.fontStyle = FontStyle.Bold;
            T2.fontSize = 65;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MCursor.transform.position = V;
                Index--;
            }
        }


    }
}
