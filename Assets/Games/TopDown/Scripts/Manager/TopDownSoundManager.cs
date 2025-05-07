using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownSoundManager : MonoBehaviour
    {
        public static TopDownSoundManager instance;

        [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
        [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
        [SerializeField][Range(0f, 1f)] private float musicVolume;

        private AudioSource musicAudioSource;
        public AudioClip musicClip;

        public TopDownSoundSource soundSourcePrefab;
        private void Awake()
        {
            instance = this;
            musicAudioSource = GetComponent<AudioSource>();
            musicAudioSource.volume = musicVolume;
            musicAudioSource.loop = true;
        }

        private void Start()
        {
            ChangeBackGroundMusic(musicClip);
        }

        public void ChangeBackGroundMusic(AudioClip clip)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public static void PlayClip(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            if (instance == null)
            {
                return;
            }

            if (instance.soundSourcePrefab == null)
            {
                return;
            }

            TopDownSoundSource obj = Instantiate(instance.soundSourcePrefab);
            TopDownSoundSource soundSource = obj.GetComponent<TopDownSoundSource>();
            soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
        }
    }
}