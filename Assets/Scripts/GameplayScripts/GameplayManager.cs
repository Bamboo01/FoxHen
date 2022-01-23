using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bamboo.Events;
using Bamboo.Utility;
using TMPro;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine.Events;

namespace FoxHen
{
    public static class FoxHenList
    {
        public static void ShuffleMe<T>(this IList<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }
    }

    public struct PlayerKilledEvent
    {
        public PlayerController killer;
        public PlayerController victim;
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
        [SerializeField] GameObject deadPrefab;

        private GameObject activeLevel = null;
        private List<UnityAction> gameEvents = new List<UnityAction>();
        List<GameObject> players = new List<GameObject>();
        List<PlayerUI> playerUIs = new List<PlayerUI>();
        List<int> playerScores = new List<int>();

        void OnDestroy()
        {
            EventManager.Instance.Close("PlayerTouchedByFox", OnPlayerTouchedByFox);
        }

        void Start()
        {
            EventManager.Instance.Listen("PlayerTouchedByFox", OnPlayerTouchedByFox);
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
            foreach (var ui in playerUIs)
            {
                ui.gameObject.SetActive(false);
            }    

            foreach (var p in seeThroughPlayers)
            {
                players.Add(p.gameObject);
                p.gameObject.SetActive(false);
                playerUIs[p.GetComponent<PlayerController>().playerID].gameObject.SetActive(true);
            }

            foreach (var p in seeThroughPlayers)
            {
                p.gameObject.SetActive(false);
            }

            DoStartingTweenAnimation();


            // Shuffle
            foreach (var level in levels)
            {
                level.SetActive(false);
                gameEvents.Add(() =>
                {
                    activeLevel.SetActive(false);
                    levels[UnityEngine.Random.Range(0, levels.Count)].SetActive(true);
                    activeLevel = levels[UnityEngine.Random.Range(0, levels.Count)];

                    subtitleTransform.gameObject.SetActive(true);
                    subtitleText.text = "Let's spice up the level!";
                    subtitleTransform.DOScale(1, 1.6f).From(Vector3.zero).OnComplete(() =>
                    {
                        subtitleTransform.DOScale(0, 1.0f).OnComplete(() =>
                        {
                            subtitleTransform.gameObject.SetActive(false);
                        });
                    });
                });
            }

            foreach (var player in players)
            {
                var controller = player.GetComponent<PlayerController>();
                gameEvents.Add(() =>
                {
                    foreach (var ui in playerUIs)
                        ui.OnChangeFox(false);

                    playerUIs[controller.playerID].OnChangeFox(true);
                    subtitleTransform.gameObject.SetActive(true);
                    subtitleText.text = "Someone's becoming the fox!";
                    subtitleTransform.DOScale(1, 1.6f).From(Vector3.zero).OnComplete(() =>
                    {
                        subtitleTransform.DOScale(0, 1.0f).OnComplete(() =>
                        {
                            subtitleTransform.gameObject.SetActive(false);
                        });
                    });

                    controller.TurnIntoFox();
                });
            }

            gameEvents.ShuffleMe();

            gameEvents.Add(() =>
            {

                subtitleTransform.gameObject.SetActive(true);
                subtitleText.text = "Game ending in 20 seconds, wrap things up!";
                subtitleTransform.DOScale(1, 1.6f).From(Vector3.zero).OnComplete(() =>
                {
                    subtitleTransform.DOScale(0, 1.0f).OnComplete(() =>
                    {
                        subtitleTransform.gameObject.SetActive(false);
                    });
                });
            });

            gameEvents.Add(() =>
            {
                int highest = 0;
                int highestScore = -1;
                for (int i = 0; i < playerScores.Count; i++)
                {
                    if (playerScores[i] > highestScore)
                    {
                        highestScore = playerScores[i];
                        highest = i;
                    }
                }

                subtitleTransform.gameObject.SetActive(true);
                subtitleText.text = "Game over! Winner is player " + (highest + 1).ToString();
                subtitleTransform.DOScale(1, 1.6f).From(Vector3.zero).OnComplete(() =>
                {
                    subtitleTransform.DOScale(0, 1.0f).OnComplete(() =>
                    {
                        subtitleTransform.gameObject.SetActive(false);
                    });
                    SceneManager.LoadScene("LobbyScene");
                });
            });

            activeLevel = levels[0];
            activeLevel.SetActive(true);
        }

        public void OnPlayerTouchedByFox(IEventRequestInfo info)
        {
            EventRequestInfo<PlayerKilledEvent> eventRequestInfo = info as EventRequestInfo<PlayerKilledEvent>;
            var victim = eventRequestInfo.body.victim;
            var killer = eventRequestInfo.body.killer;

            victim.gameObject.SetActive(false);
            Destroy(Instantiate(deadPrefab, victim.transform.position, Quaternion.identity), 5.0f);
            StartCoroutine(RespawnDelayed(victim));

            if (killer.playerID <= 4)
            {
                playerScores[killer.playerID]++;
                playerUIs[killer.playerID].OnKilledPlayer(playerScores[killer.playerID]);
            }
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
                                StartCoroutine(BeginGameActions());
                                var controller = p.GetComponent<PlayerController>();
                                if (controller.playerID <= 4)
                                    playerUIs[controller.playerID].gameObject.SetActive(true);
                            }
                            subtitleTransform.DOScale(0, 1.0f).OnComplete(() =>
                            {
                                subtitleTransform.gameObject.SetActive(false);
                            });
                        });
                    });
                });
            });
        }

        IEnumerator RespawnDelayed(PlayerController player)
        {
            yield return new WaitForSeconds(1.0f);
            player.gameObject.SetActive(true);
            player.transform.position = SpawnpointManager.Instance.GetSpawnPositionRandom();
        }

        IEnumerator BeginGameActions()
        {
            yield return new WaitForSeconds(5.0f);
            for(int i = 0; i < gameEvents.Count; i++)
            {
                gameEvents[i].Invoke();
                yield return new WaitForSeconds(20.0f);
            }
            yield break;
        }
    }
}

