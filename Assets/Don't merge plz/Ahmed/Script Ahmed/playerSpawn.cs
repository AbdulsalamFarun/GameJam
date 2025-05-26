using System.Collections;
using UnityEngine;

public class playerSpawn : MonoBehaviour
{

    public GameObject spawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        GameObject player = GameObject.FindWithTag("Player");
        InventorySystem inventory = player.GetComponent<InventorySystem>();


        if (inventory.InventoryItems.Count>=1)
        player.GetComponent<RatController>().enabled = false;
        player.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
        if (player != null)
        {
            player.transform.position = spawn.transform.position;
            Debug.Log("Player spawned at: " + spawn.transform.position);
        }
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        player.GetComponent<RatController>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
