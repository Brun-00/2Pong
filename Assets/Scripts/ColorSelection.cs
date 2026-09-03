using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    public Button uiButton;
    public Image paddleReference;

    // Determines which player's color should be saved.
    public bool isColorPlayer = false;

    public void OnButtonClick()
    {
        // Applies the selected button color to the paddle preview.
        paddleReference.color = uiButton.colors.normalColor;

        // Saves the color for the correct player.
        if (isColorPlayer)
        {
            SaveController.Instance.colorPlayer = paddleReference.color;
        }
        else
        {
            SaveController.Instance.colorEnemy = paddleReference.color;
        }
    }
}