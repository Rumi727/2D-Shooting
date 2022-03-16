using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] new protected Rigidbody2D rigidbody;

    public float maxHP = 100;
    public float hp = 100;

    protected virtual void FixedUpdate()
    {
        if (Time.timeScale <= 0)
            return;

        Vector2 dir = transform.position - Player.instance.transform.position;
        dir.Normalize();
        rigidbody.velocity = new Vector2(-3 * dir.x * 30, -3 * dir.y * 30);

        if (hp <= 0)
        {
            GameManager.score += 100;
            GameManager.remainingZombies--;
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.timeScale <= 0)
            return;

        if (collision.gameObject == Player.instance.gameObject)
        {
            Player.instance.hp -= GameManager.difficulty;
            GameManager.score -= 1;
        }
    }
}
