using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;
    public static float speed = 1;

    void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    protected override void FixedUpdate()
    {
        if (Time.timeScale <= 0)
            return;

        if (hp < 0)
            rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 5f * speed * 0.75f * 30f, Input.GetAxisRaw("Vertical") * 5f * speed * 30f);
        else
            rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 5f * speed * 30f, Input.GetAxisRaw("Vertical") * 5f * speed * 30f);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {

    }
}
