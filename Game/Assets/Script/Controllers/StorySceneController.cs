using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneController : MonoBehaviour
{

    public int Index;

    [SerializeField] GameObject Obj1;
    [SerializeField] GameObject Obj2;
    [SerializeField] GameObject Obj3;
    [SerializeField] GameObject Obj4;

    float x;
    float y;

    Vector3 Pos;

    bool Check;

    void Start()
    {
        Index = 0;
        Check = false;
        x = Obj1.GetComponent<RectTransform>().sizeDelta.x;
        y = Obj1.GetComponent<RectTransform>().sizeDelta.y;
        Pos = Obj1.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(Index)
            {
            case 0:
                    Scope(Obj1, x, y, Pos);                    
                    break;
            case 1:
                    if(!Check)
                    {
                        x = Obj2.GetComponent<RectTransform>().sizeDelta.x;
                        y = Obj2.GetComponent<RectTransform>().sizeDelta.y;
                        Pos = Obj2.transform.localPosition;
                    }                    
                    Scope(Obj2, x, y, Pos);
                    break;
            case 2:
                    if(!Check)
                    {
                        x = Obj3.GetComponent<RectTransform>().sizeDelta.x;
                        y = Obj3.GetComponent<RectTransform>().sizeDelta.y;
                        Pos = Obj3.transform.localPosition;
                    }
                    
                    Scope(Obj3, x, y, Pos);
                    break;
            case 3:
                    if(!Check)
                    {
                        x = Obj4.GetComponent<RectTransform>().sizeDelta.x;
                        y = Obj4.GetComponent<RectTransform>().sizeDelta.y;
                        Pos = Obj4.transform.localPosition;
                    }
                    
                    Scope(Obj4, x, y, Pos);
                    break;
            }
            

        }
    }

    public void Scope(GameObject _Obj, float _x, float _y, Vector3 _pos)
    {
        RectTransform rectTran = _Obj.GetComponent<RectTransform>();


        if (!Check)
        {
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1200);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 700);            
            _Obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            
            Check = true;
        }
        else
        {
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _x);
            rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _y);
            _Obj.transform.localPosition = _pos;
            
            Index++;
            Check = false;
            _Obj.gameObject.SetActive(false);
        }
    }
}
