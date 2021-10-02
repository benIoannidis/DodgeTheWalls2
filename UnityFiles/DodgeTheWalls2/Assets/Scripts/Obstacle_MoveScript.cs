using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-1 * moveSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
