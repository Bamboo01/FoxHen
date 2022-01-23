namespace FoxHen {
    internal sealed class Speed: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                other.GetComponent<PlayerStatus>()?.AddStatus(Status.hastened);
            };
        }
    }
}