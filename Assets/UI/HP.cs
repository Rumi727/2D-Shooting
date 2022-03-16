using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HP : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        transform.localScale = new Vector3(entity.hp / entity.maxHP, 1, 1);

        if (transform.localScale.x >= 0)
            spriteRenderer.color = Color.red;
        else
            spriteRenderer.color = new Color(0.6313726f, 0, 0);
    }
}
