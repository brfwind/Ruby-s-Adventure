using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiniGameB
{
    public class MiniGameExit : MonoBehaviour
    {
        public void ExitToMainGame()
        {
            // 恢复时间
            Time.timeScale = 1f;

            // 销毁 MiniGameB 的常驻系统
            if (AudioManager.instance != null)
                Destroy(AudioManager.instance.gameObject);

            if (AudioSetting.instance != null)
                Destroy(AudioSetting.instance.gameObject);

            // 切回主游戏
            SceneManager.LoadScene("MainScene");
        }
    }
}