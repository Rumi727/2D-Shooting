using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Bullet bullet;
    [SerializeField] GameObject reload;
    [SerializeField] Transform reloadSlider;

    public static float maxDelayTimer = 1;
    public static float delayTimer = 1;
    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        spriteRenderer.sprite = GunManager.selectedGun.sprite;

        Vector3 dir = Player.instance.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;

        transform.localPosition = new Vector3(Mathf.Sin(-(angle + 90) * Mathf.Deg2Rad) * -33, Mathf.Cos(-(angle + 90) * Mathf.Deg2Rad) * -33, 0);
        transform.eulerAngles = new Vector3(0, 0, angle);

        spriteRenderer.flipY = transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270;

        if (delayTimer < maxDelayTimer)
            delayTimer += Time.deltaTime;
        else
            delayTimer = maxDelayTimer;

        if (reload.activeSelf != delayTimer < maxDelayTimer)
            reload.SetActive(delayTimer < maxDelayTimer);

        reloadSlider.localScale = new Vector3(delayTimer / maxDelayTimer, 1, 1);

        if (Input.GetMouseButton(0) && delayTimer == maxDelayTimer && GunManager.selectedGun.currentLoadedBullets > 0)
        {
            Bullet bullet = Instantiate(this.bullet);
            bullet.transform.position = Player.instance.transform.position;
            bullet.transform.eulerAngles = new Vector3(0, 0, angle);

            if (transform.eulerAngles.z >= 90 && transform.eulerAngles.z <= 270)
                bullet.transform.position += (bullet.transform.right * (33 + GunManager.selectedGun.offset.x)) + (bullet.transform.up * -GunManager.selectedGun.offset.y);
            else
                bullet.transform.position += (bullet.transform.right * (33 + GunManager.selectedGun.offset.x)) + (bullet.transform.up * GunManager.selectedGun.offset.y);

            bullet.spriteRenderer.size = GunManager.selectedGun.size;
            bullet.collider.size = GunManager.selectedGun.size;

            if (Player.instance.hp < 0)
                maxDelayTimer = GunManager.selectedGun.delay * 2 / GunManager.selectedGun.currentLevel;
            else
                maxDelayTimer = GunManager.selectedGun.delay / GunManager.selectedGun.currentLevel;

            delayTimer = 0;

            GunManager.selectedGun.currentLoadedBullets--;
            GameManager.instance.audioSource.PlayOneShot(GunManager.selectedGun.audioClip);
        }
    }
}
