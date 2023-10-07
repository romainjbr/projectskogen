using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio 
{



    Player player;

    public PlayAudio(Player player)
    {
        this.player = player;
    }


    public void Moving(bool val){
        player.audioSource1.enabled = val;
    }

    public void FallingSound(){
        player.audioSource2.Play();
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }
}
