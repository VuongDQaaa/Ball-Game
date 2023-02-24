using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] Text timeDisplay;
    [SerializeField] Text scoreDisplay;
    [SerializeField] Text finalScore;
    public int playerScore;
    public float timeValue = 300;
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Awake()
    {
        Time.timeScale = 0;
        playerScore = 0;
        MakeInstance();
    }
    // Update is called once per frame
    void Update()
    {
        timeValue -= Time.deltaTime;
        if (timeValue <= 0)
        {
            timeValue = 0;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        DisplayTime(timeValue);
        SetScore(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miliseconds = timeToDisplay % 1 * 1000;

        timeDisplay.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
    }

    void SetScore(float time)
    {
        playerScore = Mathf.FloorToInt(time) * 1000 + Mathf.FloorToInt(time % 1 * 1000);
        if (PlayerController.instance.isAlived == false)
        {
            playerScore = 0;
        }
        if (PlayerController.instance.isFinished == true)
        {
            Time.timeScale = 0;
        }
        scoreDisplay.text = playerScore.ToString();
    }

    public void StartButton()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game Play");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void FinishPanel()
    {
        finishPanel.SetActive(true);
        finalScore.text = PlayerController.instance.playerScore.ToString();
    }
}
