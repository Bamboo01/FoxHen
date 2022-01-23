using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen
{
    public class SeeThroughObject : MonoBehaviour
    {
        [SerializeField] public Renderer renderer;
        [SerializeField] public float minSeeThroughDistance = 0.1f;
        [SerializeField] public float maxSeeThroughDistance = 0.5f;

        void Start()
        {
            PlayerPositionToShaderManager.Instance.AddSeeThroughObject(renderer);
            renderer.material.SetFloat("_MinDistance", minSeeThroughDistance);
            renderer.material.SetFloat("_MaxDistance", maxSeeThroughDistance);
        }

        void Update()
        {
            renderer.material.SetVector("_CurrPosition", transform.position);
    #if UNITY_EDITOR //Lazy debug, can be skipped in build
            renderer.material.SetFloat("_MinDistance", minSeeThroughDistance);
            renderer.material.SetFloat("_MaxDistance", maxSeeThroughDistance);
    #endif
        }
    }
}
