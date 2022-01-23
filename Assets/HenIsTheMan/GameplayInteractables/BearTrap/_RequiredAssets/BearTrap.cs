namespace FoxHen {
    internal sealed class BearTrap: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.stunned);

                BloodParticleSystemControl bloodParticleSystemControl = other.GetComponentInChildren<BloodParticleSystemControl>();
                if(bloodParticleSystemControl != null) {
                    bloodParticleSystemControl.Emit();
                }
            };
        }
    }
}