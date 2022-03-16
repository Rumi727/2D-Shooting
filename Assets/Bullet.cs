using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    new public BoxCollider2D collider;
    [SerializeField] new Rigidbody2D rigidbody;

    bool damageLock = false;
    void Update()
    {
        if (Time.timeScale <= 0)
            return;

        rigidbody.velocity = transform.right * 50 * 30;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (damageLock)
            return;

        damageLock = true;

        if (other.gameObject != Player.instance.gameObject)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                other.gameObject.GetComponent<Entity>().hp -= GunManager.selectedGun.damage * GunManager.selectedGun.currentLevel;
                GameManager.score += 10;
            }

            Destroy(gameObject);
        }
    }
}
