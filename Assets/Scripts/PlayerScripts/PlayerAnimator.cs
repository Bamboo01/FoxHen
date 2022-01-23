using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] RuntimeAnimatorController chickenAnimator;
    [SerializeField] RuntimeAnimatorController foxAnimator;

    private bool _isRunning = false;
    private bool _isChicken = true;

    public bool isChicken
    {
        set
        {
            _isChicken = value;
            playerAnimator.runtimeAnimatorController = value ? chickenAnimator : foxAnimator;
            transform.localScale = value ? new Vector3(0.5f, 0.5f, 0.5f) : new Vector3(0.8f, 0.8f, 0.8f);
        }
        get => _isChicken;
    }

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

    [ContextMenu("TestSwapAnim")]
    public void TestSwapAnim()
    {
        isChicken = !isChicken;
    }
}
