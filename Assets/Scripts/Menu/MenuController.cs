using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGame(int charSkinIndex)
    {
        GlobalInformation.CharacterSkinIndex = charSkinIndex;
        GlobalInformation.currentScene = 1;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
