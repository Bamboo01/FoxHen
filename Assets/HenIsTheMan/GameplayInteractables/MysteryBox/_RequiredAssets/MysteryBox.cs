using UnityEngine;

namespace FoxHen {
    internal sealed class MysteryBox: MonoBehaviour {
        [SerializeField]
        private LayerMask layerMask;

        private void OnTriggerEnter2D(Collider2D other) {
            if((layerMask.value & (1 << other.gameObject.layer)) != 0) {
                other.GetComponent<PlayerInventory>().AddRandomItem();
            }
        }
    }
}