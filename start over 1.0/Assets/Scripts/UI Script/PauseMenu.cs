using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;


    //public Canvas canvas;
    public GameObject PauseMenuUI;
    public GameObject BGMObejct;
	public AudioSource BGM;

    

    void Start() {
		this.BGM = BGMObejct.GetComponent<AudioSource>();
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        
        if (Input.GetButtonDown("Cancel"))
        {
            if(sceneName == "mainScene" || sceneName == "Tutorial")
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            else
            {

            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        BGM.Play();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
        
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        BGM.Pause();
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void quitGame()
    {
        SceneManager.LoadScene(0);
        Resume();
        Cursor.lockState = CursorLockMode.None;
    }

	public void turtorial()
	{
        
        SceneManager.LoadScene(4);
	}

    public void quit()
    {
        
        Application.Quit();
    }

}
