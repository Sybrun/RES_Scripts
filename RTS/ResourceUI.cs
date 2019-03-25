using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ResourceUI : MonoBehaviour {

    public Text woodText;
    public Text stoneText;
    public Text steelText;

    /// <summary>
    /// Sets the Text, which will be printed to the screen, equal to the material value converted to a string.
    /// </summary>
    void Update () {

        woodText.text = PlayerStats.wood.ToString();
        stoneText.text = PlayerStats.stone.ToString();
        steelText.text = PlayerStats.steel.ToString();

    }
}
