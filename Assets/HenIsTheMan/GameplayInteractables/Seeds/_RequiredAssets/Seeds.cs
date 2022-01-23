namespace FoxHen {
    internal sealed class Seeds: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.hastened);
            };
        }
    }
}