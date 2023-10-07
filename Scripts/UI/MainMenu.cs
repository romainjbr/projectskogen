using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator animator;

    [SerializeField] public AudioSource audioSourceClick;

    public void PlayGame()
        {
            animator.SetBool("go", true);
            audioSourceClick.Play();
        }
    
    public void QuitGame()
    {
        Application.Quit();
        audioSourceClick.Play();
    }
}
