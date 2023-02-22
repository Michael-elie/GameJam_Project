using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour
{
   
    public float timeValue = 90f;
    [SerializeField] private TMP_Text TimerText;
    public PlayerController PlayerController; 
    
    
    void Update()

    {

        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;

        }

        DisplayTime(timeValue);
        if ( PlayerController.playerPV <= 0 )
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (timeValue <= 0 && PlayerController.playerPV >= 0 )
        {
            SceneManager.LoadScene("Win");
        }
        
    }
    

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0 )
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float secondes = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = "TIME LEFT : " + string.Format("{0:00}:{1:00}", minutes, secondes);
    }
    
}
