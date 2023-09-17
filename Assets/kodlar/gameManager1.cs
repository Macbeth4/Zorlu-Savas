using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager1 : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public float score;
    
    private void Start() {
        UpdateHiscore();
        score = 0;
    }

    private void Update() {
        scoreText.text = score.ToString();
    }
    public void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString();
    }

    public void skoruarttir(){
        score++;
    }

    public void textortala(){
       RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        // Canvas'ın genişliği ve yüksekliği
        float canvasWidth = rectTransform.rect.width;
        float canvasHeight = rectTransform.rect.height;

        // Text'in genişliği ve yüksekliği
        float textWidth = rectTransform.sizeDelta.x;
        float textHeight = rectTransform.sizeDelta.y;

        // Text'in yeni pozisyonunu hesapla (sahnenin ortasına yerleştir)
        float newX = canvasWidth * 0.5f - textWidth * 0.5f;
        float newY = canvasHeight * 0.5f - textHeight * 0.5f;

        // Yeni pozisyonu atayarak Text'i taşı
        rectTransform.anchoredPosition = new Vector2(newX, newY);
    }
}

