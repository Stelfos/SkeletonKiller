using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject parent1;
    public GameObject parent2;

    public void OnEnable()
    {
        parent1 = GameObject.Find("ParentStats1");
        parent2 = GameObject.Find("ParentStats2");
    }
    public void Play()
    {      
        StartCoroutine(ReloadAsyncScene());
    }
    IEnumerator ReloadAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Scene sceneToLoad = SceneManager.GetSceneByName(SceneManager.GetActiveScene().name);

        parent1.transform.parent = null;
        DontDestroyOnLoad(parent1); 
        parent2.transform.parent = null;
        DontDestroyOnLoad(parent2);
        SceneManager.UnloadSceneAsync(currentScene);

        // Unload the previous Scene

        newPlayer();     
    }
    void newPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponentInChildren<PlayerStats>()
            .GenerateChild(GameObject.Find("ParentStats1").GetComponent<PlayerStats>(), GameObject.Find("ParentStats2").GetComponent<PlayerStats>());
        Debug.Log("succes");
        Time.timeScale = 1;
    }
}
