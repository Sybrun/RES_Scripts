using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFunction : MonoBehaviour {

    public GameObject NPCPrefab;
    public Transform spawnPoint;

    private bool hasReachedLimit = false;
    private int NPCCount = 0;

    public int NPCCountLimit;
    public float spawnRate;

    /// <summary>
    /// Delaying the Spawn function with the spawnRate variable.
    /// </summary>
    void Start () {

        InvokeRepeating("VillagerSpawn", 0f, spawnRate);
    }
	
	/// <summary>
    /// Checks if the building has reached his spawn limit. If so, it will stop spawning new NPCs.
    /// </summary>
	void Update () {

        if (NPCCount >= NPCCountLimit)
        {
            hasReachedLimit = true;
        }

    }

    /// <summary>
    /// If the spawn limit hasn't been reached yet, a NPC will be spawned on the spawn point you assigned.
    /// </summary>
    public void VillagerSpawn()
    {
        if (hasReachedLimit == false)
        {
            NPCCount += 1;
            GameObject NPCGO = (GameObject)Instantiate(NPCPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            return;
        }
        
    }
}
