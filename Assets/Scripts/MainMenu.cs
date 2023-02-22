using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private void Start()
    {
        _animator.SetInteger("AnimationPar", 2);
    }

    public void PlayGame()
    {
        
        SceneManager.LoadScene("Game");

    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
