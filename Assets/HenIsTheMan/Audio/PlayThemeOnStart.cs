using UnityEngine;

namespace FoxHen {
	internal sealed class PlayThemeOnStart: MonoBehaviour { //Noob
		[SerializeField]
		private PlayPauseAudio playPauseAudio;

		private void Start() {
			playPauseAudio.PlayMusic();
			Destroy(gameObject);
		}
	}
}