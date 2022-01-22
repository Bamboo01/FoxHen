using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;
using FoxHen;

namespace FoxHen
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] List<GameObject> levelPrefabs = new List<GameObject>();
    }
}
