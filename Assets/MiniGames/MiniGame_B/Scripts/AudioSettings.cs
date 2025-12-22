using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MiniGameB
{
    public class AudioSetting : MonoBehaviour
    {
        public static AudioSetting instance;
        public AudioMixer mixer;

        //单例
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);

                //初始化mixer参数
                float bgm = PlayerPrefs.GetFloat("BGMVolume", -5f);
                float sfx = PlayerPrefs.GetFloat("SFXVolume", -5f);
                mixer.SetFloat("BGM", bgm);
                mixer.SetFloat("SFX", sfx);
            }
            else
                Destroy(gameObject);
        }

        public void SetBGMVolume(float sliderValue)
        {
            float realVolume;

            if (sliderValue <= -30f)
                realVolume = -80f;  // 突变到最小音量
            else
                realVolume = sliderValue; // 其他情况直接用滑块值

            mixer.SetFloat("BGM", realVolume);
            PlayerPrefs.SetFloat("BGMVolume", sliderValue); // 保存滑块原值
        }

        public void SetSFXVolume(float sliderValue)
        {
            float realVolume;

            if (sliderValue <= -30f)
                realVolume = -80f;
            else
                realVolume = sliderValue;

            mixer.SetFloat("SFX", realVolume);
            PlayerPrefs.SetFloat("SFXVolume", sliderValue);
        }

        //供Esc面板脚本调用值的方法
        public float GetBGMVolume() => PlayerPrefs.GetFloat("BGMVolume", -5f);
        public float GetSFXVolume() => PlayerPrefs.GetFloat("SFXVolume", -5f);
    }
}