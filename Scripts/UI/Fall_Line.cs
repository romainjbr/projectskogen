using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Fall_Line : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player"){
            PauseMenuScript.Instance.TryAgain();
            vcam.gameObject.SetActive(false);
        }
    }
}
