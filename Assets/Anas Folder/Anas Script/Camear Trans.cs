using System.Collections;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    [Header("Cameras in Order")]
    public Camera[] cameras;

    [Header("Switch Interval (seconds)")]
    public float switchInterval = 4f;

    [Header("Player Camera (activates after sequence)")]
    public Camera playerCamera;
    public GameObject UI;
    public GameObject player;
    [HideInInspector] public SpawnManager spawnManager;


    public void Start()
    {
        if (SpawnManager.Instance != null)
        {
            if (SpawnManager.Instance.comingFromScene == "NawafMainMenu")
            {
                UI.SetActive(false);

                Debug.Log("Starting camera sequence...");
                StartCoroutine(SwitchCamerasOnce());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator SwitchCamerasOnce()
    {
        player.GetComponent<InventorySystem>().enabled = false;
        player.GetComponent<RatController>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        // disable all sequence cameras and the player camera at first
        foreach (var cam in cameras)
            cam.gameObject.SetActive(false);
        if (playerCamera != null)
            playerCamera.gameObject.SetActive(false);

        // step through each camera exactly once
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(switchInterval);
            cameras[i].gameObject.SetActive(false);
        }

        // after the sequence, switch to the player camera
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(true);
        }

        UI.SetActive(true);
        player.GetComponent<InventorySystem>().enabled = true;
        player.GetComponent<RatController>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
    }
}