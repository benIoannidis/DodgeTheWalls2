using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ShootingController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float cooldownTime = 1;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject rechargeBar;

    [SerializeField]
    private GameObject spriteMask;

    private Vector2 fullPos;
    private float startYPos;
    private float posDif;
    private bool waitingForBar = false;

    public bool coolingDown = false;

    private void Start()
    {
        fullPos = rechargeBar.GetComponent<RectTransform>().position;
        startYPos = fullPos.y - 2f;
        posDif = fullPos.y;

        rechargeBar.GetComponent<RectTransform>().position = new Vector2(fullPos.x, startYPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (coolingDown && !waitingForBar)
        {
            //rechargeBar.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            canvas.transform.rotation = Quaternion.Euler(0, 0, 0);
            waitingForBar = true;
            StartCoroutine("AddToProgress");
        }
        else if (!coolingDown)
        {
            //rechargeBar.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            canvas.transform.rotation = Quaternion.Euler(0, 0, 0);
            rechargeBar.GetComponent<RectTransform>().position = new Vector2(this.transform.position.x + 0.8f, startYPos);
        }
    }

    private IEnumerator AddToProgress()
    {
        float newYPos = rechargeBar.GetComponent<RectTransform>().position.y + 0.272f;
        rechargeBar.GetComponent<RectTransform>().position = new Vector2(this.transform.position.x + 0.8f, newYPos);
        yield return new WaitForSeconds(0.16f);
        waitingForBar = false;
    }

    public void Shoot()
    {
        if (!coolingDown)
        {
            coolingDown = true;
            rechargeBar.GetComponent<RectTransform>().position = new Vector2(this.transform.position.x + 0.8f, startYPos + 0.3f);
            StartCoroutine("InstantiateBullet");
        }
    }

    private IEnumerator InstantiateBullet()
    {
        bulletPrefab.GetComponent<AudioSource>().mute = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;
        GameObject newBullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        newBullet.transform.parent = null;
        newBullet.GetComponent<Bullet_Manager>().Init(true);
        yield return new WaitForSeconds(cooldownTime);
        coolingDown = false;
    }
}
