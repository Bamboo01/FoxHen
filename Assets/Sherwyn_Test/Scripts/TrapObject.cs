using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] public Renderer renderer;
    [SerializeField] public float minSeeThroughDistance;
    [SerializeField] public float maxSeeThroughDistance;

    void Start()
    {
        PlayerPositionToShaderManager.Instance.AddSeeThroughObject(renderer);
        renderer.material.SetFloat("_MinDistance", minSeeThroughDistance);
        renderer.material.SetFloat("_MaxDistance", maxSeeThroughDistance);
    }

    void Update()
    {
#if UNITY_EDITOR //Lazy debug, can be skipped in build
        renderer.material.SetVector("_CurrPosition", transform.position);
        renderer.material.SetFloat("_MinDistance", minSeeThroughDistance);
        renderer.material.SetFloat("_MaxDistance", maxSeeThroughDistance);
#endif
    }
}
