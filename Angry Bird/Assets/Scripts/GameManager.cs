using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string scenePath;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "Change Level"))
        {
            SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        }
    }
}
