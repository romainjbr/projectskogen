using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject pauseMenu;

    public Animator animator;

    private bool isPaused;

    private bool isOver;

    [SerializeField] public AudioSource backgroundMusic;
    [SerializeField] public AudioSource backgroundSound;
    [SerializeField] public AudioSource clickSound;

     
    private static PauseMenuScript instance;

    public static PauseMenuScript Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PauseMenuScript>();

            }
            return instance;
        }
    }

    void Awake()
    {

        isPaused = false;
        isOver = false;
        
    }

    void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.Escape) && !isOver)
        {
            if (!isPaused)
            {
                PauseAnim();
            }

            else
            {
                ResumeAnim();
            }
        }
        
    }

    public void PauseAnim(){
        animator.SetBool("In", true);
    }

    public void ResumeAnim(){
        Time.timeScale = 1f;
        animator.SetBool("Out", true);
        clickSound.Play();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        backgroundMusic.Pause();
        backgroundSound.Pause();
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        animator.SetBool("Out", false);
        animator.SetBool("In", false);

        isPaused = false;
        backgroundMusic.Play();
        backgroundSound.Play();
    }

    public void TryAgain()
    {
        animator.SetBool("TA", true);
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        clickSound.Play();
    }

    public void EndGame(){
        animator.SetBool("End", true);
        isOver = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        clickSound.Play();
    }
}
