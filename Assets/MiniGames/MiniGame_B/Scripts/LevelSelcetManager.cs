using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameB
{
    public class LevelSelcetManager : MonoBehaviour
    {
        public GameObject[] levelObjects;
        public LevelData[] levelDatas;

        //更新关卡状态
        void Start()
        {
            int unlockedIndex = PlayerPrefs.GetInt("UnLockedLevelIndex");

            for (int i = 0; i < levelObjects.Length; i++)
            {
                // 获取子组件
                Button btn = levelObjects[i].GetComponentInChildren<Button>();
                TextMeshProUGUI goalText = levelObjects[i].transform.Find("GoalTime").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI bestText = levelObjects[i].transform.Find("BestTime").GetComponent<TextMeshProUGUI>();

                // 设置解锁状态
                if (i < unlockedIndex)
                {
                    btn.interactable = true;
                    goalText.gameObject.SetActive(true);
                    bestText.gameObject.SetActive(true);
                }
                else
                {
                    btn.interactable = false;
                    goalText.gameObject.SetActive(false);
                    bestText.gameObject.SetActive(false);
                }

                float bestTime = PlayerPrefs.GetFloat(levelDatas[i].levelIndex + "BestTime");

                //根据成绩，修改字体颜色
                if (bestTime == 0)
                {
                    bestText.color = Color.white;
                }
                else if (bestTime < levelDatas[i].timeLimit)
                {
                    bestText.color = Color.green;
                }
                else if (bestTime < levelDatas[i].midTime)
                {
                    bestText.color = new Color(1f, 0.5f, 0f);
                }
                else
                {
                    bestText.color = Color.red;
                }

                // 显示目标和最好成绩
                goalText.text = "Challenge:" + levelDatas[i].timeLimit.ToString() + "s";
                bestText.text = "BestTime:" + PlayerPrefs.GetFloat(levelDatas[i].levelIndex + "BestTime", 0).ToString("F2") + "s";

                if (PlayerPrefs.GetFloat(levelDatas[i].levelIndex + "BestTime", 0) < levelDatas[i].timeLimit && PlayerPrefs.GetFloat(levelDatas[i].levelIndex + "BestTime", 0) != 0)
                {
                    goalText.color = Color.green;
                }
            }
        }
    }
}