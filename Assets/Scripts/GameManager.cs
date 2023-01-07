using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    public bool win = false;
    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;
    public AudioClip pickClip;

    public MusicScript musicScript;
    bool lessTime = false;

    public string loseScene;
    public string winScene;

    public TextMeshProUGUI pointShow;
    public TextMeshProUGUI keysShow;
    public TextMeshProUGUI timeShow;

    public Text timeText;
    public Text GoldKeyText;
    public Text RedKeyText;
    public Text GreenKeyText;
    public Text CrystalText;
    public Image snowFlake;

    public GameObject infoPanel;
    public Text useInfo;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useEnd;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        if(timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);

        snowFlake.enabled = false; // <------
        timeText.text = timeToEnd.ToString(); // <------
        infoPanel.SetActive(true); // <------
        pauseEnd.text = "Pause"; // <------
        reloadInfo.text = ""; // <------
        SetUseInfo(""); // <------
    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
        PickUpCheck();
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    void Stopper()
    {
        timeToEnd--;
        timeShow.text = "Time to end: " + timeToEnd;
        Debug.Log("Time: " + timeToEnd + " s");

        timeText.text = timeToEnd.ToString(); // <------
        snowFlake.enabled = false; // <------

        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        if(endGame)
        {
            EndGame();
        }

        if(timeToEnd < 20 && !lessTime)
        {
            LessTimeOn();
            lessTime = true;
        } else if (timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }

    public void PauseGame()
    {
        PlayClip(pauseClip);

        musicScript.OnPauseGame();
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;

        infoPanel.SetActive(true); // <-----
    }

    public void ResumeGame()
    {
        PlayClip(resumeClip);
        musicScript.OnResumeGame();
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;

        infoPanel.SetActive(false); // <-----
    }

    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused) {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        CancelInvoke("Stopper");

        infoPanel.SetActive(true); // <------
        if (win)
        {
            PlayClip(winClip);
            Debug.Log("You win!!! Reload?");
            SceneManager.LoadScene(winScene);

            pauseEnd.text = "You Win!!!"; // <------
            reloadInfo.text = "Reload? Y/N"; // <------
        } else
        {
            PlayClip(loseClip);
            Debug.Log("You lose!!! Reload?");
            SceneManager.LoadScene(loseScene);

            pauseEnd.text = "You Lose!!!"; // <------
            reloadInfo.text = "Reload? Y/N"; // <------
        }

    }

    public void SetUseInfo(string info) // <------
    { // <------
        useInfo.text = info; // <------
    } // <------


    public void AddPoints(int point)
    {
        points += point;
        pointShow.text = "Crystals: " + points;

        CrystalText.text = points.ToString(); // <-------
        Debug.Log("Added points");
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        Debug.Log("Added time");
        timeText.text = timeToEnd.ToString(); // <-------
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
        Debug.Log("Freez time");

        snowFlake.enabled = true; // <-------
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            Debug.Log("Added gold key");
            GoldKeyText.text = goldKey.ToString(); // <-------
        } else if(color == KeyColor.Green)
        {
            greenKey++;
            Debug.Log("Added green key");
            GreenKeyText.text = greenKey.ToString(); // <-------
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            Debug.Log("Added red key");
            RedKeyText.text = redKey.ToString(); // <-------
        }

        keysShow.text = "Red keys: " + redKey + " Green keys: " + greenKey + " Gold keys: " + goldKey;
    }

    void PickUpCheck()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Actual time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + " green: " + greenKey + " gold: " + goldKey);
            Debug.Log("Points: " + points);
        }
    }

    public void LessTimeOn()
    {
        musicScript.PitchThis(1.58f);
    }

    public void LessTimeOff()
    {
        musicScript.PitchThis(1f);
    }
}
