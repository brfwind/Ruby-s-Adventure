using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        [Header("音频片段")]
        public AudioClip clip;
        [Header("音频分组")]
        public AudioMixerGroup outputGroup;
        [Header("音频初始音量")]
        [Range(0, 1)]
        public float volume;
        [Header("音频是否开局播放")]
        public bool playOnAwake;
        [Header("音频是否循环")]
        public bool loop;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> audioDic;

    private static AudioManager instance;

    private void Awake()
    {
        instance = this;
        audioDic = new Dictionary<string, AudioSource>();
    }

    private void Start()
    {
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if (sound.playOnAwake)
                source.Play();

            audioDic.Add(sound.clip.name, source);
        }
    }

    //isWait是指，需不需要等该音频的上一次播放完成后，再播放
    public static void PlayAudio(string name, bool isWait = false)
    {
        if (!instance.audioDic.ContainsKey(name))  //如果字典里没有该音频片段
        {
            Debug.LogWarning($"名为{ name}的音频片段不存在");
            return;
        }

        if (isWait)
        {
            if (!instance.audioDic[name].isPlaying)
                instance.audioDic[name].Play();
        }
        else
            instance.audioDic[name].Play();
    }

    public static void StopAudio(string name)
    {
        if (!instance.audioDic.ContainsKey(name))  //如果字典里没有该音频片段
        {
            Debug.LogWarning($"名为{ name}的音频片段不存在");
            return;
        }

        instance.audioDic[name].Stop();
    }
}
