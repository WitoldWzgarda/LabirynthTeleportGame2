using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 recoil = transform.InverseTransformDirection(transform.right) * Random.Range(-0.1f, 0.1f) +
                transform.parent.InverseTransformDirection(transform.forward) * -1f;
            other.GetComponent<CharacterController>().Move(recoil * Time.deltaTime * force);
        }
    }
}
