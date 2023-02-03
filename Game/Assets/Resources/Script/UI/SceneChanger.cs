using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private string[] SceneName = { "MainScene", "StoryScene", "GameScene" };

    [SerializeField] private int Index;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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
