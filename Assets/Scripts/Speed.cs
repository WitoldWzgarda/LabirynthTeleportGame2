using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : PickUp
{
    public uint time = 5;

    public override void Picked()
    {
        GameManager.gameManager.SpeedUp((int)time);
        Destroy(this.gameObject);
    }
}
