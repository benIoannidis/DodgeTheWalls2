using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float cooldownTime = 1;

    private bool coolingDown = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!coolingDown)
        {
            coolingDown = true;
            StartCoroutine("InstantiateBullet");
        }
    }

    private IEnumerator InstantiateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        newBullet.transform.parent = null;
        newBullet.GetComponent<Bullet_Manager>().Init(true);
        yield return new WaitForSeconds(cooldownTime);
        coolingDown = false;
    }
}
