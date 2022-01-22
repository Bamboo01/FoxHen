using System;
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

        [SerializeField]
        private float spawnIntervalInSec;

        private void Awake() {
            if(playerTransform != null) {
                _ = playerTransform.ObserveEveryValueChanged(myTransform => myTransform.position.x)
                    .Skip(1)
                    .Buffer(TimeSpan.FromSeconds(spawnIntervalInSec))
                    .Where(myList => myList.Count > 0)
                    .Subscribe(_ => {
                        lightningParticleSystem.Emit(particleCount);
                    })
                    .AddTo(this);
            }
        }
    }
}