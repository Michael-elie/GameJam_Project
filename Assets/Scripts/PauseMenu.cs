using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
   
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
  //  public GameObject soundmanager; 


    private void Start()
    {
        Time.timeScale = 1f;
        
    }
    private void Update()
    {
        if (GameIsPaused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Stop();
            //soundmanager.SetActive(false);
        }
        else if (GameIsPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Play();
           // soundmanager.SetActive(true);
        }
        
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void  PauseGame()
    {
        
        if (GameIsPaused)
        {
            Play();
        }
        else
        {
            Stop();
        }

        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public  void Play()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void Stop()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

   
    public void QuitGame()
    {
     Application.Quit();
    }
    
    
    
    
    
}
