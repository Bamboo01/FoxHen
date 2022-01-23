using UnityEngine;

namespace FoxHen {
    internal sealed class SlowDownParticleSystemControl: MonoBehaviour {
        public void TogggleActivity() {
            slowDownParticleSystem.gameObject.SetActive(slowDownParticleSystem.gameObject.activeInHierarchy);
        }

        [SerializeField]
        private ParticleSystem slowDownParticleSystem;
    }
}