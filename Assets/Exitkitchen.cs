using UnityEngine;
using UnityEngine.SceneManagement;

public class Exitkitchen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Test Scene");

        }
    }
}
