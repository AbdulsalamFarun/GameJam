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

    public void Start()
    {
        // start the sequence once on game start
        StartCoroutine(SwitchCamerasOnce());
    }

    private IEnumerator SwitchCamerasOnce()
    {
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
    }
}