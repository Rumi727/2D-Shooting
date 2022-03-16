using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Transform instance;

    [SerializeField] Entity zombie;
    [SerializeField] Item itemObject;
    [SerializeField] List<Sprite> item;
    [SerializeField] List<int> itemPercentage;

    void OnEnable()
    {
        if (instance == null)
            instance = transform;
        else
            Destroy(gameObject);
    }

    float zombieTimer = 0;
    public static float createdZombie = 0;
    public static int createdItem = 0;
    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        if (zombieTimer <= 0 && createdZombie < (GameManager.round * 10))
        {
            Entity entity = Instantiate(zombie, transform);
            entity.maxHP = 100 * GameManager.difficulty;
            entity.hp = 100 * GameManager.difficulty;


            float x;
            if (Random.Range(0, 2) == 0)
                x = Random.Range((-Screen.width * MainCamera.cameraZoom) - 100, -Screen.width * MainCamera.cameraZoom) + Player.instance.transform.position.x;
            else
                x = Random.Range((Screen.width * MainCamera.cameraZoom) - 100, Screen.width * MainCamera.cameraZoom) + Player.instance.transform.position.x;

            float y;
            if (Random.Range(0, 2) == 0)
                y = Random.Range((-Screen.height * MainCamera.cameraZoom) - 100, -Screen.height * MainCamera.cameraZoom) + Player.instance.transform.position.y;
            else
                y = Random.Range((Screen.height * MainCamera.cameraZoom) - 100, Screen.height * MainCamera.cameraZoom) + Player.instance.transform.position.y;

            x = Mathf.Clamp(x, -2240, 2237);
            y = Mathf.Clamp(y, -1774, 1836);

            entity.transform.position = new Vector3(x, y);

            zombieTimer = Random.Range(1 / GameManager.difficulty, 5 / GameManager.difficulty);
            createdZombie++;
        }
        else
            zombieTimer -= Time.deltaTime;

        while (createdItem <= 10)
        {
            Item item = Instantiate(itemObject, transform);
            int index = itemPercentage[Random.Range(0, itemPercentage.Count)];
            item.spriteRenderer.sprite = this.item[index];
            item.index = index;
            item.pos = new Vector2(Random.Range(-2240f, 2237f), Random.Range(-1774f, 1836f));

            createdItem++;
        }
    }
}
