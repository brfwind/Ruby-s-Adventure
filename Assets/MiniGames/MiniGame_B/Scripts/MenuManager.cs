using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameB
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject escPanel;
        public Button exitGame;
        public static bool is3DCamera = false;
        public Toggle toggle;

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

        //判断UnLockedLevelIndex是否存在，不存在则初始化
        void Start()
        {
            //PlayerPrefs.DeleteAll();
            //PlayerPrefs.Save();

            if (!PlayerPrefs.HasKey("UnLockedLevelIndex"))
            {
                PlayerPrefs.SetInt("UnLockedLevelIndex", 1);
                PlayerPrefs.Save();
            }

            toggle.isOn = is3DCamera;
        }

        //去往关卡场景方法
        public void GoToLevel(string name)
        {
            TransitionManager.instance.Transition(name);
        }

        public void Set3DCamera(bool on)
        {
            is3DCamera = on;
        }
    }
}