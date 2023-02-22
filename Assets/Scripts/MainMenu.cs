using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private Scene _scene;
    private void Start()
    {
        _scene = SceneManager.GetActiveScene();
        
       /* if (_scene.name == "GameOver");
        {
            _animator.SetInteger("AnimationPar", 3);
        }*/
        if (_scene.name == "Game");
        {
            _animator.SetInteger("AnimationPar", 2);
        }
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
