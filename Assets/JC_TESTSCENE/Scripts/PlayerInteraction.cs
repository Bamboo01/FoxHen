using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen {
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private GameObject bubble;
        [SerializeField] private GameObject speedup;
        [SerializeField] private GameObject slowdown;

        public GameObject bear_trap_prefab;
        public GameObject glue_trap_prefab;
        public GameObject magic_mushroom_trap_prefab;
        public GameObject snare_trap_prefab;

        public Vector2 direction = Vector2.zero;
        public const float slowPercent = 0.4f;

        private void Start()
        {
            playerStatus = GetComponent<PlayerStatus>();
            playerData = GetComponent<PlayerData>();
            playerInventory = GetComponent<PlayerInventory>();
            rigidbody = GetComponent<Rigidbody2D>();
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

            playerInventory.activateItemDelegate += useItem;
        }

        #region callbacks
        private void SlowedStartCallback(Status _status)
        {
            playerData.eventStack.Add(SlowDown);
            slowdown.SetActive(true);

        }

        private void SlowedStopCallback(Status _status)
        {
            playerData.eventStack.Remove(SlowDown);
            slowdown.SetActive(false);
        }

        private void StunnedCallback(Status _status)
        {
            playerData.isStunned = true;
            playerData.eventStack.Add(Stop);
        }

        private void HastenedStartedCallback(Status _status)
        {
            playerData.eventStack.Add(MoveFast);
            speedup.SetActive(true);
        }

        private void InvulnerableCallback(Status _status)
        {
            playerData.isInvulnerable = true;
            bubble.SetActive(true);
        } 
        
        private void VulnerableCallback(Status _status)
        {
            playerData.isInvulnerable = false;
            bubble.SetActive(false);
        }

        private void UnstunnedCallback(Status _status)
        {
            playerData.isStunned = false;
            playerData.eventStack.Remove(Stop);
        }

        private void StopHasteCallback(Status _status)
        {
            playerData.eventStack.Remove(MoveFast);
            speedup.SetActive(false);
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

        public void Flashed(Status _status)
        {
            if (playerData.moveInputValue.magnitude > Mathf.Epsilon)
            {
                rigidbody.position += playerData.lastMoveDirection * 5.0f;
            }
        }
        #endregion

        public void useItem(ItemType item)
        {
            Vector2 instantiate_pos = playerData.lastMoveDirection * 1.0f;
            if(instantiate_pos.magnitude < Mathf.Epsilon)
            {
                instantiate_pos = new Vector2(-1, 0);
            }

            switch (item)
            {
                case ItemType.bear_trap:
                    {
                        GameObject trap = Instantiate(bear_trap_prefab, transform.position + new Vector3(instantiate_pos.x, instantiate_pos.y, 0), Quaternion.identity);
                        trap.GetComponent<AbstractGameplayInteractable>().self = gameObject;
                        break;
                    }
                case ItemType.glue_trap:
                    {
                        Instantiate(glue_trap_prefab, transform.position + new Vector3(instantiate_pos.x, instantiate_pos.y, 0), Quaternion.identity);
                        break;
                    }
                case ItemType.magic_mushroom_trap:
                    {
                        Instantiate(magic_mushroom_trap_prefab, transform.position + new Vector3(instantiate_pos.x, instantiate_pos.y, 0), Quaternion.identity);
                        break;
                    }
                case ItemType.snare_trap:
                    {
                        Instantiate(snare_trap_prefab, transform.position + new Vector3(instantiate_pos.x, instantiate_pos.y, 0), Quaternion.identity);
                        break;
                    }
                case ItemType.chicken_feed:
                    {
                        playerStatus.AddStatus(Status.hastened);
                        break;
                    }
                case ItemType.chicken_flash:
                    {
                        playerStatus.AddStatus(Status.flashed);
                        break;
                    }
                case ItemType.chicken_shield:
                    {
                        playerStatus.AddStatus(Status.invulnerable);
                        break;
                    }
                case ItemType.berries:
                    {
                        playerStatus.AddStatus(Status.hastened);
                        break;
                    }
                case ItemType.speed:
                    {
                        playerStatus.AddStatus(Status.hastened);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}