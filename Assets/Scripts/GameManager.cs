using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
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
        Debug.Log("Time: " + timeToEnd + " s");
        if(timeToEnd <= 0)
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
    }

    public void ResumeGame()
    {
        PlayClip(resumeClip);
        musicScript.OnResumeGame();
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
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
        CancelInvoke("Stopper");
        if(win)
        {
            PlayClip(winClip);
            Debug.Log("You win!!! Reload?");
        } else
        {
            PlayClip(loseClip);
            Debug.Log("You lose!!! Reload?");
        }
    }

    public void AddPoints(int point)
    {
        points += point;
        Debug.Log("Added points");
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        Debug.Log("Added time");
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
        Debug.Log("Freez time");
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            Debug.Log("Added gold key");
        } else if(color == KeyColor.Green)
        {
            greenKey++;
            Debug.Log("Added green key");
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            Debug.Log("Added red key");
        }
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
