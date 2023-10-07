using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] public AudioSource audioSourceClick;


    public void LaunchGame(){

        audioSourceClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

}
