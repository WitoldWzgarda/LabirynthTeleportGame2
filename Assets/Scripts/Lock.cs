using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public Door[] doors;
    public KeyColor myColor;
    bool iCanOpen = false;
    bool locked = false;
    Animator key;

    public bool winningPortal;
    // Start is called before the first frame update
    void Start()
    {
        key = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iCanOpen && !locked) // <--------
        {
            GameManager.gameManager.SetUseInfo("Press E to open lock"); // <--------
        } // <--------

        if (Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());
        }
    }

    public void UseKey()
    {
        foreach (Door door in doors)
        {
            door.OpenClose();

            if (winningPortal)
            {
                GameManager.gameManager.win = true;
                GameManager.gameManager.EndGame();
            }
        }
    }

    public bool CheckTheKey()
    {
        if (GameManager.gameManager.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.gameManager.redKey--;

            GameManager.gameManager.RedKeyText.text = GameManager.gameManager.redKey.ToString(); // <--------

            locked = true;
            return true;
        }
        else if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.greenKey--;

            GameManager.gameManager.GreenKeyText.text = GameManager.gameManager.greenKey.ToString(); // <--------

            locked = true;
            return true;
        }
        else if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.gameManager.goldKey--;

            GameManager.gameManager.GoldKeyText.text = GameManager.gameManager.goldKey.ToString(); // <--------

            locked = true;
            return true;
        }
        else
        {
            Debug.Log("Nie masz klucza");
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You can Use lock");

            GameManager.gameManager.SetUseInfo(""); // <--------
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You can not Use lock");
        }
    }
}
