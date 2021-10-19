using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is the script to control player movement
/// </summary>
public class Player_MovementController : MonoBehaviour
{
    //reference to game lane objects
    public GameObject[] gameLanes;

    //to keep track of current lane player is in
    private int currentLaneKey = 0;

    //reference to the games score manager script
    [SerializeField]
    private Game_ScoreManager scoreManager;

    private void Start()
    {
        //find which lane the player is in
        bool inLane = false;

        for (int i = 0; i < gameLanes.Length; i++)
        {
            if (gameLanes[i].transform.position.x == this.transform.position.x)
            {
                currentLaneKey = i;
                inLane = true;
                break;
            }
        }
        
        //if player is not suitably inside a lane, move the player to the centre-most lane
        if (!inLane)
        {
            int middleLane = gameLanes.Length / 2;
            currentLaneKey = middleLane;
            transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        //ensure on both platforms that the click/tap is not over the top of a UI element
        #region Check input
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
        #endregion

        //Tilt player toward camera
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

    //called in update
    private void CheckMovement()
    {
        if (LeftInput())
        {
            MoveLeft();
        }
        else if (RightInput())
        {
            MoveRight();
        }
    }

    //return true if click/tap on left half of the screen
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

    //return true if click/tap on right half of the screen
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

    //check if on the edge lane or not, and move accordingly
    private void MoveLeft()
    {
        //not on left hand edge
        if (currentLaneKey > 0)
        {
            currentLaneKey--;
            transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            currentLaneKey = gameLanes.Length - 1;
            transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        }
    }

    //check if on the edge lane or not, and move accordingly
    private void MoveRight()
    {
        //not on right hand edge
        if (currentLaneKey < gameLanes.Length - 1)
        {
            currentLaneKey++;
            transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            currentLaneKey = 0;
            transform.position = new Vector3(gameLanes[currentLaneKey].transform.position.x, transform.position.y, transform.position.z);
        }
    }

    //check for collision with score object trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            scoreManager.AddScore();
        }
    }

    //check for collision with enemy ships and asteroids
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
            default:
                break;
        }
    }

    //check for collision with enemy bullet triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            scoreManager.PlayerDied();
        }
    }
}
