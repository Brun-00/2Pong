using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public Color colorPlayer = Color.white;
    public Color colorEnemy = Color.white;

    private static SaveController _instance;

    public string namePlayer;
    public string nameEnemy;

    public string GetName(bool isPlayer)
    {
        // Returns the saved name for the selected player.
        return isPlayer ? namePlayer : nameEnemy;
    }

    public static SaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SaveController>();

                if (_instance == null)
                {
                    GameObject singletonObject =
                        new GameObject(typeof(SaveController).Name);

                    _instance = singletonObject.AddComponent<SaveController>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // Makes sure only one instance exists across scenes.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reset()
    {
        // Resets the current match customization.
        nameEnemy = "";
        namePlayer = "";

        colorEnemy = Color.white;
        colorPlayer = Color.white;
    }

    public void SaveWinner(string winner)
    {
        // Stores the winner of the latest match.
        PlayerPrefs.SetString("SavedWinner", winner);
    }

    public string GetLastWinner()
    {
        // Retrieves the winner saved from the previous match.
        return PlayerPrefs.GetString("SavedWinner");
    }

    public void ClearSave()
    {
        // Deletes all saved data and reloads the current scene.
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}