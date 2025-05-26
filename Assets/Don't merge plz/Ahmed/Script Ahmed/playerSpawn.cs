using UnityEngine;

public class playerSpawn : MonoBehaviour
{

    public GameObject spawn;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player.transform.position = spawn.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
