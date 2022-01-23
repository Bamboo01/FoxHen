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

    public class PlayerStatus : MonoBehaviour
    {
        public delegate void StatusEventTrigger(Status s);

        public Dictionary<Status, float> statusDuration { get; private set; }
        public Dictionary<Status, float> statusTime { get; private set; }
        public Dictionary<Status, StatusEventTrigger> statusStartedCallback { get; private set; }
        public Dictionary<Status, StatusEventTrigger> statusPerformedCallback { get; private set; }
        public Dictionary<Status, StatusEventTrigger> statusCancelledCallback { get; private set; }

        private List<Status> statusList;
        public Status status { get; private set; }

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

            statusPerformedCallback = new Dictionary<Status, StatusEventTrigger>();

            statusCancelledCallback = new Dictionary<Status, StatusEventTrigger>();

            
            foreach (var status in statusList)
            {
                statusStartedCallback.Add(status, null);
                statusPerformedCallback.Add(status, null);
                statusCancelledCallback.Add(status, null);
            }
            
            if (statusDuration.Count < statusList.Count - 1)
            {
                Debug.LogError("PlayerAttributes.cs: status duration != status count");
            }
        }

        private void Start()
        {
            status = Status.none;
        }

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
                        statusPerformedCallback[currStatus]?.Invoke(currStatus);
                    }
                }
            }
        }

        private void StopStatus(Status _status)
        {
            status &= ~_status;
            statusTime[_status] = 0.0f;
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
            statusStartedCallback[_status]?.Invoke(_status);
        }
    }
}