using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen
{
    public class StartGameCollider : MonoBehaviour
    {
        ContactFilter2D contactFilter;
        List<Collider2D> results;
        bool allPlayersPresent;

        private void Start()
        {
            contactFilter = new ContactFilter2D();
            results = new List<Collider2D>();
            allPlayersPresent = false;
        }

        private void Update()
        {
            if (allPlayersPresent)
                LobbyManager.Instance.AllPlayersInCoop(Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            int playerCOOPCount = 0;
            gameObject.GetComponent<Collider2D>().OverlapCollider(contactFilter, results);
            foreach (Collider2D collider in results)
            {
                if (collider.gameObject.tag == "Player")
                    ++playerCOOPCount;
            }
            results.Clear();
            if (playerCOOPCount == LobbyManager.Instance.GetPlayerCount())
            {
                allPlayersPresent = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                LobbyManager.Instance.PlayerExitCoop();
                allPlayersPresent = false;
            }
        }
    }
}
