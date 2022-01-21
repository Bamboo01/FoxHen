using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Bamboo.Utility;

namespace Bamboo.Audio
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioclip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(.1f, 10f)]
        public float pitch = 1f;
        public bool loop = false;
        public bool usesFadeMixer = true;

        [HideInInspector]
        public AudioSource source;
    }


    public class SoundManager : Singleton<SoundManager>
    {
        [Header("2D Audio")]
        [SerializeField] List<Sound> soundClips = new List<Sound>();
        [SerializeField] AudioMixerGroup fadeMixer;
        [SerializeField] AudioMixerGroup masterMixer;
        [Header("3D Sound")]
        [SerializeField] bool canPlay3DAudio;
        [SerializeField] int pointAudioPoolSize;
        [SerializeField] GameObject pointAudioPrefab;
        [Header("Volume Control")]
        [SerializeField] [Range(0.0f, 1.0f)] private float  MasterVolume = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float  FadeMixerVolume = 1.0f;

        private Dictionary<int, Sound> IDToSoundClip = new Dictionary<int, Sound>();
        private Dictionary<string, int> NameToID = new Dictionary<string, int>();
        private Queue<GameObject> pointAudioPool = new Queue<GameObject>();

        public bool has3DAudio => canPlay3DAudio;

        #region point audio pooling
        public GameObject spawnFromPool(int count = 0)
        {
            if (count == pointAudioPool.Count)
            {
                GameObject obj = Instantiate(pointAudioPrefab);
                obj.SetActive(true);
                obj.transform.SetParent(transform);
                pointAudioPool.Enqueue(obj);
                return obj;
            }

            GameObject poolobject = pointAudioPool.Dequeue();
            if (poolobject.activeSelf)
            {
                pointAudioPool.Enqueue(poolobject);
                return spawnFromPool(++count);
            }
            else
            {
                poolobject.SetActive(true);
                pointAudioPool.Enqueue(poolobject);
                return poolobject;
            }
        }
        #endregion

        #region core functions
        protected override void OnAwake()
        {
            int ID = 0;
            foreach (Sound clip in soundClips)
            {
                clip.source = gameObject.AddComponent<AudioSource>();
                clip.source.clip = clip.audioclip;
                clip.source.volume = clip.volume;
                clip.source.pitch = clip.pitch;
                clip.source.loop = clip.loop;

                if (clip.usesFadeMixer)
                {
                    clip.source.outputAudioMixerGroup = fadeMixer;
                }

                NameToID.Add(clip.name, ID);
                IDToSoundClip.Add(ID, clip);
                ID++;
            }

            if (canPlay3DAudio)
            {
                for (int i = 0; i < pointAudioPoolSize; i++)
                {
                    GameObject obj = Instantiate(pointAudioPrefab);
                    obj.SetActive(false);
                    pointAudioPool.Enqueue(obj);
                    obj.transform.SetParent(this.gameObject.transform);
                }
            }
        }

        public void PlaySound(string Name)
        {
            int ID;
            if (NameToID.TryGetValue(Name, out ID))
            {
                Sound clip = IDToSoundClip[ID];
                if (!clip.source.isPlaying)
                {
                    clip.source.volume = clip.volume;
                    clip.source.Play();
                }
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }

        public void ForcePlaySound(string Name)
        {
            int ID;
            if (NameToID.TryGetValue(Name, out ID))
            {
                Sound clip = IDToSoundClip[ID];
                clip.source.volume = clip.volume;
                clip.source.Play();
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }

        public void StopSound(string Name)
        {
            int ID;
            if (NameToID.TryGetValue(Name, out ID))
            {
                IDToSoundClip[ID].source.Stop();
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }

        public void StopAllSounds()
        {
            foreach (Sound clip in soundClips)
            {
                clip.source.Stop();
            }
        }

        public void PauseSound(string Name)
        {
            int ID;
            if (NameToID.TryGetValue(Name, out ID))
            {
                IDToSoundClip[ID].source.Pause();
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }

        public void PauseAllSounds()
        {
            foreach (Sound clip in soundClips)
            {
                clip.source.Pause();
            }
        }

        public int GetSoundID(string name)
        {
            return NameToID[name];
        }

        public void UpdateSoundPitch(string Name, float pitch)
        {
            Sound clip;
            if (IDToSoundClip.TryGetValue(GetSoundID(Name), out clip))
            {
                clip.pitch = Mathf.Clamp(pitch, 0.1f, 10.0f);
                clip.source.pitch = Mathf.Clamp(pitch, 0.1f, 10.0f);
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }

        public void UpdateSoundVolume(string Name, float Volume)
        {
            Sound clip;
            if (IDToSoundClip.TryGetValue(GetSoundID(Name), out clip))
            {
                clip.volume = Mathf.Clamp(Volume, 0f, 1.0f);
                clip.source.volume = Mathf.Clamp(Volume, 0f, 1.0f);
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
            }
        }
        #endregion

        public void ChangeMasterVolume(float newValue)
        {
            MasterVolume = newValue;
            masterMixer.audioMixer.SetFloat("Master", MasterVolume);
        }

        public void ChangeFadeMasterVolume(float newValue)
        {
            FadeMixerVolume = newValue;
        }

        public AudioSource PlaySoundAtPoint(string Name, Vector3 position, bool oneshot = true, float spatialblend = 1.0f)
        {
            int ID;
            if (NameToID.TryGetValue(Name, out ID))
            {
                // Get the clip
                Sound clip = IDToSoundClip[ID];
                GameObject pointAudio = spawnFromPool();
                AudioSource audioSource = pointAudio.GetComponent<AudioSource>();
                audioSource.clip = clip.audioclip;
                audioSource.volume = clip.volume;
                audioSource.pitch = clip.pitch;
                audioSource.loop = !oneshot;
                audioSource.spatialBlend = spatialblend;
                audioSource.outputAudioMixerGroup = masterMixer;
                audioSource.Play();

                if (oneshot)
                {
                    audioSource.gameObject.SetActiveDelayed(false, audioSource.clip.length);
                }

                pointAudio.transform.position = position;
                return audioSource;
            }
            else
            {
                Debug.LogError("Failed to get sound with name " + Name);
                return null;
            }
        }

        public AudioSource GetPointSound(Vector3 position)
        {
            GameObject pointAudio = spawnFromPool();
            AudioSource audioSource = pointAudio.GetComponent<AudioSource>();
            pointAudio.transform.position = position;
            return audioSource;
        }
        
        public Coroutine FadeInSound(string name, float duration)
        {
            Sound sound;
            if (!IDToSoundClip.TryGetValue(GetSoundID(name), out sound))
            {
                return null;
            }
            if (sound.source.isPlaying)
            {
                return null;
            }
            return StartCoroutine(PlayFadeSound(name, duration));
        }

        public Coroutine FadeOutSound(string name, float duration)
        {
            Sound sound;
            if (!IDToSoundClip.TryGetValue(GetSoundID(name), out sound))
            {
                return null;
            }
            if (!sound.source.isPlaying)
            {
                return null;
            }
            return StartCoroutine(StopFadeSound(name, duration));
        }

        public void ForceResetFades()
        {
            StopAllCoroutines();
            StopAllSounds();
            FadeMixerGroup.ForceResetFades(fadeMixer.audioMixer, "FadeMaster");
        }

        IEnumerator PlayFadeSound(string name, float duration)
        {
            if (IDToSoundClip[NameToID[name]].usesFadeMixer)
            {
                PlaySound(name);
                yield return StartCoroutine(FadeMixerGroup.StartFade(fadeMixer.audioMixer, "FadeMaster", duration, FadeMixerVolume));
            }
            else
            {
                Debug.LogWarning("Tried to fade a sound that doesn't use the 2D Mixer!");
            }
            yield break;
        }

        IEnumerator StopFadeSound(string name, float duration)
        {
            if (IDToSoundClip[NameToID[name]].usesFadeMixer)
            {
                yield return StartCoroutine(FadeMixerGroup.StartFade(fadeMixer.audioMixer, "FadeMaster", duration, 0.0f));
                StopSound(name);
            }
            else
            {
                Debug.LogWarning("Tried to fade a sound that doesn't use the 2D Mixer!");
            }
            yield break;
        }
    }
}