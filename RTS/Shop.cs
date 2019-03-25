using UnityEngine;

public class Shop : MonoBehaviour {

    public BuildingBlueprint house;
    public BuildingBlueprint tower;
    public BuildingBlueprint barracks;
    public BuildingBlueprint resourceCamp;
    public BuildingBlueprint archery;

    BuildManager buildManager;

    /// <summary>
    /// Sets the buildManager variable equal to an instance of the BuildManager script.
    /// </summary>
	void Start ()
	{
		buildManager = BuildManager.instance;
	}

    /// <summary>
    /// Button functionality which allows the user to select a building to build. If the House button is pressed,
    /// a House will be selected and there will be a message in the Console to let you know you successfully
    /// selected a House.
    /// </summary>
    public void SelectHouse ()
	{
		Debug.Log("House selected");
		buildManager.SelectBuildingToBuild (house);
	}

    /// <summary>
    /// Button functionality which allows the user to select a building to build. If the Resource Camp button is pressed,
    /// a Resource Camp will be selected and there will be a message in the Console to let you know you successfully
    /// selected a Resource Camp.
    /// </summary>
	public void SelectResourceCamp ()
	{
		Debug.Log ("Resource Camp selected");
		buildManager.SelectBuildingToBuild (resourceCamp);
	}

    /// <summary>
    /// Button functionality which allows the user to select a building to build. If the Tower button is pressed,
    /// a Tower will be selected and there will be a message in the Console to let you know you successfully
    /// selected a Tower.
    /// </summary>
	public void SelectTower ()
	{
		Debug.Log ("Tower selected");
		buildManager.SelectBuildingToBuild (tower);
	}

    /// <summary>
    /// Button functionality which allows the user to select a building to build. If the Barracks button is pressed,
    /// a Barrack will be selected and there will be a message in the Console to let you know you successfully
    /// selected a Barrack.
    /// </summary>
	public void SelectBarracks ()
	{
		Debug.Log ("Barracks selected");
		buildManager.SelectBuildingToBuild (barracks);
	}

    /// <summary>
    /// Button functionality which allows the user to select a building to build. If the Archery button is pressed,
    /// an Archery will be selected and there will be a message in the Console to let you know you successfully
    /// selected an Archery.
    /// </summary>
    public void SelectArchery()
    {
        Debug.Log("Archery selected");
        buildManager.SelectBuildingToBuild(archery);
    }
}
