namespace FoxHen {
    internal sealed class Mushroom: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.confused);

                BloodParticleSystemControl bloodParticleSystemControl = other.GetComponentInChildren<BloodParticleSystemControl>();
                if(bloodParticleSystemControl != null) {
                    bloodParticleSystemControl.Emit();
                }
            };
        }
    }
}