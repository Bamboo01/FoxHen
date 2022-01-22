using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace FoxHen {
    internal abstract class AbstractTrap: MonoBehaviour {
        public virtual void OnTrigger() {
            gameObject.SetActive(false);
        }

        protected virtual void Awake() {
            //onTriggerObservable = Observable.Create<Unit>(observer => {
            //    observer.OnNext();
            //    return default;
            //});
        }

        internal void AddAffectableGameObj(object owner, GameObject gameObj) {
            if(!affectableDict.ContainsKey(owner)) {
                affectableDict.Add(owner, new List<GameObject>());
            }

            affectableDict[owner].Add(gameObj);
        }

        internal void RemoveAffectableGameObj(object owner, GameObject gameObj) {
            if(!affectableDict.ContainsKey(owner)) {
                return;
            }

            _ = affectableDict[owner].Remove(gameObj);
        }

        protected internal IObservable<Unit> onTriggerObservable;

        protected float maxHealth;

        protected float currHealth;

        protected float rangeDmgRange; //Dist rangeDmg will go

        protected float rangeDmg;

        protected float triggerRange; //Range in which the trap will be triggered

        protected float triggerDmg;

        private Dictionary<object, List<GameObject>> affectableDict;
    }
}