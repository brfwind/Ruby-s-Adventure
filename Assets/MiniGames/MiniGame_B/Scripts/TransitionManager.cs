using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameB
{
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager instance;

        [Header("渐入渐出")]
        //给黑色Panel添加CanvasGroup组件，便于方便调整Alpha值
        private CanvasGroup canvasGroup;
        public float speed;
        [Header("渐弹渐闭")]
        public AnimationCurve showCurve;
        public AnimationCurve hideCurve;
        public float curveSpeed;

        //单例模式
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            DontDestroyOnLoad(this.gameObject);
        }

        #region 渐入渐出
        //进游戏，渐变白
        void Start()
        {
            StartCoroutine(Fade(0));
        }

        //供外部调用的方法
        public void Transition(string sceneName)
        {
            StartCoroutine(TransitionToScene(sceneName));
        }

        private IEnumerator TransitionToScene(string sceneName)
        {
            yield return Fade(1);
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Fade(0);
        }

        private IEnumerator Fade(int goal)
        {
            //拦截输入
            canvasGroup.blocksRaycasts = true;

            //实现渐变
            while (Mathf.Abs(canvasGroup.alpha - goal) > 0.01f)
            {
                switch (goal)
                {
                    case 1:
                        canvasGroup.alpha += Time.deltaTime * speed;
                        break;
                    case 0:
                        canvasGroup.alpha -= Time.deltaTime * speed;
                        break;
                }

                yield return null;
            }

            canvasGroup.blocksRaycasts = false;
        }
        #endregion

        #region 渐弹渐闭

        //显示面板
        public void ShowPanel(GameObject panel)
        {
            Time.timeScale = 0;  //立刻暂停游戏
            panel.SetActive(true); //确保面板激活
            StartCoroutine(TransitionPanel(panel, 1));
        }

        //关闭面板
        public IEnumerator ClosePanel(GameObject panel)
        {
            //先等待面板关闭动画结束，再恢复时间流逝
            yield return StartCoroutine(TransitionPanel(panel, 0));
            Time.timeScale = 1;
        }

        //1显示，0关闭
        private IEnumerator TransitionPanel(GameObject panel, int amount)
        {
            float timer = 0f;

            AnimationCurve curve = null;

            //注意：渐弹要保证弹前，面板是Active状态
            //      渐闭要保证闭后，面板是关闭状态
            if (amount == 1)
            {
                panel.SetActive(true);
                curve = showCurve;
            }
            else if (amount == 0)
            {
                curve = hideCurve;
            }

            while (timer <= 1f)
            {
                float scale = curve.Evaluate(timer);
                panel.transform.localScale = Vector3.one * scale;

                //这里用unscaledDeltaTime代替DeltaTime
                //前者不受游戏中timeScale的影响
                //就算游戏暂停，UI仍会继续播放完
                timer += Time.unscaledDeltaTime * curveSpeed;
                yield return null;
            }

            if (amount == 0)
            {
                panel.SetActive(false);
            }
        }
        #endregion
    }
}