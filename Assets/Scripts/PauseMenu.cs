using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class PauseMenu : MonoBehaviour
{
   
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
  //public GameObject soundmanager; 
  //public AudioMixer musicMixer; 

  public static float musicVolume { get; private set; }
  public static float soundEffectsVolume { get; private set; }

  [SerializeField] private TextMeshProUGUI musicSliderText;
  [SerializeField] private TextMeshProUGUI soundEffectsSliderText;

    private void Start()
    {
        Time.timeScale = 1f;
        
    }
    private void Update()
    {
        if (GameIsPaused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            Stop();
            // soundmanager.SetActive(false);
        }
        else if (GameIsPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Play();
           // soundmanager.SetActive(true);
        }
        
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 
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
    
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        
        musicSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.Instance.UpdateMixerVolume();
    }
    
    
    
}
