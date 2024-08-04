using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private Button buttonContinue;
    private Button buttonRestart;
    private Button buttonExit;

    public GameObject optionsPrefab;
    private GameObject optionsInstance;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked()
    {
        optionsInstance = Instantiate(optionsPrefab,transform.position, transform.rotation);
        // optionsInstance.transform.SetParent(transform, false);
        optionsInstance.SetActive(true);
        optionsInstance.transform.position = Vector3.zero;

        GameObject bContinue = GameObject.Find("ButtonContinue");
        buttonContinue = bContinue.GetComponent<Button>();
        GameObject bRestart = GameObject.Find("ButtonRestart");
        buttonRestart = bRestart.GetComponent<Button>();
        GameObject bExit = GameObject.Find("ButtonExit");
        buttonExit = bExit.GetComponent<Button>();

        buttonContinue.onClick.AddListener(OnContinueClicked);
        buttonRestart.onClick.AddListener(OnRestartClicked);
        buttonExit.onClick.AddListener(OnExitClicked);
    }

    void OnContinueClicked()
    {
        Destroy(optionsInstance);
    }

    void OnRestartClicked()
    {
        string curSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadSceneAsync(curSceneName);
    }

    void OnExitClicked()
    {
        string curSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive).completed += (AsyncOperation operation) =>
        {
            SceneManager.UnloadSceneAsync(curSceneName);
        };
    }

    
}
