using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

namespace Sherwyn_Test
{
    public class PlayerPositionTestManager : Singleton<PlayerPositionTestManager>
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            _persistent = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            PlayerPositionsHolder[] seeThroughPlayers = FindObjectsOfType<PlayerPositionsHolder>();
            PlayerPositionToShaderManager.Instance.InitManager(seeThroughPlayers);
        }
    }
}
