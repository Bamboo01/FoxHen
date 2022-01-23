namespace FoxHen {
    internal sealed class Berry: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.hastened);
            };
        }
    }
}