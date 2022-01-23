using System.Collections;
using UnityEngine;

namespace FoxHen {
    internal sealed class PlayRandSoundsAtRandIntervals: MonoBehaviour {
        [SerializeField]
        private PlayPauseAudio[] playPauseAudioArr;

        [SerializeField]
        private float minDelayInSec;

        [SerializeField]
        private float maxDelayInSec;

        private void Start() {
            DontDestroyOnLoad(gameObject);

            _ = StartCoroutine(nameof(MyCoroutine));
        }

        private IEnumerator MyCoroutine() {
            while(true) {
                yield return new WaitForSeconds(UnityEngine.Random.Range(minDelayInSec, maxDelayInSec));
                playPauseAudioArr[UnityEngine.Random.Range(0, playPauseAudioArr.Length)].PlaySound();
            }
        }
    }
}