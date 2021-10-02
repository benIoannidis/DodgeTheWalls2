using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the script to control player movement
/// </summary>

public class Player_MovementController : MonoBehaviour
{
    private GameObject[] gameLanes;

    private int currentLaneKey = 0;

    private BoxCollider2D m_collider;

    private float newXTarget = 0;

    private string direction = null;
    private bool movementAnimationComplete = true;

    private void Start()
    {
        gameLanes = GameObject.Find("LaneManager").GetComponent<LaneManager>().GetLanes();
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
        m_collider.enabled = false;
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


    //Movement animation functions
    private void LeftAnimation()
    {
        
    }

    private void LeftFlipAnimation()
    {

    }

    private void RightAnimation()
    {

    }

    private void RightFlipAnimation()
    {

    }
}
