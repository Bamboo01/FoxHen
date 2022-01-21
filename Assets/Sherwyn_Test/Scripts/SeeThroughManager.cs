using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

namespace Sherwyn_Test
{
    public class SeeThroughManager : Singleton<SeeThroughManager>
    {
        const int _maxPlayers = 4;
        private int _numPlayers;
        private Vector3[] _playerPositions;
        private bool isInit = false;

        void InitManager(int numPlayers)
        {
            _numPlayers = numPlayers > _maxPlayers ? _maxPlayers : numPlayers;
            _playerPositions = new Vector3[_numPlayers];

            for(int i = 0; i < _numPlayers; i++)
            {
                _playerPositions[i] = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            }

            isInit = true;
        }

        void Update()
        {
            if (!isInit) return;


        }
    }
}
