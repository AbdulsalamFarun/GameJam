using UnityEngine;
using UnityEngine.SceneManagement;

public class Exitkitchen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance.SetComingFrom(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Test Scene");

        }
    }
}
