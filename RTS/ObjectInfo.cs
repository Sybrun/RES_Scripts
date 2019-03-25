using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class ObjectInfo : MonoBehaviour {

    public TaskList task;

    GameObject targetNode;
    public NodeManager.ResourceTypes heldResourceType;
    public int heldResource;
    public int maxHeldResource = 20;

    public bool isSelected = false;
    public bool isGathering = false;

    public GameObject[] resourceCamps;

    private NavMeshAgent agent;

    //public GameObject icon;
    //public CanvasGroup InfoPanel;

    //public int health;
    //public int maxHealth;  
    //public string objectName;
    //public TMP_Text nameDisp;
    //public Slider HealthBar;
    //public TMP_Text healthDisp;

    //public int atk;
    //public int def;
    //public TMP_Text atkDisp;
    //public TMP_Text defDisp;

    /// <summary>
    /// Starts the GatherTick() Coroutine. + Calls the NavMeshAgent() Component
    /// </summary>
    void Start () {

        StartCoroutine(GatherTick());
        agent = GetComponent<NavMeshAgent>();
		
	}
	
	/// <summary>
    /// This Function sends the agent to the nearest Resource Camp if he has any resources with him 
    /// when his targetNode is set to null or when he has reached his limit of carryable resources.
    /// If not, his task is Idle.
    /// Also, this function calls the RightClick() function when the richt mouse button is pressed.
    /// And activates an infopanel of the selected NPC. If nothing is selected, the panel dissapears.
    /// </summary>
	void Update () {

        if(targetNode == null)
        {
            if (heldResource != 0)
            {
                resourceCamps = GameObject.FindGameObjectsWithTag("ResourceCamp");
                agent.destination = GetClosestDropOff(resourceCamps).transform.position;
                resourceCamps = null;
                task = TaskList.Delivering;
            }
            else
            {
                task = TaskList.Idle;
            }
        }

        if (heldResource >= maxHeldResource)
        {
            resourceCamps = GameObject.FindGameObjectsWithTag("ResourceCamp");
            agent.destination = GetClosestDropOff(resourceCamps).transform.position;
            resourceCamps = null;
            task = TaskList.Delivering;
        }

        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            RightClick();
        }

        //This part of code has been replaced by another team member's script. However, it could still be used if we change plans later. 
        /*if (health <= 0)
        {
            Destroy(gameObject);
        }
        HealthBar.maxValue = maxHealth;
        HealthBar.value = health;

        nameDisp.text = objectName;
        icon.SetActive(isSelected);
        healthDisp.text = "HP:  " + health;
        atkDisp.text = "ATK:  " + atk;
        defDisp.text = "DEF:  " + def;
        if (isSelected)
        {
            InfoPanel.alpha = 1;
            InfoPanel.blocksRaycasts = true;
            InfoPanel.interactable = true;
            
        }
        else
        {
            InfoPanel.alpha = 0;
            InfoPanel.blocksRaycasts = false;
            InfoPanel.interactable = false;
        }*/
	
	}

    /// <summary>
    /// This Function puts in an array of GameObjects (which are Resource Camps in-game) and it calculates 
    /// which dropOff (Resource Camp) has the shortest distance. After this calculation, the closest dropOff 
    /// is set as the targetDrop. This targetDrop is the new destination of our agent.
    /// </summary>
    GameObject GetClosestDropOff(GameObject[] dropOffs)
    {
   
            GameObject closestDrop = null;
            float closestDistance = Mathf.Infinity;
            Vector3 position = transform.position;

            foreach (GameObject targetDrop in dropOffs)
            {
                Vector3 direction = targetDrop.transform.position - position;
                float distance = direction.sqrMagnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDrop = targetDrop;
                }
            }


        return closestDrop;
    }

    /// <summary>
    /// If a NPC is selected, the RightClick() function will direct an agent to his new destination
    /// and will adjust it's task to the player his desired functionality.
    /// </summary>
    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 200))
        {
            if(hit.collider.tag == "Ground")
            {
                agent.destination = hit.point;
                Debug.Log("Moving");
                task = TaskList.Moving;
            }else if (hit.collider.tag == "Resource")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("Harvesting");
                task = TaskList.Gathering;
                targetNode = hit.collider.gameObject;
            }
            else if (hit.collider.tag == "EnemyBase")
            {
                agent.destination = hit.collider.gameObject.transform.position;
                Debug.Log("ATTACKING!");
                targetNode = hit.collider.gameObject;
            }
        }

    }

    /// <summary>
    /// This Function checks if the NPC has entered the trigger with an "Resource" or "ResourceCamp" tag
    /// with an extra check if the Task is set properly (and if it's in sync with what the NPC should be doing).
    /// If the tag of the trigger it collides with is "Resource", it'll set the isGathering bool to true,
    /// which will allow the resources, that are being carried by the NPC, to be added by 1 each second. 
    /// If the tag is "ResourceCamp" however, it'll add the amount of carried resource to the right resource group.
    /// (So, if 20 wood is being carried, the player will receive 20 Wood rather than 20 Steel, as it should obviously)
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource" && task == TaskList.Gathering)
        {
            isGathering = true;
            heldResourceType = hitObject.GetComponent<NodeManager>().resourceType;
            
        }else if (hitObject.tag == "ResourceCamp" && task == TaskList.Delivering)
        {
            if(heldResourceType == NodeManager.ResourceTypes.Wood)
                PlayerStats.wood += heldResource;

            else if (heldResourceType == NodeManager.ResourceTypes.Stone)
                PlayerStats.stone += heldResource;

            else if (heldResourceType == NodeManager.ResourceTypes.Steel)
                PlayerStats.steel += heldResource;

            heldResource = 0;
            task = TaskList.Gathering;
            agent.destination = targetNode.transform.position;
        }
    }

    /// <summary>
    /// if the NPC exits the trigger with the "Resource" tag, the amount of carried resources will not be
    /// added by 1 each second anymore.
    /// </summary>
    public void OnTriggerExit(Collider other)
    {
        GameObject hitObject = other.gameObject;

        if (hitObject.tag == "Resource")
        {
            isGathering = false;
        }
    }

    /// <summary>
    /// As long as the current amount of resources is not equal or more than the maximum limit we put on 
    /// the amount of carryable resources and the bool isGathering is true, the amount of current resources 
    /// that are being carried by an agent will be added up by 1 each second + when the amount is higher than
    /// the fixed limit, the amount will be set to the limit amount. This is necessary because otherwise we could 
    /// end up with an agent who has 1 or 2 more resources than we origionally wanted, due to the time it takes 
    /// for the agent to leave the "Resource" trigger. And mind that as long as an NPC is in the trigger,
    /// the resource will increase by 1.
    /// </summary>
    IEnumerator GatherTick()
    {
        if (heldResource < maxHeldResource)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (isGathering)
                {
                    heldResource++;
                    if (heldResource > maxHeldResource)
                        heldResource = maxHeldResource;
                }

            }
        }
        
    }
}
