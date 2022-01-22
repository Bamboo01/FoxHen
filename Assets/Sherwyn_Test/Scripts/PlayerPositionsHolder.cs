using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionsHolder : MonoBehaviour
{
    public Transform playerSpriteTransform;
    public Transform playerPivotTransform;

    void Awake()
    {
        playerPivotTransform = transform;
    }
}
