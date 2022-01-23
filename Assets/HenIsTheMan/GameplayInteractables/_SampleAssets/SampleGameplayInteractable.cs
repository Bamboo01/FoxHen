using UnityEngine;

namespace FoxHen {
    internal sealed class SampleGameplayInteractable: AbstractGameplayInteractable {
        [SerializeField]
        private BloodParticleSystemControl bloodParticleSystemControl;

        private void OnEnable() {
            if(bloodParticleSystemControl != null) {
                triggerDelegate += _ => {
                    bloodParticleSystemControl.Emit();
                };
            }
        }
    }
}