using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bamboo.UI;
using Bamboo.Utility;
using UnityEngine.InputSystem;

public class MainMenuManagerPLUS : Singleton<MainMenuManagerPLUS>
{
    [SerializeField] List<PlayerSlot> playerSlots;
    Dictionary<PlayerSlot, GameObject> assignedSlots;

    private void Start()
    {
        assignedSlots = new Dictionary<PlayerSlot, GameObject>();
        foreach (PlayerSlot slot in playerSlots)
        {
            assignedSlots.Add(slot, null);
        }
        playerSlots.Clear();
    }

    public void playerEnter(PlayerInput input)
    {
        GameObject newPlayer = input.gameObject;
        int playerNum = 0;
        foreach (PlayerSlot slot in assignedSlots.Keys)
        {
            ++playerNum;
            assignedSlots.TryGetValue(slot, out GameObject assignedPlayer);
            if (assignedPlayer == null)
            {
                assignedSlots[slot] = newPlayer;
                slot.playerAssigned(playerNum);
                break;
            }
        }
    }

    public void playerLeave(GameObject player)
    {
        Destroy(player);
    }

    public void playerMenuInput(bool up, GameObject player)
    {

    }
}
