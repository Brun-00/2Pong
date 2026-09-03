using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneHelper : MonoBehaviour
{
    // Name of the scene that should be opened.
    public string sceneToOpen;

    public void OpenScene()
    {
        Debug.Log("Opening scene: " + sceneToOpen);

        // Loads the selected scene.
        SceneManager.LoadScene(sceneToOpen);
    }
}