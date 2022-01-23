namespace FoxHen {
    internal sealed class MysteryBox: AbstractGameplayInteractable {
        private void OnEnable() {
            triggerDelegate += (other) => {
                _ = other.GetComponent<PlayerInventory>()?.AddRandomItem(other.GetComponent<PlayerController>().isFox);
            };
        }
    }
}