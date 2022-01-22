using UniRx;
using UnityEngine;

namespace FoxHen {
    internal sealed class LightningParticleSystemControl: MonoBehaviour {
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private ParticleSystem lightningParticleSystem;

        [SerializeField]
        private int particleCount;

        private void Awake() {
            if(playerTransform != null) {
                _ = playerTransform.ObserveEveryValueChanged(myTransform => myTransform.position.x)
                    .Skip(1)
                    .Buffer(1)
                    .Subscribe(_ => {
                        lightningParticleSystem.Emit(particleCount);
                    });
            }
        }
    }
}