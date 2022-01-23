using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;
using Bamboo.Events;

namespace FoxHen
{
    public class SpawnpointManager : Singleton<SpawnpointManager>
    {
        List<Transform> spawnPoints = new List<Transform>();
        override protected void OnAwake()
        {
            _persistent = false;
            EventManager.Instance.Listen("PlayerSpawned", OnPlayerSpawned);
            foreach (Transform child in transform)
            {
                spawnPoints.Add(child);
            }
        }

        void OnDestroy()
        {
            EventManager.Instance.Close("PlayerSpawned", OnPlayerSpawned);
        }

        public void OnPlayerSpawned(IEventRequestInfo info)
        {
            // TODO: play particles when they spawn
            ((info as EventRequestInfo).sender as PlayerPositionsHolder).transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }

        public Vector3 GetSpawnPositionRandom()
        {
            return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }
    }
}
