using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bamboo.UI;
using Bamboo.Utility;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

namespace FoxHen
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        [SerializeField] List<PlayerSlot> playerSlots;
        [SerializeField] Text timerText;
        Dictionary<PlayerSlot, GameObject> assignedSlots;
        float timeTillGameplaySceneTransisiton;

        private void Start()
        {
            assignedSlots = new Dictionary<PlayerSlot, GameObject>();
            foreach (PlayerSlot slot in playerSlots)
            {
                assignedSlots.Add(slot, null);
            }
            playerSlots.Clear();
            timeTillGameplaySceneTransisiton = 5f;
        }

        public void playerEnter(PlayerInput input)
        {
            GameObject newPlayer = input.gameObject;
            int playerNum = 0;
            foreach (PlayerSlot slot in assignedSlots.Keys)
            {
                assignedSlots.TryGetValue(slot, out GameObject assignedPlayer);
                if (assignedPlayer == null)
                {
                    assignedSlots[slot] = newPlayer;
                    slot.playerAssigned();
                    Color indicatorColor = Color.grey;
                    switch (playerNum)
                    {
                        case 1:
                            indicatorColor = Color.red;
                            break;
                        case 2:
                            indicatorColor = Color.green;
                            break;
                        case 3:
                            indicatorColor = Color.blue;
                            break;
                    }
                    newPlayer.GetComponentInChildren<PlayerController>().playerIndicatorSprite.color = indicatorColor;
                    break;
                }
                ++playerNum;
            }
        }

        public void AllPlayersInCoop(float deltaTime)
        {
            timeTillGameplaySceneTransisiton -= deltaTime;
            timerText.text = timeTillGameplaySceneTransisiton.ToString("0.#");
            if (timeTillGameplaySceneTransisiton <= 0)
            {
                AudioManager.Instance.PauseAllMusic();
                SceneManager.LoadScene("Gameplay");
            }

            if (!TargetGroupCameraManager.Instance.TargetGroupOn)
                TargetGroupCameraManager.Instance.TurnOnTargetGroup();
        }

        public void TimerReset()
        {
            timeTillGameplaySceneTransisiton = 5f;
            timerText.text = "";

            if (TargetGroupCameraManager.Instance.TargetGroupOn)
                TargetGroupCameraManager.Instance.TurnOffTargetGroup(99999);
        }

        public int GetPlayerCount()
        {
            return PlayerInput.all.Count;
        }

        public void playerLeave(GameObject player)
        {
            Destroy(player);
        }

        public void playerMenuInput(bool up, GameObject player)
        {

        }
    }
}
