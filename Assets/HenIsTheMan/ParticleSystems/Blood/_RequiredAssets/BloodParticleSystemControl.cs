using UnityEngine;

namespace FoxHen {
    internal sealed class BloodParticleSystemControl: MonoBehaviour {
        public void Emit() {
            bloodParticleSystem.Emit(particleCount);
        }

        [SerializeField]
        private ParticleSystem bloodParticleSystem;

        [SerializeField]
        private int particleCount;

        private ParticleSystem.EmitParams emitParams;

        private void Awake() {
            emitParams = new ParticleSystem.EmitParams();
        }
    }
}