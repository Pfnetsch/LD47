using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("Game").buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
