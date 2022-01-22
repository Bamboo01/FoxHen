using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoxHen {
    [System.Flags]
    public enum Status : int {
        none = 0,
        slowed = 1 << 0,
        stunned = 1 << 1,
        hastened = 1 << 2,
        invulnerable = 1 << 3,
        total = ~0
    }

    public class PlayerAttributes : MonoBehaviour
    {
        private delegate void StatusEventTrigger(Status s);

        public float moveSpeed { get; private set; }
        private float defaultMoveSpeed = 5.0f;

        public bool charType { get; private set; }

        private Dictionary<Status, float> statusDuration;
        private Dictionary<Status, float> statusTime;
        private Dictionary<Status, StatusEventTrigger> statusStartedCallback;
        private Dictionary<Status, StatusEventTrigger> statusPerformedCallback;
        private Dictionary<Status, StatusEventTrigger> statusCancelledCallback;
        private List<Status> statusList;
        private Status status;

        public bool isInvulnerable { get; private set;  }

        private void Awake()
        {
            statusList = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();

            statusDuration = new Dictionary<Status, float>();
            statusDuration.Add(Status.none, 0.0f);
            statusDuration.Add(Status.slowed, 3.0f);
            statusDuration.Add(Status.stunned, 1.0f);
            statusDuration.Add(Status.hastened, 2.0f);
            statusDuration.Add(Status.invulnerable, 1.0f);

            statusTime = new Dictionary<Status, float>();
            foreach (var status in statusList)
            {
                statusTime.Add(status, 0.0f);
            }

            statusStartedCallback = new Dictionary<Status, StatusEventTrigger>();
            statusStartedCallback.Add(Status.invulnerable, InvulnerableCallback);
            statusStartedCallback.Add(Status.slowed, SlowedCallback);
            statusStartedCallback.Add(Status.stunned, StunnedCallback);
            statusStartedCallback.Add(Status.hastened, HastenedCallback);

            statusPerformedCallback = new Dictionary<Status, StatusEventTrigger>();

            statusCancelledCallback = new Dictionary<Status, StatusEventTrigger>();
            statusCancelledCallback.Add(Status.slowed, HastenedCallback);
            statusCancelledCallback.Add(Status.stunned, StunnedCallback);
            statusCancelledCallback.Add(Status.hastened, SlowedCallback);
            statusCancelledCallback.Add(Status.invulnerable, VulnerableCallback);

            if(statusDuration.Count < statusList.Count - 1)
            {
                Debug.LogError("PlayerAttributes.cs: status duration != status count");
            }
        }

        private void Start()
        {
            moveSpeed = defaultMoveSpeed;
            status = Status.none;
        }

        #region callbacks

        private void SlowedCallback(Status _status)
        {
            moveSpeed *= 0.6667f;
            Debug.Log(moveSpeed);
        }

        private void StunnedCallback(Status _status)
        {
            moveSpeed *= 0.0f;
        }

        private void HastenedCallback(Status _status)
        {
            moveSpeed *= 1.5f;
            Debug.Log(moveSpeed);
        }

        private void InvulnerableCallback(Status _status)
        {
            isInvulnerable = true;
        }

        private void VulnerableCallback(Status _status)
        {
            isInvulnerable = false;
        }
        #endregion

        #region things that you will probably never change
        private void Update()
        {
            foreach (var currStatus in statusList)
            {
                if (!statusTime.ContainsKey(currStatus) || !statusDuration.ContainsKey(currStatus))
                {
                    return;
                }

                if ((status & currStatus) == currStatus)
                {
                    if (statusTime[currStatus] > statusDuration[currStatus])
                    {
                        StopStatus(currStatus);
                    }
                    else
                    {
                        statusTime[currStatus] += Time.deltaTime;
                        if(statusPerformedCallback.ContainsKey(currStatus))
                            statusPerformedCallback[currStatus]?.Invoke(currStatus);
                    }
                }
            }
        }

        private void StopStatus(Status _status)
        {
            status &= ~_status;
            statusTime[_status] = 0.0f;
            if(statusCancelledCallback.ContainsKey(_status))
                statusCancelledCallback[_status]?.Invoke(_status);
        }

        public void AddStatus(Status _status)
        {
            if ((status & _status) == _status)
            {
                StopStatus(_status);
                status |= _status;
            }
            else
            {
                status |= _status;
                statusTime[_status] = 0.0f;
            }
            if (statusStartedCallback.ContainsKey(_status))
                statusStartedCallback[_status]?.Invoke(_status);
        }
        #endregion
    }


}