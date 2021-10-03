using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_MoveScript : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionParticles;

    public float moveSpeed = 5f;

    public bool isEnemy = true;

    public Game_ScoreManager scoreManager;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-1 * moveSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //Player bullet layer
        {
            if (isEnemy)
            {
                scoreManager.AddScore();
            }
            Instantiate(explosionParticles, this.transform.position, this.transform.rotation);
            Destroy(collision.gameObject);
            //Do explosion or some shit
        }
        Destroy(this.gameObject);
    }
}
