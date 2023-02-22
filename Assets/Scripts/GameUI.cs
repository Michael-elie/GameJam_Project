using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public Image _HealthBar;
    public Image donutBar;
    public Image bananeBar;
    public Image icecreamBar;
    public Image saucisseBar;
    public PlayerController _playerController; 
   
    void Start()
    {
        
    }

    
    public void Update()
    {
       //UpdateHealthBar();
    }

    private void OnEnable()
    {
        PlayerController.OnUpdateHealth += UpdateHealthBar;
    }
    private void OnDisable()
    {
        PlayerController.OnUpdateHealth -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        
        _HealthBar.fillAmount = _playerController.playerPV / _playerController.maxPlayerPV ;
    }
}
