using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameB
{
    public class EscPanelAudio : MonoBehaviour
    {
        public Slider musicSlider;
        public Slider soundSlider;

        //让每次Esc面板上音量Slider的位置和实际音量匹配
        void Start()
        {
            // 初始化 Slider 显示单例的值
            musicSlider.value = AudioSetting.instance.GetBGMVolume();
            soundSlider.value = AudioSetting.instance.GetSFXVolume();

            musicSlider.onValueChanged.AddListener(AudioSetting.instance.SetBGMVolume);
            soundSlider.onValueChanged.AddListener(AudioSetting.instance.SetSFXVolume);
        }
    }

}

