using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    private bool _isRunning = false;

    public void PickupItem()
    {
        playerAnimator.SetTrigger("TriggerPickup");
    }

    public void IsRunning(bool isRunning)
    {
        if (_isRunning == isRunning) return;
        _isRunning = isRunning;
        playerAnimator.SetBool("IsRunning", isRunning);
    }
}
