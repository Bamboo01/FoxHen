using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen
{
    public class ItemboxSpawner : MonoBehaviour
    {
        public GameObject mysterybox;
        public bool isEnabled;
        float respawnTime;
        const float respawnDuration = 5.0f;

        private void Start()
        {
            respawnTime = 0.0f;
            isEnabled = true;
        }

        private void Update()
        {
            if (!isEnabled)
                return;

            if (mysterybox)
                return;

            respawnTime += Time.deltaTime;
            if(respawnTime > respawnDuration)
            {
                //spawn mystery box
                Instantiate(mysterybox, transform);
            }
        }
    }
}
