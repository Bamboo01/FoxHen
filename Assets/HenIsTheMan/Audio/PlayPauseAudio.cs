using Genesis.Wisdom;
using UnityEngine;

namespace FoxHen {
	[CreateAssetMenu(
		fileName = nameof(PlayPauseAudio),
		menuName = StrHelper.scriptableObjsFolderPath + nameof(PlayPauseAudio)
	)]
	internal sealed class PlayPauseAudio: ScriptableObject { //Noob
		[SerializeField]
		private string myName;

		public void PlayMusic() {
			AudioManager.Instance.PlayMusic(myName);
		}

		public void PlaySound() {
			AudioManager.Instance.PlaySound(myName);
		}
	}
}