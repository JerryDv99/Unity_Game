using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private string[] SceneName = { "MainScene", "StoryScene", "GameScene" };

    private int Index;

    private void Start()
    {
        Index = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetNextScene();
    }

    void SetNextScene()
    {
        if (Index >= SceneName.Length)
            return;

        LoadingController.SetLoad(SceneName[Index]);
        Index++;
    }
}
