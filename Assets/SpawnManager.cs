using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject streetSpawnAtHole;
    public GameObject streetSpawnAtKitchenDoor;
    public GameObject kitchenSpawnAtKitchenDoor;
    public GameObject CameraCutScene;
    public GameObject CameraTransition;

    [HideInInspector]
    public string comingFromScene = "";

    private void Awake()
    {
        CameraCutScene = GameObject.Find("Cameras");
        CameraTransition = GameObject.Find("Camears Transport");

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
        player.GetComponent<Rigidbody>().isKinematic = true; // Temporarily disable physics
        player.GetComponent<RatController>().enabled = false; // Re-enable physics after setting position
        if (sceneName == "Test Scene")
        {

            Debug.Log("Spawning player in Test Scene");
            if (comingFromScene == "NawafMainMenu")
            {
                player.transform.localPosition = streetSpawnAtHole.transform.localPosition;
            }
            else if (comingFromScene == "TheKitchen")
            {
                player.transform.position = streetSpawnAtKitchenDoor.transform.position;
                CameraTransition.gameObject.SetActive(false);
                CameraCutScene.gameObject.SetActive(false);
            }
        }
            else if (sceneName == "TheKitchen")
            {
                player.transform.position = kitchenSpawnAtKitchenDoor.transform.position;
            }
         yield return new WaitForSeconds(0.1f);
        
                        player.GetComponent<Rigidbody>().isKinematic = false;   
                player.GetComponent<RatController>().enabled = true; // Re-enable RatController  
    }
}
