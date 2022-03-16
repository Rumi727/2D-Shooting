using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    float tempScore = 0;
    float score = 0;

    bool isScoreDown = false;
    float scoreColortimer = 0;
    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        score = Mathf.Round(Mathf.Lerp(score, GameManager.score, 0.125f * GameManager.fpsDeltaTime) * 1000) * 0.001f;

        if (Mathf.Round(tempScore) != Mathf.Round(score))
        {
            text.text = Mathf.Round(score).ToString();
            scoreColortimer = 0;
        }

        if (Mathf.Round(tempScore) > Mathf.Round(GameManager.score))
            isScoreDown = true;
        else
            isScoreDown = false;

        tempScore = score;

        if (isScoreDown)
            text.color = Color.Lerp(text.color, Color.red, 0.25f * GameManager.fpsDeltaTime);
        else
            text.color = Color.Lerp(text.color, Color.black, 0.25f * GameManager.fpsDeltaTime);

        if (scoreColortimer > 0.1f)
            isScoreDown = false;
        else
            scoreColortimer += Time.deltaTime;
    }
}
