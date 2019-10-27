using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //public Button mButton;
    //public GameObject mainmenu;
    //public GameObject optionsmenu;
    
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
    }
}
