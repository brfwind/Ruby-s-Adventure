using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MiniGameB
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Player player;

        [Header("Audio Clips")]
        public AudioClip bgmClip;
        public AudioClip foodClip;
        public AudioClip winGameClip;
        public AudioClip midGameClip;
        public AudioClip badGameClip;

        [Header("AudioBehaviour Source")]
        public AudioSource bgmMusic;
        public AudioSource overGame;
        public AudioSource food;

        //单例
        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            DontDestroyOnLoad(this);
        }

        public void PlayOverGame()
        {
            overGame.Play();
        }

        public void PlayFood()
        {
            food.PlayOneShot(foodClip);
        }
    }

}

