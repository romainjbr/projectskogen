using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackCommand : Command
{

    private Player player;

    public AttackCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.Attack();
    }
}
