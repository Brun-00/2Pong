using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text uiWinner;

    void Start()
    {
        // Resets the current match data before entering the menu.
        SaveController.Instance.Reset();

        string lastWinner = SaveController.Instance.GetLastWinner();

        // Shows the winner of the previous match, if there is one.
        if (lastWinner != "")
            uiWinner.text = "Last Winner: " + lastWinner;
        else
            uiWinner.text = "";
    }
}