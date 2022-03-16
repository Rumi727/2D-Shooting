using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;
    [SerializeField] AudioClip reloadSound;

    public static Gun selectedGun => guns[selectedGunIndex];
    public static int selectedGunIndex = 0;

    public static long loadedBullets => selectedGun.currentLoadedBullets;
    public static long remainingBullets => selectedGun.currentRemainingBullets;

    [SerializeField] List<Gun> _guns = new List<Gun>();

    public static List<Gun> guns => instance._guns;

    void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        foreach (var item in guns)
        {
            item.currentLoadedBullets = (long)item.defaultLoadedBullets;
            item.currentRemainingBullets = (long)item.defaultRemainingBullets;
        }
    }

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && PlayerGun.delayTimer >= PlayerGun.maxDelayTimer)
        {
            selectedGunIndex += 1;

            if (selectedGunIndex >= guns.Count)
                selectedGunIndex = 0;

            int i = 0;
            while (!selectedGun.available)
            {
                if (i >= guns.Count)
                    return;

                selectedGunIndex += 1;

                if (selectedGunIndex >= guns.Count)
                    selectedGunIndex = 0;

                i++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && PlayerGun.delayTimer >= PlayerGun.maxDelayTimer)
            selectedGunIndex = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha1) && guns[1].available && PlayerGun.delayTimer >= PlayerGun.maxDelayTimer)
            selectedGunIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2) && guns[2].available && PlayerGun.delayTimer >= PlayerGun.maxDelayTimer)
            selectedGunIndex = 2;

        if (selectedGun.currentLevel < 0.25f)
            selectedGun.currentLevel = 0.25f;

        if (((Input.GetKeyDown(KeyCode.Q) && PlayerGun.delayTimer >= PlayerGun.maxDelayTimer && selectedGun.currentLoadedBullets != Mathf.Round(selectedGun.numberOfReloadBullets * selectedGun.currentLevel)) || selectedGun.currentLoadedBullets < 1) && selectedGun.currentRemainingBullets > 0)
        {
            selectedGun.currentRemainingBullets -= (long)(Mathf.Round(selectedGun.numberOfReloadBullets * selectedGun.currentLevel) - selectedGun.currentLoadedBullets);
            selectedGun.currentLoadedBullets = (long)Mathf.Round(selectedGun.numberOfReloadBullets * selectedGun.currentLevel);

            if (selectedGun.currentRemainingBullets < 0)
            {
                selectedGun.defaultLoadedBullets += selectedGun.currentRemainingBullets;
                selectedGun.currentRemainingBullets = 0;
            }

            PlayerGun.delayTimer = 0;

            if (Player.instance.hp < 0)
                PlayerGun.maxDelayTimer = 3 / selectedGun.currentLevel * 2;
            else
                PlayerGun.maxDelayTimer = 3 / selectedGun.currentLevel;

            GameManager.instance.audioSource.PlayOneShot(reloadSound);
        }
    }

    [System.Serializable]
    public class Gun
    {
        [System.NonSerialized] public long currentLoadedBullets = 0;
        [System.NonSerialized] public long currentRemainingBullets = 0;
        [System.NonSerialized] public float currentLevel = 1;

        public bool available = false;

        [Space]

        [Min(0)] public long defaultLoadedBullets = 6;
        [Min(0)] public long defaultRemainingBullets = 27;

        [Space]

        [Min(0)] public long numberOfReloadBullets = 6;
        [Min(0)] public long numberOfBulletsYouCanGet = 2;

        [Space]

        [Min(0)] public float delay = 1;

        [Space]

        [Min(0)] public float damage = 50;

        [Space]

        [Min(0)] public Vector2 size = new Vector2(5, 3);
        [Min(0)] public Vector2 offset = new Vector2(8, 3);

        [Space]

        public Sprite sprite;
        public Sprite uiSprite;
        public AudioClip audioClip;
    }
}