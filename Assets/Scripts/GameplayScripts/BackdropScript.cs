using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxHen
{
    public class BackdropScript : MonoBehaviour
    {
        [SerializeField] PolygonCollider2D cameraConfiner;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Vector2 outOfBoundsThreshold;

        void Start()
        {
            float x = spriteRenderer.size.x + (outOfBoundsThreshold.x * 2);
            float y = spriteRenderer.size.y + (outOfBoundsThreshold.y * 2);
            cameraConfiner.points[0] = new Vector2(x / -2.0f, y / 2.0f);
            cameraConfiner.points[1] = new Vector2(x / 2.0f, y / 2.0f);
            cameraConfiner.points[2] = new Vector2(x / 2.0f, y / -2.0f);
            cameraConfiner.points[3] = new Vector2(x / -2.0f, y / -2.0f);
        }

        void UpdateBackdrop(Sprite newBg, Vector2 newSize, Vector2 newOutOfBoundsThreshold)
        {
            spriteRenderer.sprite = newBg;
            spriteRenderer.size = newSize;
            outOfBoundsThreshold = newOutOfBoundsThreshold;


            float x = spriteRenderer.size.x + (outOfBoundsThreshold.x * 2);
            float y = spriteRenderer.size.y + (outOfBoundsThreshold.y * 2);
            cameraConfiner.points[0] = new Vector2(x / -2.0f, y / 2.0f);
            cameraConfiner.points[1] = new Vector2(x / 2.0f, y / 2.0f);
            cameraConfiner.points[2] = new Vector2(x / 2.0f, y / -2.0f);
            cameraConfiner.points[3] = new Vector2(x / -2.0f, y / -2.0f);
        }
    }
}