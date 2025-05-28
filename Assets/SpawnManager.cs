using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject streetSpawnAtHole;
    public GameObject streetSpawnAtKitchenDoor;
    public GameObject kitchenSpawnAtKitchenDoor;
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
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in scene.");
            yield break;
        }
        player.GetComponent<Rigidbody>().isKinematic = true; 
        player.GetComponent<RatController>().enabled = false;
        if (sceneName == "Test Scene")
        {
            if (comingFromScene == "NawafMainMenu")
            {
                player.transform.localPosition = streetSpawnAtHole.transform.localPosition;
            }
            else if (comingFromScene == "TheKitchen")
            {
                player.transform.position = streetSpawnAtKitchenDoor.transform.position;
            }
        }
            else if (sceneName == "TheKitchen")
            {
                player.transform.position = kitchenSpawnAtKitchenDoor.transform.position;
            }
        if (sceneName == "FinalLeaderboard")
        {
            player.SetActive(false);
        }
         yield return new WaitForSeconds(0.1f);
    }
}
