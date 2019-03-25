using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughColor;
    private Color startColor;

    public Vector3 positionOffset;

    [Header("Optional")]
	public GameObject building;

	private Renderer rend;

	BuildManager buildManager;

    /// <summary>
    /// Applies a StartColor to all Nodes and sets the buildManager (variable of the BuildManager Script)
    /// to BuildManager.instance.
    /// </summary>
	void Start(){

		rend = GetComponent<Renderer> ();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;

	}

    /// <summary>
    /// Calculates the position where the buildings should be placed on the node after a purchase.
    /// </summary>
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    /// <summary>
    /// Checks if there's already a building on the Node and if the player has enough materials when you click 
    /// on a Node. If there's already a building on the node, you'll get a message that you can't build there. 
    /// If the player doesn't have enough materials, the code will just return. If there's no building and 
    /// the player has enough materials however, a building will be placed on the node that was assigned by the player.
    /// </summary>
	void OnMouseDown(){

		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (!buildManager.CanBuild) 
			return;

		if (building != null) {
			Debug.Log ("Can't build there");
			return;
		}

        buildManager.BuildBuildingOn(this);

	}

    /// <summary>
    /// If you hover over a Node, it will give you visual feedback if you can build there or not.
    /// </summary>
	void OnMouseEnter(){

		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (!buildManager.CanBuild) 
			return;
		
		rend.material.color = hoverColor;

        if(buildManager.HasWood)
        {
            rend.material.color = hoverColor;
        }else
        {
            rend.material.color = notEnoughColor;
        }
        if (buildManager.HasStone)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughColor;
        }
        if (buildManager.HasSteel)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughColor;
        }


    }

	void OnMouseExit(){

		rend.material.color = startColor;

	}
}

