using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] AudioClip restart;
    [SerializeField] AudioClip restart2;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Slider restartSlider;
    static float timer = 0;
    static float timer2 = 0;
    bool keyLock = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (!keyLock)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(0, timer * 20), 0.5f * Time.unscaledDeltaTime * 30);
                timer += Time.unscaledDeltaTime;
                timer2 += Time.unscaledDeltaTime;
            }
            else
            {
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(0, -45), 0.5f * Time.unscaledDeltaTime * 30);
                timer = 0;
                timer2 = 0;
            }
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(0, -45), 0.5f * Time.unscaledDeltaTime * 30);
            timer = 0;
            timer2 = 0;

            keyLock = false;
        }

        if (restartSlider.slider.value == 1 || Player.instance.hp < (-Player.instance.maxHP * 0.5f))
        {
            if (timer2 > 0.3f / 4)
            {
                timer2 = 0;
                GameManager.instance.audioSource.PlayOneShot(restart);
            }
            else if (timer > 0.3f)
            {
                keyLock = true;

                GameManager.instance.audioSource.Stop();
                GameManager.Restart();
                GameManager.instance.audioSource.PlayOneShot(restart2);
            }
        }
        else
        {
            if (timer2 > 1.2f / 4)
            {
                timer2 = 0;
                GameManager.instance.audioSource.PlayOneShot(restart);
            }
            else if (timer > 1.2f)
            {
                keyLock = true;

                GameManager.instance.audioSource.Stop();
                GameManager.Restart();
                GameManager.instance.audioSource.PlayOneShot(restart2);
            }
        }
    }
}
