using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freez : PickUp
{
    public int freezTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Picked()
    {
        GameManager.gameManager.PlayClip(GameManager.gameManager.pickClip);
        GameManager.gameManager.FreezTime(freezTime);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
