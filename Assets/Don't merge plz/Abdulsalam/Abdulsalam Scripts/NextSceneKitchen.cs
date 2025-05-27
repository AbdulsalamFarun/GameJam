using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneKitchen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SpawnManager.Instance.SetComingFrom(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("TheKitchen");
        }
    }
}
