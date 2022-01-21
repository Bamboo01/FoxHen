using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bamboo.Audio
{
    public class AudioTest : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(randomWhistles());
            StartCoroutine(messBGM());
        }

        IEnumerator randomWhistles()
        {
            while (true)
            {
                SoundManager.Instance.PlaySoundAtPoint("whistle", new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2)));
                yield return new WaitForSeconds(3.0f);
            }
        }

        IEnumerator messBGM()
        {
            while (true)
            {
                yield return SoundManager.Instance.FadeInSound("bgm1", Random.Range(1.0f, 5.0f));
                yield return SoundManager.Instance.FadeOutSound("bgm1", Random.Range(1.0f, 5.0f));
                yield return SoundManager.Instance.FadeInSound("bgm2", Random.Range(1.0f, 5.0f));
                yield return SoundManager.Instance.FadeOutSound("bgm2", Random.Range(1.0f, 5.0f));
            }
        }
    }
}
