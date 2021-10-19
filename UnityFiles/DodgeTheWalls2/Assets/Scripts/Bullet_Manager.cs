using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used for movement of bullets (both enemy and player bullets)
/// </summary>
public class Bullet_Manager : MonoBehaviour
{
    //this is set on instantiation (Init method below)
    private bool isPlayerBullet;
    private float moveSpeed = 10;

    //called when instantiating the bullet, informs which direction it will travel
    public void Init(bool isPlayerBullet)
    {
        this.isPlayerBullet = isPlayerBullet;
        //move enemy bullets faster (for a challenge)
        if (!isPlayerBullet)
        {
            moveSpeed = 20;
        }
    }

    private void Update()
    {
        //determine if it is a player or enemy bullet, and move in the appropriate direction
        int playerCheck = isPlayerBullet ? 1 : -1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerCheck * moveSpeed);

        //cull bullets that are out of view
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
