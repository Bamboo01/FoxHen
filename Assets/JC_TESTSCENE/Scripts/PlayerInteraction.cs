using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerStatus playerStatus;
        private PlayerData playerData;

        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            playerData = GetComponent<PlayerData>();
            SetUpCallbacks();

        }

        private void SetUpCallbacks()
        {
            playerStatus.statusStartedCallback[Status.slowed] += SlowedCallback;
            playerStatus.statusStartedCallback[Status.stunned] += StunnedCallback;
            playerStatus.statusStartedCallback[Status.hastened] += HastenedCallback;
            playerStatus.statusStartedCallback[Status.invulnerable] += InvulnerableCallback;

            playerStatus.statusPerformedCallback[Status.slowed] += InvulnerableCallback;

            playerStatus.statusTime[Status.slowed] = 5.0f;


        }

        #region callbacks

        private void SlowedCallback(Status _status)
        {
            playerData.moveSpeed *= 0.6667f;
        }

        private void SlowedPerformedCallback(Status _status)
        {

        }

        private void StunnedCallback(Status _status)
        {
            playerData.moveSpeed *= 0.0f;
        }

        private void HastenedCallback(Status _status)
        {
            playerData.moveSpeed *= 1.5f;
        }

        private void InvulnerableCallback(Status _status)
        {
            playerData.isInvulnerable = true;
        }

        private void VulnerableCallback(Status _status)
        {
            playerData.isInvulnerable = false;
        }

        private void UnstunnedCallback(Status _status)
        {
            playerData.moveSpeed = playerData.defaultMoveSpeed;
        }
        #endregion
    }
}