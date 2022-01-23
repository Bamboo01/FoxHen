using UniRx;
using UnityEngine;

namespace FoxHen {
    internal sealed class PlaySoundAtRegularIntervals: MonoBehaviour {
        [SerializeField]
        private PlayPauseAudio playPauseAudio;

        private void Start() {
            _ = Observable.EveryUpdate()
                .Subscribe(_ => {
                    playPauseAudio.PlaySound();
                });
        }
    }
}