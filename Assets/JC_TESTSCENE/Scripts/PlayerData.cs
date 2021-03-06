using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {
    public class PlayerData : MonoBehaviour
    {
        public float moveSpeed
        {
            get
            {
                return GetMoveSpeed();
            }
        }
        public float defaultMoveSpeed = 20.0f;

        public bool isStunned { get; set; }
        public bool isInvulnerable { get; set; }

        public Vector2 moveInputValue;
        public Vector2 lastMoveDirection;

        public ItemType currItem;

        public delegate float EventStack(float f);
        public List<EventStack> eventStack = new List<EventStack>();

        private void Start()
        {
            isStunned = false;
            isInvulnerable = false;
            currItem = ItemType.none;
            moveInputValue = Vector2.zero;
        }

        public float GetMoveSpeed()
        {
            float _moveSpeed = defaultMoveSpeed;

            foreach (var _event in eventStack)
            {
                _moveSpeed = (float)_event?.Invoke(_moveSpeed);

            }

            return _moveSpeed;
        }
    }
}
