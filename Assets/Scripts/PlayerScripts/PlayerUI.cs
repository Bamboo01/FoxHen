using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Image playerIcon;
    [SerializeField] Sprite foxIcon;
    [SerializeField] Sprite chickenIcon;

    public void OnChangeFox(bool isTrue)
    {
        playerIcon.sprite = isTrue ? foxIcon : chickenIcon;
    }

    public void OnKilledPlayer(int newScore)
    {
        scoreText.text = "Score: " + newScore.ToString();
    }
}
