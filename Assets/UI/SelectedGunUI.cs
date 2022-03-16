using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedGunUI : MonoBehaviour
{
    public Image image;
    public TMP_Text loadedBulletsText;
    public TMP_Text remainingBulletsText;

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        image.sprite = GunManager.selectedGun.uiSprite;

        loadedBulletsText.text = GunManager.loadedBullets.ToString();
        remainingBulletsText.text = GunManager.remainingBullets.ToString();
    }
}
