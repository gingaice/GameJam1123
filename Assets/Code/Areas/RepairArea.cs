using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairArea : AreaBase
{
    protected override void Effect(PlayerBase player)
    {
        player.Repair(5);
        base.Effect(player);
    }
}
