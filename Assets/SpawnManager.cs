using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Singleton pattern so it persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        if (scene.name == "Test Scene")
        {
            if (comingFromScene == "NawafMainMenu")
                player.transform.position = streetSpawnAtHole;
            else if (comingFromScene == "TheKitchen")
                player.transform.position = streetSpawnAtKitchenDoor;
        }
        else if (scene.name == "TheKitchen")
        {
            player.transform.position = kitchenSpawnAtKitchenDoor;
        }
    }

    public void SetComingFrom(string sceneName)
    {
        comingFromScene = sceneName;
    }
}
