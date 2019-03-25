using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingHealth : MonoBehaviour {

    public int health = 100;
    private bool isAttacked = false;
    public GameObject townCenter;
    public GameObject gameOver;

    /// <summary>
    /// Sets the isAttacked boolean to false as the start value.
    /// </summary>
    void Start () {

        isAttacked = false;
		
	}
	
	/// <summary>
    /// The Update function checks the health of the item which this script is attached to (Town Center). 
    /// If the health of this item is, or is less than 0, it will set the game object gameOver 
    /// (which is the game over screen) active. As well as destroy the game object townCenter 
    /// (which is the Town Center).
    /// </summary>
	void Update () {

        if(health <= 0)
        {
            Destroy(townCenter);
            gameOver.SetActive(true);
        }
		
	}

    /// <summary>
    /// Checks if the trigger of the game object collides with an object with the tag "Enemy". If so,
    /// it will write a message in the console, set the isAttacked bool to true and start the Coroutine
    /// "DecreaseHealth". If the tag of the colliding object is not "Enemy", it'll simply return.
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("HELP IM BEING ATTACKED");
            isAttacked = true;
            StartCoroutine(DecreaseHealth());
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Checks if the leaving colliding object has the tag "Enemy" and will set the isAttacked boolean 
    /// to false again. If the tag is anything else, it'll simply return.
    /// </summary>
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            isAttacked = false;
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// As long as the isAttacked boolean is "true" this IEnumerator will cause 10 damage per second.
    /// </summary>
    IEnumerator DecreaseHealth()
    {
        while(isAttacked == true)
        {
            yield return new WaitForSeconds(1);
            health -= 10;
        }
        
    }

}
