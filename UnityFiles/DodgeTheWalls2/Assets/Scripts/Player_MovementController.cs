using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This is the script to control player movement
/// </summary>

public class Player_MovementController : MonoBehaviour
{
    public GameObject[] gameLanes;

    private int currentLaneKey = 0;

    private BoxCollider2D m_collider;

    private float newXTarget = 0;

    private string direction = null;
    private bool movementAnimationComplete = true;

    [SerializeField]
    private Game_ScoreManager scoreManager;

    private void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();

        for (int i = 0; i < gameLanes.Length; i++)
        {
            if (gameLanes[i].transform.position.x == this.transform.position.x)
            {
                currentLaneKey = i;
                break;
            }
        }
    }

    private void Update()
    {
#if PLATFORM_ANDROID
        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            CheckMovement();
        }
#else
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            CheckMovement();
        }
#endif


        //REMOVE ONCE ANIMATIONS ARE IN
        switch (currentLaneKey)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, -50, 0);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, -25, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 25, 0);
                break;
            case 4:
                transform.rotation = Quaternion.Euler(0, 50, 0);
                break;
            default:
                break;
        }
    }

    private void CheckMovement()
    {
        if (direction == null)
        {
            if (LeftInput())
            {
                direction = "left";
                MoveLeft();
            }
            else if (RightInput())
            {
                direction = "right";
                MoveRight();
            }
        }
        else
        {
            if (direction == "left")
            {
                //don't run flip animation
                if (currentLaneKey != gameLanes.Length - 1)
                {
                    LeftAnimation();
                }
                else
                {
                    LeftFlipAnimation();
                }
            }
            else
            {
                //don't run flip animation
                if (currentLaneKey != 0)
                {
                    RightAnimation();
                }
                else
                {
                    RightFlipAnimation();
                }
            }
        }
    }

    private bool LeftInput()
    {
        int screenMidPoint = (Screen.width / 2);
        Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos.x > screenMidPoint)
            {
                return false;
            }
            return true;
        }
        if (Input.GetKeyDown("left") ||
            Input.GetKeyDown(KeyCode.A))
        {
            return true;
        }
        return false;
    }

    private bool RightInput()
    {
        int screenMidPoint = (Screen.width / 2);
        Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            if (mousePos.x < screenMidPoint)
            {
                return false;
            }
            return true;
        }
        if (Input.GetKeyDown("right") ||
            Input.GetKeyDown(KeyCode.D))
        {
            return true;
        }
        return false;
    }

    private void MoveLeft()
    {
        //m_collider.enabled = false;
        //not on left hand edge
        if (currentLaneKey > 0)
        {
            currentLaneKey--;
            movementAnimationComplete = false;
            newXTarget = gameLanes[currentLaneKey].transform.position.x;
            LeftAnimation();
        }
        else
        {
            currentLaneKey = gameLanes.Length - 1;
            movementAnimationComplete = false;
            newXTarget = gameLanes[currentLaneKey].transform.position.x;
            LeftFlipAnimation();
        }
    }

    private void MoveRight()
    {
        //not on right hand edge
        if (currentLaneKey < gameLanes.Length - 1)
        {
            currentLaneKey++;
            movementAnimationComplete = false;
            newXTarget = gameLanes[currentLaneKey].transform.position.x;
            RightAnimation();
        }
        else
        {
            currentLaneKey = 0;
            movementAnimationComplete = false;
            newXTarget = gameLanes[currentLaneKey].transform.position.x;
            RightFlipAnimation();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            scoreManager.AddScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7:
                scoreManager.PlayerDied();
                break;
            case 8:
                scoreManager.PlayerDied();
                break;
            case 10:
                scoreManager.PlayerDied();
                break;
            default:
                break;
        }
    }


    #region FIX LATER TO ADD ANIMATIONS
    //Movement animation functions
    private void LeftAnimation()
    {
        transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        direction = null;
    }

    private void LeftFlipAnimation()
    {
        transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        direction = null;
    }

    private void RightAnimation()
    {
        transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        direction = null;
    }

    private void RightFlipAnimation()
    {
        transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        direction = null;
    }
#endregion
}
