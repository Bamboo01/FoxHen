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
            foreach (PlayerSlot slot in assignedSlots.Keys)
            {
                assignedSlots.TryGetValue(slot, out GameObject assignedPlayer);
                if (assignedPlayer == null)
                {
                    assignedSlots[slot] = newPlayer;
                    slot.playerAssigned();
                    break;
                }
            }
        }

        public void AllPlayersInCoop(float deltaTime)
        {
            timeTillGameplaySceneTransisiton -= deltaTime;
            if (timeTillGameplaySceneTransisiton <= 0)
            {
                SceneManager.LoadScene("Gameplay");
            }
        }

        public void PlayerExitCoop()
        {
            timeTillGameplaySceneTransisiton = 5f;
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
