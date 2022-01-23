using UnityEngine;

namespace FoxHen {
    internal sealed class SpeedUpParticleSystemControl: MonoBehaviour {
        public void TogggleActivity() {
            speedUpParticleSystem.gameObject.SetActive(speedUpParticleSystem.gameObject.activeInHierarchy);
        }

        [SerializeField]
        private ParticleSystem speedUpParticleSystem;
    }
}