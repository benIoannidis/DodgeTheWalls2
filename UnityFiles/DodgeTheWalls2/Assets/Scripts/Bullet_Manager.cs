using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{
    private bool isPlayerBullet;
    private float moveSpeed = 10;
    public void Init(bool isPlayerBullet)
    {
        this.isPlayerBullet = isPlayerBullet;
    }

    private void Update()
    {
        int playerCheck = isPlayerBullet ? 1 : -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerCheck * moveSpeed);

        if (transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }
    }
}
