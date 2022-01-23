using UnityEngine;

namespace FoxHen {
    internal sealed class MysteryBox: AbstractGameplayInteractable {
        [SerializeField]
        private LayerMask layerMask;

        private void OnEnable() {
            triggerDelegate += (other) => {
                if((layerMask.value & (1 << other.gameObject.layer)) != 0) {
                    other.GetComponent<PlayerInventory>().AddRandomItem();
                }
            };
        }
    }
}