namespace FoxHen {
    internal sealed class Shield: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.invulnerable);
            };
        }
    }
}