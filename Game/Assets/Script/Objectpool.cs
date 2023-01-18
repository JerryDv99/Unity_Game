using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject pPrefabObj = null;

    public Queue<GameObject> pQueue = new Queue<GameObject>();

    public void Initialize(GameObject _pPrefabObj)
    {
        this.pPrefabObj = _pPrefabObj;
        for(int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(_pPrefabObj, this.transform);
            pQueue.Enqueue(obj);
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
        }
    }

    public void InsertQueue(GameObject _obj, Transform _pos)
    {
        pQueue.Enqueue(_obj);
        _obj.transform.position = new Vector3(_pos.position.x ,0.0f, _pos.position.z);
        _obj.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject obj = pQueue.Dequeue();
        obj.SetActive(true);

        return obj;
    }
}
