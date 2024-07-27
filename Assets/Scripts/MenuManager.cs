using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider processBar;
    void Start()
    {
        
    }

    public void LoadGame()
    {
        StartCoroutine(StartLoading_4(1));
    }

    private IEnumerator StartLoading_4(int scene)
    {
        int displayBar = 0;
        int targetBar;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            targetBar = (int)op.progress * 100;
            while (displayBar < targetBar)
            {
                ++displayBar;
                SetBarValue(displayBar);
                yield return new WaitForEndOfFrame();
            }
        }

        targetBar = 100;
        while (displayBar < targetBar)
        {
            ++displayBar;
            SetBarValue(displayBar);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    private void SetBarValue(float v)
    {
        processBar.value = v / 100;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;//如果是在unity编译器中

#else

        Application.Quit();//否则在打包文件中

#endif   
    }

}
