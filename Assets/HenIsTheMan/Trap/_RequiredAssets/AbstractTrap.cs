using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace FoxHen {
    internal abstract class AbstractTrap: MonoBehaviour {
        internal delegate void TriggerDelegate();

        internal event TriggerDelegate triggerDelegate;

        public virtual void OnTrigger() {
            triggerDelegate?.Invoke();
        }

        protected void Awake() {
            _ = trapAttribs.ObserveEveryValueChanged(myTrapAttribs => myTrapAttribs.lifetime)
                .Where(lifetime => lifetime <= 0.0f)
                .Subscribe(_ => {
                    gameObject.SetActive(false);
                });

            if(trapAttribs.shldUseLifetime) {
                _ = this.UpdateAsObservable()
                    .Subscribe(_ => {
                        trapAttribs.lifetime -= Time.deltaTime;
                    });
            }

            AwakeFunc();
        }

        protected abstract void AwakeFunc();

        private void OnCollisionEnter(Collision collision) {
            if((collision.gameObject.layer & trapAttribs.layerMask) != 0) {
                trapAttribs.lifetime = 0.0f;
            }
        }

        [SerializeField]
        private TrapAttribs trapAttribs;
    }
}