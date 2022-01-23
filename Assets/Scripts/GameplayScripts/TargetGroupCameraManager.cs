using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Bamboo.Utility;
using Bamboo.Events;

namespace FoxHen
{ 
    public class TargetGroupCameraManager : Singleton<TargetGroupCameraManager>
    {
        private CinemachineTargetGroup cinemachineTargetGroup;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        public bool TargetGroupOn;

        protected override void OnAwake()
        {
            _persistent = false;
            base.OnAwake();
        }

        void Start()
        {
            EventManager.Instance.Listen("PlayerSpawned", OnPlayerSpawned);
            cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

            //PlayerPositionsHolder[] seeThroughPlayers = FindObjectsOfType<PlayerPositionsHolder>();
            //foreach (var p in seeThroughPlayers)
            //{
            //    float radius = p.spriteRenderer.size.x > p.spriteRenderer.size.y ? p.spriteRenderer.size.x : p.spriteRenderer.size.y;
            //    cinemachineTargetGroup.AddMember(p.playerSpriteTransform, 1.0f, radius);
            //}

            TurnOffTargetGroup(99999);
        }

        void OnDestroy()
        {
            EventManager.Instance.Close("PlayerSpawned", OnPlayerSpawned);
        }

        void OnPlayerSpawned(IEventRequestInfo info)
        {
            PlayerPositionsHolder playerInfo = (info as EventRequestInfo).sender as PlayerPositionsHolder;
            float radius = playerInfo.spriteRenderer.size.x > playerInfo.spriteRenderer.size.y ? playerInfo.spriteRenderer.size.x : playerInfo.spriteRenderer.size.y;
            cinemachineTargetGroup.AddMember(playerInfo.playerSpriteTransform, 1.0f, radius);
        }

        public void TurnOffTargetGroup(float orthoSize)
        {
            cinemachineTargetGroup.enabled = false;
            var framingTransposer = cinemachineVirtualCamera.GetComponentInChildren<CinemachineFramingTransposer>();
            orthoSize = Mathf.Clamp(orthoSize, framingTransposer.m_MinimumOrthoSize, framingTransposer.m_MaximumOrthoSize);
            cinemachineVirtualCamera.m_Lens.OrthographicSize = orthoSize;
            cinemachineVirtualCamera.m_LookAt = null;
            cinemachineVirtualCamera.m_Follow = null;

            TargetGroupOn = false;
        }

        public void TurnOnTargetGroup()
        {
            cinemachineTargetGroup.enabled = true;
            cinemachineVirtualCamera.m_LookAt = cinemachineTargetGroup.transform;
            cinemachineVirtualCamera.m_Follow = cinemachineTargetGroup.transform;

            TargetGroupOn = true;
        }

        [ContextMenu("TestOff")]
        public void TestTurnOff()
        {
            TurnOffTargetGroup(99999);
        }

        [ContextMenu("TestOn")]
        public void TestTurnOn()
        {
            TurnOnTargetGroup();
        }
    }
}
