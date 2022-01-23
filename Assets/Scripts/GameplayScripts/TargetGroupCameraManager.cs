using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bamboo.Utility;
using Bamboo.Events;

namespace FoxHen
{ 
    public class TargetGroupCameraManager : MonoBehaviour
    {
        private CinemachineTargetGroup cinemachineTargetGroup;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        void Start()
        {
            EventManager.Instance.Listen("PlayerSpawned", OnPlayerSpawned);
            cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

            PlayerPositionsHolder[] seeThroughPlayers = FindObjectsOfType<PlayerPositionsHolder>();
            foreach (var p in seeThroughPlayers)
            {
                float radius = p.spriteRenderer.size.x > p.spriteRenderer.size.y ? p.spriteRenderer.size.x : p.spriteRenderer.size.y;
                cinemachineTargetGroup.AddMember(p.playerSpriteTransform, 1.0f, radius);
            }
        }

        void OnPlayerSpawned(IEventRequestInfo info)
        {
            PlayerPositionsHolder playerInfo = (info as EventRequestInfo).sender as PlayerPositionsHolder;
            float radius = playerInfo.spriteRenderer.size.x > playerInfo.spriteRenderer.size.y ? playerInfo.spriteRenderer.size.x : playerInfo.spriteRenderer.size.y;
            cinemachineTargetGroup.AddMember(playerInfo.playerSpriteTransform, 1.0f, radius);
        }

        void TurnOffTargetGroup(int orthoSize)
        {
            cinemachineTargetGroup.enabled = false;
            //cinemachineVirtualCamera.m_Lens.OrthographicSize = cinemachineVirtualCamera.
        }
    }
}
