using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

public class PlayerPositionToShaderManager : Singleton<PlayerPositionToShaderManager>
{
    const int _maxPlayers = 4;
    private int _numPlayers;
    private PlayerPositionsHolder[] _players;
    private bool isInit = false;

    private List<Renderer> renderers = new List<Renderer>();

    protected override void OnAwake()
    {
        base.OnAwake();
        _persistent = false;
    }

    public void InitManager(PlayerPositionsHolder[] players)
    {
        _numPlayers = players.Length > _maxPlayers ? _maxPlayers : players.Length;
        _players = players;
        isInit = true;
    }

    public void AddSeeThroughObject(Renderer obj)
    {
        renderers.Add(obj);
    }


    void Update()
    {
        if (!isInit) return;

        Vector4[] positions = new Vector4[_players.Length];
        Vector4[] pivotPositions = new Vector4[_players.Length];
        for (int i = 0; i < _players.Length; i++)
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
