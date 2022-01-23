using UnityEngine;

namespace FoxHen {
    internal sealed class SampleGameplayInteractable: AbstractGameplayInteractable {
        [SerializeField]
        private BloodParticleSystemControl bloodParticleSystemControl;

        private void OnEnable() {
            if(bloodParticleSystemControl != null) {
                triggerDelegate += (other) => {
                    bloodParticleSystemControl.Emit();
                    other.gameObject.GetComponent<PlayerStatus>()?.AddStatus(Status.slowed);
                };
            }
        }
    }
}