using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int index = 0;
    public bool hpRadar = false;
    public bool bulletRadar = false;
    public bool levelRadar = false;
    public Vector2 pos = Vector2.zero;

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        transform.position = pos;

        if (((hpRadar || Player.instance.hp < 0) && index == 0) || (bulletRadar && (index == 1 || index == 2 || index == 3)) || (levelRadar && (index == 4 || index == 5 || index == 6 || index == 10 || index == 11)))
        {
            Vector2 cameraPos = Camera.main.transform.position;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, (-Screen.width * 0.5f * MainCamera.cameraZoom) + 15 + cameraPos.x, (Screen.width * 0.5f * MainCamera.cameraZoom) - 15 + cameraPos.x), Mathf.Clamp(transform.position.y, (-Screen.height * 0.5f * MainCamera.cameraZoom) + 15 + cameraPos.y, (Screen.height * 0.5f * MainCamera.cameraZoom) - 15 + cameraPos.y));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.instance.gameObject)
        {
            switch (index)
            {
                case 0:
                    Player.instance.hp += 25 * (Player.instance.maxHP / 100);
                    if (Player.instance.hp > Player.instance.maxHP)
                        Player.instance.hp = Player.instance.maxHP;
                    break;
                case 1:
                    GunManager.guns[0].currentRemainingBullets += (long)Mathf.Floor(GunManager.guns[0].numberOfBulletsYouCanGet * GameManager.difficulty * (GameManager.round * 10) * 1.5f);
                    break;
                case 2:
                    GunManager.guns[1].available = true;
                    GunManager.guns[1].currentRemainingBullets += (long)Mathf.Floor(GunManager.guns[1].numberOfBulletsYouCanGet * GameManager.difficulty * (GameManager.round * 10) * 1.5f);
                    break;
                case 3:
                    GunManager.guns[2].available = true;
                    GunManager.guns[2].currentRemainingBullets += (long)Mathf.Floor(GunManager.guns[2].numberOfBulletsYouCanGet * GameManager.difficulty * (GameManager.round * 10) * 1.5f);
                    break;
                case 4:
                    GunManager.guns[0].currentLevel += 0.25f;
                    break;
                case 5:
                    GunManager.guns[1].currentLevel += 0.25f;
                    break;
                case 6:
                    GunManager.guns[2].currentLevel += 0.25f;
                    break;
                case 7:
                    GunManager.guns[0].currentLevel -= 0.25f;
                    break;
                case 8:
                    GunManager.guns[1].currentLevel -= 0.25f;
                    break;
                case 9:
                    GunManager.guns[2].currentLevel -= 0.25f;
                    break;
                case 10:
                    Player.instance.maxHP += 50;
                    Player.instance.hp += 50;
                    break;
                case 11:
                    Player.speed += 0.02f;
                    break;
                case 12:
                    Item[] items = FindObjectsOfType<Item>();
                    foreach (var item in items)
                        item.hpRadar = true;
                    break;
                case 13:
                    items = FindObjectsOfType<Item>();
                    foreach (var item in items)
                        item.bulletRadar = true;
                    break;
                case 14:
                    items = FindObjectsOfType<Item>();
                    foreach (var item in items)
                        item.levelRadar = true;
                    break;
            }

            Map.createdItem--;
            Destroy(gameObject);
        }
    }
}
