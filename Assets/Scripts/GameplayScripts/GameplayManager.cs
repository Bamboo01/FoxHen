using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Events;
using TMPro;
using DG.Tweening;

namespace FoxHen
{
    public struct PlayerKilledEvent
    {
        PlayerController killer;
        PlayerController victim;
    }

    public class GameplayManager : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] RectTransform subtitleTransform;
        [SerializeField] TMP_Text subtitleText;

        [Header("UI icons")]
        [SerializeField] PlayerUI playerOneUI;
        [SerializeField] PlayerUI playerTwoUI;
        [SerializeField] PlayerUI playerThreeUI;
        [SerializeField] PlayerUI playerFourUI;

        [Header("Gameplay")]
        [SerializeField] List<GameObject> levels;

        List<GameObject> players = new List<GameObject>();
        List<PlayerUI> playerUIs = new List<PlayerUI>();
        List<int> playerScores = new List<int>();

        void Start()
        {
            playerUIs = new List<PlayerUI>();

            playerUIs.Add(playerOneUI);
            playerUIs.Add(playerTwoUI);
            playerUIs.Add(playerThreeUI);
            playerUIs.Add(playerFourUI);

            playerScores.Add(0);
            playerScores.Add(0);
            playerScores.Add(0);
            playerScores.Add(0);

            PlayerPositionsHolder[] seeThroughPlayers = FindObjectsOfType<PlayerPositionsHolder>();
            foreach (var p in seeThroughPlayers)
            {
                players.Add(p.gameObject);
                p.gameObject.SetActive(false);
            }

            foreach (var p in seeThroughPlayers)
            {
                p.gameObject.SetActive(false);
            }

            DoStartingTweenAnimation();
        }

        public void RandomlySwitchLevel()
        {

        }

        public void ChooseRandomFox()
        {

        }

        private void DoStartingTweenAnimation()
        {
            subtitleText.text = "Ready?";
            subtitleTransform.DOScale(1, 1.6f).From(Vector3.zero).OnComplete(() =>
            {
                subtitleText.text = "Foxes...";
                subtitleTransform.DOShakeAnchorPos(0.8f, 50, 5, 80).OnComplete(()=>
                {
                    subtitleText.text = "Hens...";
                    subtitleTransform.DOShakeAnchorPos(0.8f, 50, 5, 80).OnComplete(() =>
                    {
                        subtitleText.text = "GO!";
                        subtitleTransform.DOShakeAnchorPos(0.8f, 50, 5, 80).OnComplete(() =>
                        {
                            foreach (var p in players)
                            {
                                p.gameObject.SetActive(true);
                                EventManager.Instance.Publish("PlayerSpawned", p.GetComponent<PlayerPositionsHolder>());

                                var controller = p.GetComponent<PlayerController>();
                                if (controller.playerID <= 4)
                                    playerUIs[controller.playerID].gameObject.SetActive(true);
                            }
                            subtitleTransform.DOScale(0, 1.0f);
                        });
                    });
                });
            });
        }
    }
}

