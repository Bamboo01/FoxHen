namespace FoxHen {
    internal sealed class GlueTrap: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.slowed);

                BloodParticleSystemControl bloodParticleSystemControl = other.GetComponentInChildren<BloodParticleSystemControl>();
                if(bloodParticleSystemControl != null) {
                    bloodParticleSystemControl.Emit();
                }
            };
        }
    }
}