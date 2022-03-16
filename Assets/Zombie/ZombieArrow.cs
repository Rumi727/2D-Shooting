using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ZombieArrow : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        transform.localPosition = Vector3.zero;

        Vector2 cameraPos = Camera.main.transform.position;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, (-Screen.width * 0.5f * MainCamera.cameraZoom) + 18 + cameraPos.x, (Screen.width * 0.5f * MainCamera.cameraZoom) - 18 + cameraPos.x), Mathf.Clamp(transform.position.y, (-Screen.height * 0.5f * MainCamera.cameraZoom) + 18 + cameraPos.y, (Screen.height * 0.5f * MainCamera.cameraZoom) - 18 + cameraPos.y));

        Vector3 dir = transform.position - entity.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 180);

        spriteRenderer.enabled = Vector3.Distance(transform.position, entity.transform.position) >= 30;
    }
}
