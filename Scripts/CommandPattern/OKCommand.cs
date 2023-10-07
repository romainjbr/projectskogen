using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKCommand : Command
{
    private Player player;

    public OKCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown() 
    {
        MessageBoxScript.Instance.QuitBox();   
    }

}
