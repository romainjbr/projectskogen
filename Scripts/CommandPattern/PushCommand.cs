using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCommand : Command
{
    private Player player;

    public PushCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown() 
    {
        player.Actions.isPushing = true;
        player.Actions.Push();
    }

    public override void GetKeyUp()
    {
        player.Actions.isPushing = false;
        player.Actions.Push();
    }
}
