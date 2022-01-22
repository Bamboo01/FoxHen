using UnityEngine;

namespace FoxHen {
    internal abstract class AbstractTrap: MonoBehaviour {
        public virtual void OnTrigger() {
            gameObject.SetActive(false);
        }

        protected float maxHealth;

        protected float currHealth;

        protected float range;

        protected float rangeDmg;

        protected float triggerDmg;
    }
}