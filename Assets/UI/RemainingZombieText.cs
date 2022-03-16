using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingZombieText : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        text.text = GameManager.remainingZombies.ToString();
    }
}
