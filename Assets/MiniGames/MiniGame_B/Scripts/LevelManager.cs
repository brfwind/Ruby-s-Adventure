using System;
using UnityEngine;
using TMPro;

namespace MiniGameB
{
    public class LevelManager : MonoBehaviour
    {
        public Player player;
        public LevelData levelData;
        public UIManagerB ui;
        private AudioManager music;
        private int totalFoods;
        private int score = 0;
        private float timer = 0f;
        private bool started = false;
        private bool finished = false;

        public event Action OnWinGame;

        //获取食物数量
        //获取关卡名称
        //订阅事件
        void Start()
        {
            totalFoods = GameObject.FindGameObjectsWithTag("Food").Length;
            music = AudioManager.instance;
            ui.SetLevelText(levelData.levelName);

            // 订阅 Player 的事件
            if (player != null)
                player.OnFoodCollected += CollectFood;
        }

        //计时逻辑(优化时间状态检测)
        string lastState = "";
        void Update()
        {
            if (!started && player.move)
                started = true;

            if (started && !finished)
            {
                timer += Time.deltaTime;
                ui.SetTimeText(timer);
            }

            string currentState;

            if (timer < levelData.timeLimit)
                currentState = "excellent";
            else if (timer < levelData.midTime)
                currentState = "mid";
            else
                currentState = "slow";

            if (currentState != lastState)
            {
                switch (currentState)
                {
                    case "excellent":
                        ui.SetTextColor(Color.green);
                        ui.SetWinTextAndColor("EXCELLENT!", Color.green);
                        music.overGame.clip = music.winGameClip;
                        break;

                    case "mid":
                        ui.SetTextColor(new Color(1f, 0.5f, 0f));
                        ui.SetWinTextAndColor("GOOD ENOUGH!", new Color(1f, 0.5f, 0f));
                        music.overGame.clip = music.midGameClip;
                        break;

                    case "slow":
                        ui.SetTextColor(Color.red);
                        ui.SetWinTextAndColor("TOO SLOW!", Color.red);
                        music.overGame.clip = music.badGameClip;
                        break;
                }

                lastState = currentState;
            }
        }


        //事件回调方法
        void CollectFood()
        {
            score++;

            if (score >= totalFoods)
            {
                Win();
            }
        }

        //关卡结束后续逻辑
        void Win()
        {
            finished = true;
            started = false;

            // 禁止玩家移动，增大摩擦让球停下
            if (player != null)
            {
                player.canControl = false;
                player.rb.drag = 5.5f;
            }

            // 更新最佳时间
            levelData.bestTime = Mathf.Min(timer, levelData.bestTime);

            //更新最佳时间
            if (!PlayerPrefs.HasKey(levelData.levelIndex + "BestTime"))
            {
                PlayerPrefs.SetFloat(levelData.levelIndex + "BestTime", timer);
            }
            else if (timer < PlayerPrefs.GetFloat(levelData.levelIndex + "BestTime"))
            {
                PlayerPrefs.SetFloat(levelData.levelIndex + "BestTime", timer);
            }

            //触发事件
            if (timer <= levelData.timeLimit)
            {
                OnWinGame?.Invoke();
            }

            //更新已解锁关卡
            if (levelData.levelIndex >= PlayerPrefs.GetInt("UnLockedLevelIndex") && timer <= levelData.midTime)
            {
                PlayerPrefs.SetInt("UnLockedLevelIndex", levelData.levelIndex + 1);
                Debug.Log("解锁了");
                PlayerPrefs.Save();
            }

            //播放over音乐
            music.PlayOverGame();

            // 显示胜利面板
            ui.ShowOverPanel();
        }
    }

}

