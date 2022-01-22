using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace FoxHen {
    internal abstract class AbstractTrap: MonoBehaviour {
        internal delegate void TriggerDelegate();

        internal event TriggerDelegate triggerDelegate;

        protected void Awake() {
            trapAttribs.currLifetime = trapAttribs.maxLifetime;

            _ = trapAttribs.ObserveEveryValueChanged(myTrapAttribs => myTrapAttribs.currLifetime)
                .Where(lifetime => lifetime <= 0.0f)
                .Subscribe(_ => {
                    gameObject.SetActive(false);
                });

            if(trapAttribs.shldLifetimeDecreaseOverTime) {
                _ = this.UpdateAsObservable()
                    .Subscribe(_ => {
                        trapAttribs.currLifetime -= Time.deltaTime;
                    });
            }

            AwakeFunc();
        }

        protected virtual void AwakeFunc() {
        }

        protected void OnTriggerEnter2D(Collider2D other) {
            if((trapAttribs.layerMask.value & (1 << other.gameObject.layer)) != 0) {
                trapAttribs.currLifetime = 0.0f;
                triggerDelegate?.Invoke();
            }
        }

        [SerializeField]
        private TrapAttribs trapAttribs;
    }
}