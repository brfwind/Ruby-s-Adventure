using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace MiniGameB
{
    public class UIManagerB : MonoBehaviour
    {
        public static UIManagerB instance;

        [Header("GameOverPanel")]
        public Button reTry;
        public Button menu;
        public Button nextLevel;

        [Header("EscPanel")]
        public Button continueB;
        public Button menu1;
        public Button exit;

        [Header("PlayPanel")]
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI winText;
        public TextMeshProUGUI levelName;

        [Header("Panels")]
        public GameObject overPanel;
        public GameObject escPanel;

        //单例
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        #region 按钮功能实现
        //Esc按压检测
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (escPanel.activeInHierarchy)
                {
                    StartCoroutine(TransitionManager.instance.ClosePanel(escPanel));
                }
                else
                {
                    TransitionManager.instance.ShowPanel(escPanel);
                }
            }
        }

        //重玩按钮
        public void RestartGame()
        {
            if (escPanel.activeInHierarchy == true)
            {
                escPanel.SetActive(false);
                Time.timeScale = 1;
            }

            TransitionManager.instance.Transition(SceneManager.GetActiveScene().name);
        }

        //返回选关按钮
        public void BackToMenu()
        {
            Time.timeScale = 1;
            TransitionManager.instance.Transition("Menu");
        }

        //退出游戏按钮
        public void ExitGame()
        {
            Debug.Log("回到主场景");
            SceneManager.LoadScene("MainScene");
        }

        //下一关按钮
        public void LoadNextLevel()
        {
            //获取当前场景(关卡)名
            string cur = SceneManager.GetActiveScene().name;

            // 正则表达式提取关卡数字（假设一定有数字）
            Match match = Regex.Match(cur, @"\d+$");
            int level = int.Parse(match.Value);

            if (++level <= PlayerPrefs.GetInt("UnLockedLevelIndex"))
            {
                nextLevel.interactable = true;
                string nextLevelName = "Level_" + level;
                TransitionManager.instance.Transition(nextLevelName);
            }
            else
            {
                nextLevel.interactable = false;
            }
        }
        #endregion

        public void SetTextColor(Color color)
        {
            timeText.color = color;
        }

        public void SetTimeText(float timer)
        {
            timeText.text = "Time: " + timer.ToString("F2");
        }

        public void ShowOverPanel()
        {
            overPanel.SetActive(true);
        }

        public void SetLevelText(string name)
        {
            levelName.text = name;
        }

        public void SetWinTextAndColor(string content, Color color)
        {
            winText.text = content;
            winText.color = color;
        }
    }
}

