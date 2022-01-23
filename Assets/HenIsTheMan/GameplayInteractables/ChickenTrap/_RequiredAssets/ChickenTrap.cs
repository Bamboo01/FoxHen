namespace FoxHen {
    internal sealed class ChickenTrap: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.stunned);
            };
        }
    }
}