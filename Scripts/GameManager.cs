using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    


    private Animator _pauseAnimator;
    
    [SerializeField]
    private GameObject PauseMainMenu;
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.R) || CrossPlatformInputManager.GetButtonDown("Fire") && _isGameOver == true)
        //{
          //  SceneManager.LoadScene(1);
            //Time.timeScale = 1;
        //}

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
              PauseMainMenu.SetActive(true);
              _pauseAnimator.SetBool("IsPause", true);
            Time.timeScale = 0;
            
            
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void PPauseTheGame()
    {
        
        {
            PauseMainMenu.SetActive(true);
            _pauseAnimator.SetBool("IsPause", true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMainMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseMainMenu.SetActive(false);
    }
    

    
}
