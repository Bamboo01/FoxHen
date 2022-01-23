using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FoxHen
{
    public class PlayerSlot : MonoBehaviour
    {
        public void playerAssigned()
        {
            gameObject.SetActive(false);
        }
    }
}
