using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int Index;

    const int Idle = 0;
    const int Walk = 1;
    const int Doubt = 2;
    const int Chase = 3;
    const int Fight = 4;
    const int Sleep = 5;
    const int Stun = 6;
    const int Die = 7;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void SetIndex(int _Index)
    {
        Index = _Index;
    }

    public int GetIndex()
    {
        return Index;
    }
}
