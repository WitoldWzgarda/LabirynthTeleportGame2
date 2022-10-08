using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : PickUp
{
    public bool addTime; //true dodaje, false odejmuje czas
    public uint time = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Picked()
    {
        int sign;
        if (addTime)
        {
            sign = 1;
        } else
        {
            sign = -1;
        }
        GameManager.gameManager.AddTime((int)time * sign);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
