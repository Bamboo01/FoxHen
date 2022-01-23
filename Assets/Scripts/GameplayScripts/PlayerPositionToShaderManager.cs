using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;
using Bamboo.Events;

namespace FoxHen
{
    public class PlayerPositionToShaderManager : Singleton<PlayerPositionToShaderManager>
    {
        const int _maxPlayers = 4;
        private int _numPlayers;
        private List<PlayerPositionsHolder> _players = new List<PlayerPositionsHolder>();
        private bool isInit = false;

        private List<Renderer> renderers = new List<Renderer>();

        protected override void OnAwake()
        {
            base.OnAwake();
            _persistent = false;
        }

        void Start()
        {
            EventManager.Instance.Listen("PlayerSpawned", OnPlayerSpawned);

            var players = FindObjectsOfType<PlayerPositionsHolder>();
            _numPlayers = players.Length > _maxPlayers ? _maxPlayers :  players.Length;

            for (int i = 0; i < _numPlayers; i++)
            {
                _players.Add(players[i]);
            }
        }

        public void OnPlayerSpawned(IEventRequestInfo info)
        {
            if (_numPlayers == 4) return;
            if (_numPlayers == 0) isInit = true;
            _numPlayers++;

            _players.Add((info as EventRequestInfo).sender as PlayerPositionsHolder);
        }

        public void AddSeeThroughObject(Renderer obj)
        {
            renderers.Add(obj);
        }


        void Update()
        {
            if (!isInit) return;

            Vector4[] positions = new Vector4[_maxPlayers];
            Vector4[] pivotPositions = new Vector4[_maxPlayers];
            for (int i = 0; i < _players.Count; i++)
            {
                pivotPositions[i] = _players[i].playerPivotTransform.position;
                positions[i] = _players[i].playerSpriteTransform.position;
            }

            foreach(var a in renderers)
            {
                a.material.SetInt("_NumPlayers", _numPlayers);
                a.material.SetVectorArray("_PlayerPositions", positions);
                a.material.SetVectorArray("_PlayerPivotPositions", pivotPositions);
            }
        }
    }
}
