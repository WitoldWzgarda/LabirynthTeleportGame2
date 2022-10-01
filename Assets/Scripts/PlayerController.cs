using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    Vector3 velocity; //posluzy do wyliczenia predkosci
    CharacterController characterController;
    // Start is called before the first frame update

    public Transform groundCheck; //miejsce na nasz obiekt;
    public LayerMask groundMask; //grupa obiekt�w kt�re b�d� uznawane za teren

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        RaycastHit hit; //zmienna w kt�re zapisaywana jest referencja do utworzonego obiektu
        if (Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down), out hit, .4f, groundMask))
        {
            string terrainType;
            terrainType = hit.collider.gameObject.tag; // sprawdzamy tag w co udzerzyli�my 

            switch (terrainType)
            {
                default: //standardowa predkosc gdy chodzimy po terenie
                    speed = 12;
                    break;
                case "Low": // pr�dko�� gdy chodimy po terenie spowalniaj�cym
                    speed = 3;
                    break;
                case "High": // pr�dko�� gdy chodzimy po terenie  pszy�pieszaj�cym
                    speed = 30;
                    break;
            }
        }

    }
}
