using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {

    [System.Flags]
    public enum Status : int {
        none = 0,
        slowed = 1 << 0,
        stunned = 1 << 1,
        hastened = 1 << 2,
        total = ~0
    }

    public class PlayerAttributes : MonoBehaviour
    {
        public float moveSpeed;
        public bool charType;
        public Status status;

        private void Start()
        {
            status = Status.slowed;
            status |= Status.hastened;
            if ( (status & Status.slowed) == Status.slowed )
            {
                moveSpeed *= 0.5f;
            };
        }
    }
}