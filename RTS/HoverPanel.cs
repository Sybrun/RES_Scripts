using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPanel : MonoBehaviour {

    public GameObject hoverPanel_House;
    public GameObject hoverPanel_Camp;
    public GameObject hoverPanel_Tower;
    public GameObject hoverPanel_Barracks;
    public GameObject hoverPanel_Archery;
    public GameObject button;

    /// <summary>
    /// This Script checks if the mouse enters one of the building buttons. If the mouse enters one of the buttons,
    /// a hover panel with information about the building will be set to active. If the mouse leaves the button,
    /// the hover panel will be set inactive again.
    /// </summary>
    public void OnPointerEnter()
    {

        if (button.name == "ShopHouseItem")
        {
            hoverPanel_House.SetActive(true);
        }
        if (button.name == "ShopResourceCampItem")
        {
            hoverPanel_Camp.SetActive(true);
        }
        if (button.name == "ShopTowerItem")
        {
            hoverPanel_Tower.SetActive(true);
        }
        if (button.name == "ShopBarracksItem")
        {
            hoverPanel_Barracks.SetActive(true);
        }
        if (button.name == "ShopArcheryItem")
        {
            hoverPanel_Archery.SetActive(true);
        }

    }

    public void OnPointerExit()
    {

        if (button.name == "ShopHouseItem")
        {
            hoverPanel_House.SetActive(false);
        }
        if (button.name == "ShopResourceCampItem")
        {
            hoverPanel_Camp.SetActive(false);
        }
        if (button.name == "ShopTowerItem")
        {
            hoverPanel_Tower.SetActive(false);
        }
        if (button.name == "ShopBarracksItem")
        {
            hoverPanel_Barracks.SetActive(false);
        }
        if (button.name == "ShopArcheryItem")
        {
            hoverPanel_Archery.SetActive(false);
        }

    }





}
