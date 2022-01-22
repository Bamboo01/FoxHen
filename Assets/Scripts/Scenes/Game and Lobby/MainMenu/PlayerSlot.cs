using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    public void playerAssigned(int number)
    {
        GetComponentInChildren<Text>().text = $"Player {number}!";
        //gameObject.SetActive(false);
    }
}
