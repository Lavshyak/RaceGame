using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class NextLevelSetterAndCarSelectingSceneLoader : MonoBehaviour
{
    public string nextLevelSceneName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(nameof(NextLevelSetterAndCarSelectingSceneLoader) + " Started");
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
        //SceneManager.LoadScene(levelSceneName);
        GameManager.instance.NextLevelSceneName = nextLevelSceneName;
        SceneManager.LoadScene("SelectCarScene");
    }

    // Update is called once per frame
    void Update()
    {
    }
}