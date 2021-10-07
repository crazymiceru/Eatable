using System.Collections.Generic;
using UnityEngine;

namespace Eatable
{
    public class SoundUI : MonoBehaviour
    {
        public static SoundUI inst { get; private set; }
        [SerializeField] private AudioSource startAudioSource;
        private List<AudioSource> audioSources=new List<AudioSource>();

        private void Awake()
        {
            inst = this;
            audioSources.Add(startAudioSource);
        }

        public void Play(AudioClip audioClip)
        {
            bool isHave=false;
            AudioSource currentAudioSource=null;
            foreach (var item in audioSources)
            {
                if (!item.isPlaying)
                {
                    currentAudioSource = item;
                    isHave = true;
                }
            }
            if (!isHave)
            {
                currentAudioSource = Instantiate(audioSources[0].gameObject, gameObject.transform)
                    .GetComponent<AudioSource>();
                audioSources.Add(currentAudioSource);                 
            }
            currentAudioSource.clip = audioClip;
            currentAudioSource.Play();
        }
    }
}