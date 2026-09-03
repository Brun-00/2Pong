using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    public void ClearData()
    {
        // Clears all saved player customization and settings.
        SaveController.Instance.ClearSave();
    }
}