using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuBehaviour : MonoBehaviour
{
    public void startGame(){
        SceneManager.LoadScene("MovementTest");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
