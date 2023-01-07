using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public float speed;
    public LayerMask canHit;
    bool move = false;
    bool first = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //Debug.Log(Physics.Raycast(transform.position, transform.up, out hit, 100) + "   " + hit.transform.tag);
        if (first && !move && Physics.Raycast(transform.position, transform.up, out hit, 100) && hit.transform.tag == "Player")
        {
            Debug.Log("SHOOOOOOOT!!!");
            move = true;
            first = false;
        }

        if (Physics.Raycast(transform.position, transform.up, out hit, 1f) && hit.collider.transform.tag != "Player")
        {
            move = false;
            Debug.Log("Stopped");
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed);
        }
    }
}
