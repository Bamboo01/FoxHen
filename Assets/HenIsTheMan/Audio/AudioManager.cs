using Bamboo.Utility;
using Genesis.Wisdom;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {
	internal sealed class AudioManager: Singleton<AudioManager> {
		#region Fields

		[SerializeField]
		private bool shldSetDefaultVols;

		[ShowHideInInspector(true, nameof(shldSetDefaultVols), true)]
		[SerializeField]
		private float defaultMusicVol;

		[ShowHideInInspector(true, nameof(shldSetDefaultVols), true)]
		[SerializeField]
		private float defaultSoundVol;

		[SerializeField]
		private AudioClip[] musicAudioClips;

		[SerializeField]
		private AudioClip[] soundAudioClips;

		private Dictionary<string, AudioSource> music;
		private Dictionary<string, AudioSource> sounds;

		private float musicVol;
		private float soundVol;

		#endregion

		#region Properties

		internal float MusicVol {
			get => musicVol;
			private set {
				musicVol = value;
				PlayerPrefs.SetFloat("MusicVol", musicVol);
			}
		}

		internal float SoundVol {
			get => soundVol;
			private set {
				soundVol = value;
				PlayerPrefs.SetFloat("SoundVol", soundVol);
			}
		}

		#endregion

		#region Ctors and Dtor

		internal AudioManager(): base() {
			music = null;
			sounds = null;

			musicVol = 0.0f;
			soundVol = 0.0f;

			musicAudioClips = System.Array.Empty<AudioClip>();
			soundAudioClips = System.Array.Empty<AudioClip>();
		}

		#endregion

		#region Unity User Callback Event Funcs

		protected override void OnAwake() {
			if(shldSetDefaultVols) {
				musicVol = defaultMusicVol;
				soundVol = defaultSoundVol;

				PlayerPrefs.SetFloat("MusicVol", musicVol);
				PlayerPrefs.SetFloat("SoundVol", soundVol);
			} else {
				musicVol = PlayerPrefs.GetFloat("MusicVol", 0.0f);
				soundVol = PlayerPrefs.GetFloat("SoundVol", 0.0f);
			}

			AudioSource audioSrc;
			music = new Dictionary<string, AudioSource>();
			sounds = new Dictionary<string, AudioSource>();

			foreach(AudioClip audioClip in musicAudioClips) {
				audioSrc = gameObject.AddComponent<AudioSource>();
				audioSrc.clip = audioClip;

				audioSrc.mute = false;
				audioSrc.playOnAwake = false;
				audioSrc.loop = true;

				music.Add(audioClip.name, audioSrc);
			}

			foreach(AudioClip audioClip in soundAudioClips) {
				audioSrc = gameObject.AddComponent<AudioSource>();
				audioSrc.clip = audioClip;

				audioSrc.mute = false;
				audioSrc.playOnAwake = false;
				audioSrc.loop = false;

				sounds.Add(audioClip.name, audioSrc);
			}
		}

		#endregion

		internal void AdjustVolOfAllMusic(float vol) {
			MusicVol = vol;

			foreach(KeyValuePair<string, AudioSource> pair in music) {
				pair.Value.volume = musicVol;
			}
		}

		internal void AdjustVolOfAllSounds(float vol) {
			SoundVol = vol;

			foreach(KeyValuePair<string, AudioSource> pair in sounds) {
				pair.Value.volume = soundVol;
			}
		}

		internal void PauseMusic(string name) {
			music[name].Pause();
		}

		internal void PauseSound(string name) {
			sounds[name].Pause();
		}

		internal void PauseAll() {
			PauseAllMusic();
			PauseAllSounds();
		}

		internal void PauseAllMusic() {
			foreach(KeyValuePair<string, AudioSource> pair in music) {
				pair.Value.Pause();
			}
		}

		internal void PauseAllSounds() {
			foreach(KeyValuePair<string, AudioSource> pair in sounds) {
				pair.Value.Pause();
			}
		}

		internal void PlayMusic(string name) {
			music[name].volume = musicVol;
			music[name].Play();
		}

		internal void PlaySound(string name) {
			sounds[name].volume = soundVol;
			sounds[name].Play();
		}

		internal void PlayAll() {
			PlayAllMusic();
			PlayAllSounds();
		}

		internal void PlayAllMusic() {
			foreach(KeyValuePair<string, AudioSource> pair in music) {
				pair.Value.volume = musicVol;
				pair.Value.Play();
			}
		}

		internal void PlayAllSounds() {
			foreach(KeyValuePair<string, AudioSource> pair in sounds) {
				pair.Value.volume = soundVol;
				pair.Value.Play();
			}
		}
	}
}