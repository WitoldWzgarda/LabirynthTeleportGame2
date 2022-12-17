using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyColor //zmienna typu wyliczeniowego
{
    Red,
    Green,
    Gold
}

public class Key : PickUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public KeyColor color;

    public override void Picked()
    {
        GameManager.gameManager.PlayClip(GameManager.gameManager.pickClip);
        GameManager.gameManager.AddKey(color);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
