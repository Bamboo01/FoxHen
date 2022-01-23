using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace FoxHen {
    internal abstract class AbstractGameplayInteractable: MonoBehaviour {
        internal delegate void TriggerDelegate(Collider2D _);

        internal event TriggerDelegate triggerDelegate;

        protected void Awake() {
            gameplayInteractableAttribs.currLifetime = gameplayInteractableAttribs.maxLifetime;

            _ = gameplayInteractableAttribs.ObserveEveryValueChanged(
                mygameplayInteractableAttribs => mygameplayInteractableAttribs.currLifetime
            )
                .Where(lifetime => lifetime <= 0.0f)
                .Subscribe(_ => {
                    Destroy(gameObject);
                })
                .AddTo(this);

            if(gameplayInteractableAttribs.shldLifetimeDecreaseOverTime) {
                _ = this.UpdateAsObservable()
                    .Subscribe(_ => {
                        gameplayInteractableAttribs.currLifetime -= Time.deltaTime;
                    })
                    .AddTo(this);
            }

            AwakeFunc();
        }

        protected virtual void AwakeFunc() {
        }

        protected void OnTriggerEnter2D(Collider2D other) {
            if((gameplayInteractableAttribs.layerMask.value & (1 << other.gameObject.layer)) != 0) {
                gameplayInteractableAttribs.currLifetime = 0.0f;
                triggerDelegate?.Invoke(other);
            }
        }

        [SerializeField]
        private GameplayInteractableAttribs gameplayInteractableAttribs;
    }
}