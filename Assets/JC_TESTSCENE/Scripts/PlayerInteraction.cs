using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;
        [SerializeField] private PlayerData playerData;

        public const float slowPercent = 0.7f;

        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            playerData = GetComponent<PlayerData>();
            SetUpCallbacks();
        }

        private void SetUpCallbacks()
        {
            playerStatus.statusStartedCallback[Status.slowed] += SlowedStartCallback;
            playerStatus.statusStartedCallback[Status.stunned] += StunnedCallback;
            playerStatus.statusStartedCallback[Status.hastened] += HastenedStartedCallback;
            playerStatus.statusStartedCallback[Status.invulnerable] += InvulnerableCallback;
            playerStatus.statusStartedCallback[Status.confused] += StartConfusedCallback;

            playerStatus.statusPerformedCallback[Status.slowed] += InvulnerableCallback;

            playerStatus.statusCancelledCallback[Status.stunned] += UnstunnedCallback;
            playerStatus.statusCancelledCallback[Status.invulnerable] += VulnerableCallback;
            playerStatus.statusCancelledCallback[Status.slowed] += SlowedStopCallback;
            playerStatus.statusCancelledCallback[Status.hastened] += StopHasteCallback;
            playerStatus.statusCancelledCallback[Status.confused] += StopConfusedCallback;
        }

        #region callbacks
        private void SlowedStartCallback(Status _status)
        {
            playerData.eventStack.Add(SlowDown);
        }

        private void SlowedStopCallback(Status _status)
        {
            Debug.Log(playerData.eventStack.Count);
            playerData.eventStack.Remove(SlowDown);
            Debug.Log(playerData.eventStack.Count);
        }

        private void StunnedCallback(Status _status)
        {
            playerData.isStunned = true;
            playerData.eventStack.Add(Stop);
        }

        private void HastenedStartedCallback(Status _status)
        {
            playerData.eventStack.Add(MoveFast);
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
            playerData.isStunned = false;
            playerData.eventStack.Remove(Stop);
        }

        private void StopHasteCallback(Status _status)
        {
            playerData.eventStack.Remove(MoveFast);
        }

        private void StartConfusedCallback(Status _status)
        {
            playerData.eventStack.Add(Confused);
        }

        private void StopConfusedCallback(Status _status)
        {
            playerData.eventStack.Remove(Confused);
        }

        public float SlowDown(float moveSpeed)
        {
            if (playerData.isInvulnerable)
            {
                return moveSpeed;
            }
            return moveSpeed * (1 - slowPercent);
        }

        public float Stop(float moveSpeed)
        {
            return moveSpeed * 0.0f;
        }

        public float MoveFast(float moveSpeed)
        {
            return moveSpeed * (1 / (1 - slowPercent));
        }

        public float Confused(float moveSpeed)
        {
            if (playerData.isInvulnerable)
            {
                return moveSpeed;
            }
            return moveSpeed *= -1;
        }

        #endregion
    }
}