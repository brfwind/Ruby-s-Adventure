using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Button startButton;
    public Button overButton;
    public GameObject StartPanel;
    public GameObject PlayPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public static bool isStart = false;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        startButton.onClick.AddListener(StartGame); 
        overButton.onClick.AddListener(OverGame);
        bestScoreText.text = "最高分：" + PlayerPrefs.GetInt("BestScore");
    }

    void Update()
    {
        if(isStart)
        {
            StartPanel.gameObject.SetActive(false);
            PlayPanel.gameObject.SetActive(true);
            scoreText.text = "分数：" + BirdController.score;
        }
        if(BirdController.isDead)
        {
            overButton.gameObject.SetActive(true);
        }
    }

    private void StartGame()
    {
        isStart = true;
    }

    private void OverGame()
    {
        UIManager.isStart = false;
        BirdController.isDead = false;

        if(PlayerPrefs.HasKey("BestScore"))
        {
            int lastBest = PlayerPrefs.GetInt("BestScore");
            int nowBest = Mathf.Max(lastBest,BirdController.score);
            PlayerPrefs.SetInt("BestScore",nowBest);
        }
        else
        {
            PlayerPrefs.SetInt("BestScore",BirdController.score);
        }

        BirdController.score = 0;
        SceneManager.LoadScene("Game");
    }

    public void ReturnMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
