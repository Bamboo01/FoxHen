using UnityEngine;

namespace FoxHen {
    internal sealed class SampleGameplayInteractable: AbstractGameplayInteractable {
        [SerializeField]
        private BloodParticleSystemControl bloodParticleSystemControl;

        private void OnEnable() {
            triggerDelegate += _ => {
                bloodParticleSystemControl.Emit();
            };
        }
    }
}