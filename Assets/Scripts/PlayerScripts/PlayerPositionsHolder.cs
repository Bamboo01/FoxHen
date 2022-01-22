using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;
using Bamboo.Events;

namespace FoxHen
{
    public class PlayerPositionsHolder : MonoBehaviour
    {
        public Transform playerSpriteTransform;
        public Transform playerPivotTransform;
        public SpriteRenderer spriteRenderer;

        void Awake()
        {
            playerPivotTransform = transform;
        }

        void Start()
        {
            EventManager.Instance.Publish("PlayerSpawned", this);
        }
    }
}