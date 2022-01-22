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

        [SerializeField]
        private TrapAttribs trapAttribs;

        private Dictionary<object, List<GameObject>> affectableDict;
    }
}