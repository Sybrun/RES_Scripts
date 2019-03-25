using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;

	void Awake ()
	{
		instance = this;
	}

	public GameObject housePrefab;
	public GameObject resourceCampPrefab;
	public GameObject towerPrefab;
	public GameObject barracksPrefab;
    public GameObject archeryPrefab;

    /// <summary>
    /// Sets the buildingToBuild to null (because the player has to choose a building he wants to build first).
    /// </summary>
	void Start()
	{
		buildingToBuild = null;
	}
	
	private BuildingBlueprint buildingToBuild;


    /// <summary>
    /// Checks if the buildingToBuild is not null. If it's not (So, the player has chosen a building he wants
    /// to build), the code will check if the player has enough Wood, Stone and Steel to buy the wanted building.
    /// If certain resources are lacking to afford the building, the code will simply return nothing or a message 
    /// that will inform the player that he is lacking the required materials.
    /// If the player does have enough materials, his total amount of resources will be subtracted by the cost of 
    /// the building and the purchased building will be placed on the node which was chosen by the player.
    /// Finally, there will be a message informing the player how many resources he has left after purchasing the building.
    /// </summary>
    public bool CanBuild { get { return buildingToBuild != null; } }
    public bool HasWood { get { return PlayerStats.wood >= buildingToBuild.wood_Cost; } }
    public bool HasStone { get { return PlayerStats.stone >= buildingToBuild.stone_Cost; } }
    public bool HasSteel { get { return PlayerStats.steel >= buildingToBuild.steel_Cost; } }

    public void BuildBuildingOn(Node node)
    {
        if(PlayerStats.wood < buildingToBuild.wood_Cost)
        {
            //Debug.Log("Not enough Wood!");
            return;
        }

        if (PlayerStats.stone < buildingToBuild.stone_Cost)
        {
            //Debug.Log("Not enough Stone!");
            return;
        }

        if (PlayerStats.steel < buildingToBuild.steel_Cost)
        {
            //Debug.Log("Not enough Steel!");
            return;
        }

        PlayerStats.wood -= buildingToBuild.wood_Cost;
        PlayerStats.stone -= buildingToBuild.stone_Cost;
        PlayerStats.steel -= buildingToBuild.steel_Cost;

        GameObject building = (GameObject)Instantiate(buildingToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.building = building;

        Debug.Log("Building purchased! Resources left: " + PlayerStats.wood + " Wood, " + PlayerStats.stone + " Stone, " + PlayerStats.steel + " Steel.");

    }

	public void SelectBuildingToBuild (BuildingBlueprint building)
	{
		buildingToBuild = building;
	}

}
