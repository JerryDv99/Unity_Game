using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    static string NexScene;

    [SerializeField] private Image ProgressBar;
    [SerializeField] private Image Progress;
    private float Crossline;

    void Start()
    {
        ProgressBar.fillAmount = 0.0f;
        StartCoroutine(LoadScene());
        Crossline = 0.25f;
    }

    public static void SetLoad(string _SceneName)
    {
        NexScene = _SceneName;

        SceneManager.LoadScene("NextSceneUI");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation Aop = SceneManager.LoadSceneAsync(NexScene);
        Aop.allowSceneActivation = false;

        float FakeLoadingTime = 0.0f;
        
        while (!Aop.isDone)
        {
            yield return null;

            if (Aop.progress < Crossline)
                ProgressBar.fillAmount = Aop.progress;
            else
            {
                FakeLoadingTime += Time.unscaledTime * 0.001f;
                ProgressBar.fillAmount = Mathf.Lerp(Crossline, 1.0f, FakeLoadingTime);
                Vector3 Pos = new Vector3(Progress.transform.position.x + FakeLoadingTime * 13, Progress.transform.position.y, Progress.transform.position.z);
                Progress.transform.position = Pos;

                if (ProgressBar.fillAmount >= 1.0f)
                {
                    Aop.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
