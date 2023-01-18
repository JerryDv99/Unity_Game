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
        for(int i = 0; i < 14; i++)
        {
            GameObject obj = Instantiate(_pPrefabObj, this.transform);
            pQueue.Enqueue(obj);
            obj.transform.position = this.transform.position;
            obj.SetActive(false);
        }
    }

    public void InsertQueue(GameObject _obj)
    {
        pQueue.Enqueue(_obj);
        
        _obj.SetActive(false);
    }

    public GameObject GetQueue(Transform _pos)
    {
        GameObject obj = pQueue.Dequeue();
        obj.transform.position = new Vector3(_pos.position.x, 0.0f, _pos.position.z);
        obj.SetActive(true);

        return obj;
    }
}
