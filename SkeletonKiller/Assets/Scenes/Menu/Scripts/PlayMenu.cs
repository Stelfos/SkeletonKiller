using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public GameObject parent1;
    public GameObject parent2;
    public void NewGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        parent1.transform.parent = null;
        DontDestroyOnLoad(parent1);
        parent2.transform.parent = null;
        DontDestroyOnLoad(parent2);

        newPlayer();
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);


    }
    void newPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponentInChildren<PlayerStats>()
            .GenerateChild(GameObject.Find("ParentStats1").GetComponent<PlayerStats>(), GameObject.Find("ParentStats2").GetComponent<PlayerStats>());
    }
}
