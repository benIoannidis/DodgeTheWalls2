using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to handle the movement and collision of instantiated enemies and meteors
/// </summary>
public class Obstacle_MoveScript : MonoBehaviour
{
    //reference to the appropriate explosion particle prefab
    [SerializeField]
    private GameObject explosionParticles;

    public float moveSpeed = 5f;

    //bool that is checked when instantiating explosion prefab, as well as whether to add score or not when destroyed
    public bool isEnemy = true;
    private bool canShoot = true;
    public int highestRandShootingNumber;

    [SerializeField]
    private GameObject m_bulletPrefab;

    public Game_ScoreManager scoreManager;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, (-1 * moveSpeed));

        if (isEnemy && canShoot)
        {
            canShoot = false;
            StartCoroutine("Shoot");
        }
        if (this.transform.position.y < 5)
        {
            canShoot = false;
        }
    }

    private IEnumerator Shoot()
    {
        //decide randomly between specified values (0 being shoot)
        int rand = Random.Range(0, highestRandShootingNumber);
        if (rand == 0)
        {
            //Shoot
            m_bulletPrefab.GetComponent<AudioSource>().mute = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;
            GameObject newBullet = Instantiate(m_bulletPrefab, this.transform.position, this.transform.rotation);
            newBullet.transform.parent = null;
            newBullet.GetComponent<Bullet_Manager>().Init(false);
        }
        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }

    //check for collision with player bullet trigger, and spawn death particles, and destroy this obstacle as well as bullet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //Player bullet layer
        {
            if (isEnemy)
            {
                scoreManager.AddScore();
            }
            explosionParticles.GetComponent<AudioSource>().mute = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;
            Instantiate(explosionParticles, this.transform.position, this.transform.rotation);
            Destroy(collision.gameObject);
            //Do explosion or some shit
        }
        else if (collision.gameObject.layer == 10)
        {
            if (!isEnemy)
            {
                explosionParticles.GetComponent<AudioSource>().mute = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;
                Instantiate(explosionParticles, this.transform.position, this.transform.rotation);
                Destroy(collision.gameObject);
            }
        }
        Destroy(this.gameObject);
    }
}
