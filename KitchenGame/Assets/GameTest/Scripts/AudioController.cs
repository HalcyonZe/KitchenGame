using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    protected AudioSource m_as;

    [System.Serializable]
    public struct MyAudio
    {
        public string audioName;
        public AudioClip audioClip;
    }
    public MyAudio[] MyAudioes;

    public Dictionary<string, AudioClip> AudioClipDic;

    void Start()
    {
        m_as = this.GetComponent<AudioSource>();
        AudioClipDic = new Dictionary<string, AudioClip>();
        for (int i = 0; i < MyAudioes.Length; i++)
        {
            AudioClipDic.Add(MyAudioes[i].audioName, MyAudioes[i].audioClip);
        }

    }

    public void SetAudioPlay(string audio)
    {
        if (AudioClipDic.ContainsKey(audio))
        {
            /*m_as.clip = AudioClipDic[audio];
            if (!m_as.isPlaying)
            {
                m_as.Play();
            }*/
            m_as.PlayOneShot(AudioClipDic[audio]);
        }
    }


}
