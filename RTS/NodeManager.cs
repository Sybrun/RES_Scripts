using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour {

    public enum ResourceTypes { Wood, Stone, Steel}
    public ResourceTypes resourceType;

    public float harvestTime;
    public float availableResource;

    public int gatherers;

	/// <summary>
    /// Starts the ResourceTick() Coroutine.
    /// </summary>
	void Start () {
        StartCoroutine(ResourceTick());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// If the resources in game (wood, stone, steel) could've been destroyed (which they currently can't), 
    /// this Function would've looked for the amount of Villagers that were gathering resources and substract that
    /// from the total available resources.
    /// </summary>
    public void ResourceGather()
    {
        if (gatherers != 0)
        {
            availableResource -= gatherers;
        }
    }

    /// <summary>
    /// This Function causes a 1 second delay each time before the ResourceGather() is called.
    /// </summary>
    IEnumerator ResourceTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ResourceGather();
        }
    }
}
