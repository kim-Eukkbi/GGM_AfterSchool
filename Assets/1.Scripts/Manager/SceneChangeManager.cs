using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance;

    [SerializeField , Header("µð¹ö±ë¿ë")]
    private int sceneindex;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(SceneLoadCo(index));
    }

    private IEnumerator SceneLoadCo(int index)
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(index);

        while(!sceneLoad.isDone)
        {
            yield return null;
        }

        //InputManager.Instance.ClearAllValues();
        sceneindex = index;
    }
}
