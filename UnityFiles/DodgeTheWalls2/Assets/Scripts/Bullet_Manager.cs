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
        if (!isPlayerBullet)
        {
            moveSpeed = 20;
        }
    }

    private void Update()
    {
        int playerCheck = isPlayerBullet ? 1 : -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerCheck * moveSpeed);

        if (isPlayerBullet && transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }

        else if (!isPlayerBullet && transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
