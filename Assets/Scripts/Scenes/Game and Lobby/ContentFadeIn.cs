using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen
{
    public class ContentFadeIn : MonoBehaviour
    {
        CanvasGroup content;
        // Start is called before the first frame update
        void Start()
        {
            content = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (content.alpha < 1)
                content.alpha += Time.deltaTime * 2;
            else
                this.enabled = false;
        }
    }
}
