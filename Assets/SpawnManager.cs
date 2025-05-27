using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public Vector3 streetSpawnAtHole;
    public Vector3 streetSpawnAtKitchenDoor;
    public Vector3 kitchenSpawnAtKitchenDoor;

    [HideInInspector]
    public string comingFromScene = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetComingFrom(string sceneName)
    {
        comingFromScene = sceneName;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedSpawn(scene.name));
    }

    private IEnumerator DelayedSpawn(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in scene.");
            yield break;
        }

        if (sceneName == "Test Scene")
        {
            Debug.Log("Spawning player in Test Scene");
            if (comingFromScene == "NawafMainMenu")
                player.transform.position = streetSpawnAtHole;
            else if (comingFromScene == "TheKitchen")
                player.transform.position = streetSpawnAtKitchenDoor;
        }
        else if (sceneName == "TheKitchen")
        {
            player.transform.position = kitchenSpawnAtKitchenDoor;
        }
    }
}
