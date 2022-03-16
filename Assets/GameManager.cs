using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource audioSource;

    public static float fpsDeltaTime => Time.deltaTime * 30;

    public static long score = 0;

    public static ulong round = 0;
    public static ulong remainingZombies = 0;

    public Image vignetting;
    public CanvasGroup roundCanvasGroup;
    public TMP_Text roundText;
    public AudioClip roundAudio;
    public GameObject gameOver;
    public TMP_Text gameOverScoreText;
    public GameObject pause;
    static float roundAlphaDelay = 0;

    public static float difficulty = 1;

    public static void NextRound()
    {
        round++;

        instance.roundText.text = round.ToString();
        remainingZombies = round * 10;
        Map.createdZombie = 0;

        instance.audioSource.PlayOneShot(instance.roundAudio);

        instance.roundCanvasGroup.alpha = 1;
        roundAlphaDelay = 5;

        difficulty = round * 0.1f + 0.9f;

        instance.gameOver.SetActive(false);
    }

    public static void Restart()
    {
        Time.timeScale = 1;

        instance.gameOver.SetActive(false);
        instance.pause.SetActive(false);

        score = 0;
        difficulty = 0;
        round = 0;
        remainingZombies = 0;

        Player.instance.transform.position = Vector3.zero;

        Transform[] gameObjects = Map.instance.GetComponentsInChildren<Transform>();
        for (int i = 1; i < gameObjects.Length; i++)
            Destroy(gameObjects[i].gameObject);

        Map.createdItem = 0;
        Map.createdZombie = 0;

        for (int i = 0; i < GunManager.guns.Count; i++)
        {
            GunManager.Gun item = GunManager.guns[i];
            item.currentLoadedBullets = item.defaultLoadedBullets;
            item.currentRemainingBullets = item.defaultRemainingBullets;
            item.currentLevel = 1;

            if (i > 0)
                item.available = false;
        }

        Player.speed = 1;
        Player.instance.maxHP = 100;
        Player.instance.hp = 100;

        PlayerGun.maxDelayTimer = 1;
        PlayerGun.delayTimer = 1;

        NextRound();
    }

    public static void GameOver()
    {
        Time.timeScale = 0;
        instance.gameOver.SetActive(true);
        instance.gameOverScoreText.text = "최종 점수 : " + score;
    }

    void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        NextRound();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (Screen.fullScreen)
                Screen.SetResolution(480, 360, false);
            else
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }

        if (roundAlphaDelay <= 0)
            roundCanvasGroup.alpha -= 0.01f * fpsDeltaTime;
        else
            roundAlphaDelay -= Time.deltaTime;

        if (remainingZombies <= 0)
            NextRound();

        vignetting.color = new Color(1, 1, 1, 1 - (Player.instance.hp / Player.instance.maxHP * 2));

        if (Player.instance.hp < -Player.instance.maxHP * 0.5f)
            GameOver();
        else if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = Time.timeScale <= 0 ? 1 : 0;
            instance.pause.SetActive(!instance.pause.activeSelf);
        }

    }
}
