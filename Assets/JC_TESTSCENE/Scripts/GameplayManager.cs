using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager self;

    private void Awake()
    {
        self = this;
    }
}
