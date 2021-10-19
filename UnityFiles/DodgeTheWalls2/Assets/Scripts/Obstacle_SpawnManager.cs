using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script to handle instantiation of obstacle objects (enemies and meteors)
/// </summary>
public class Obstacle_SpawnManager : MonoBehaviour
{
    //prefabs to instantiate
    [Header("Prefabs")]

    [SerializeField]
    private GameObject obstacle;

    [SerializeField]
    private GameObject enemyShip;

    [SerializeField]
    private GameObject scoreObject;

    [Header("Lanes")]
    //reference to game lane objects
    [SerializeField]
    private GameObject[] gameLanes;

    //exposed spawn settings
    [Header("Spawn settings")]

    [SerializeField]
    private float minSpawnTime = 0.25f;

    [SerializeField]
    private float maxSpawnTime = 2f;

    //determines spawn delay, as well as obstacle movement speed on instantiation
    private int currentDifficulty = 10;
    public float timeBetweenSpawns;

    private float obstacleMovementSpeed = 5f;

    private bool cooldownActive = false;

    [Header("Score")]

    [SerializeField]
    private Game_ScoreManager scoreManager;

    //struct to hold patterns
    struct Pattern
    {
        public Pattern(bool b0, bool b1, bool b2, bool b3, bool b4)
        {
            positions = new bool[5];
            positions[0] = b0;
            positions[1] = b1;
            positions[2] = b2;
            positions[3] = b3;
            positions[4] = b4;
        }

        public bool[] positions;
    }

    //list of patterns to call at random
    Dictionary<int, Pattern> m_patterns;

    private bool canIncreaseDif = true;

    private void Start()
    {
        m_patterns = new Dictionary<int, Pattern>();

        #region Create and assign patterns
        #region Single obstacle patterns
        m_patterns[0] = new Pattern(true, false, false, false, false);
        m_patterns[1] = new Pattern(false, true, false, false, false);
        m_patterns[2] = new Pattern(false, false, true, false, false);
        m_patterns[3] = new Pattern(false, false, false, true, false);
        m_patterns[4] = new Pattern(false, false, false, false, true);
        #endregion
        #region Double obstacle patterns
        m_patterns[5] = new Pattern(true, true, false, false, false);
        m_patterns[6] = new Pattern(false, true, true, false, false);
        m_patterns[7] = new Pattern(false, false, true, true, false);
        m_patterns[8] = new Pattern(false, false, false, true, true);
        m_patterns[9] = new Pattern(true, false, false, false, true);
        m_patterns[10] = new Pattern(true, false, false, true, false);
        m_patterns[11] = new Pattern(true, false, true, false, false);
        m_patterns[12] = new Pattern(false, true, false, false, true);
        m_patterns[13] = new Pattern(false, false, true, false, true);
        #endregion
        #region Triple obstacle patterns
        m_patterns[14] = new Pattern(true, true, true, false, false);
        m_patterns[15] = new Pattern(false, true, true, true, false);
        m_patterns[16] = new Pattern(false, false, true, true, true);
        m_patterns[17] = new Pattern(true, false, false, true, true);
        m_patterns[18] = new Pattern(true, false, true, false, true);
        m_patterns[19] = new Pattern(true, true, false, false, true);

        m_patterns[20] = new Pattern(true, true, false, true, false);
        m_patterns[21] = new Pattern(false, true, false, true, true);
        m_patterns[22] = new Pattern(true, false, true, true, false);
        m_patterns[23] = new Pattern(false, true, true, false, true);
        #endregion
        #region Quad obstacle patterns
        m_patterns[24] = new Pattern(true, true, true, true, false);
        m_patterns[25] = new Pattern(false, true, true, true, true);
        m_patterns[26] = new Pattern(true, false, true, true, true);
        m_patterns[27] = new Pattern(true, true, false, true, true);
        m_patterns[28] = new Pattern(true, true, true, false, true);
        #endregion
        #region Quint obstacle pattern
        m_patterns[29] = new Pattern(true, true, true, true, true);
        #endregion
        #endregion

        timeBetweenSpawns = maxSpawnTime;
    }

    private void Update()
    {
        if (!cooldownActive)
        {
            cooldownActive = true;
            StartCoroutine("SpawnObstacles");
        }

        //Intervals to increase difficulty
        switch (scoreManager.m_score)
        {
            case 10: //difficulty 9
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 11:
                canIncreaseDif = true;
                break;

            case 20: //difficulty 8
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 21:
                canIncreaseDif = true;
                break;

            case 30: //difficulty 7
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 31:
                canIncreaseDif = true;
                break;

            case 50: //difficulty 6
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 51:
                canIncreaseDif = true;
                break;

            case 75: //difficulty 5
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 76:
                canIncreaseDif = true;
                break;

            case 100: //difficulty 4
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 101:
                canIncreaseDif = true;
                break;

            case 130: //difficulty 3
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 131:
                canIncreaseDif = true;
                break;

            case 160: //difficulty 2
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 161:
                canIncreaseDif = true;
                break;

            case 200: //difficulty 1
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;
            case 201:
                canIncreaseDif = true;
                break;

            case 250: //difficulty 0
                if (canIncreaseDif)
                {
                    canIncreaseDif = false;
                    IncreaseDifficulty();
                }
                break;

            default:
                break;
        }
    }

    //check difficulty, and spawn the appropriate number of obstacles, at the current difficulties movement speed
    private IEnumerator SpawnObstacles()
    {
        int patternKey;
        if (currentDifficulty > 5)
        {
            int rand = Random.Range(0, 4);
            if (rand < 3)
            {
                patternKey = Random.Range(0, 24);
            }
            else
            {
                patternKey = Random.Range(14, 29);
            }
        }
        else if (currentDifficulty > 2)
        {
            int rand = Random.Range(0, 4);
            if (rand < 2)
            {
                patternKey = Random.Range(0, 24);
            }
            else
            {
                patternKey = Random.Range(14, 30);
            }
        }
        else
        {
            int rand = Random.Range(0, 4);
            if (rand == 0)
            {
                patternKey = Random.Range(0, 24);
            }
            else
            {
                patternKey = Random.Range(14, 30);
            }
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
        
        //decide whether to spawn a meteor or enemy ship
        for (int i = 0; i < 5; i++)
        {
            if (m_patterns[patternKey].positions[i])
            {
                GameObject newObject;
                int rand = Random.Range(0, 2);

                switch (rand)
                {
                    case 0:
                        newObject = Instantiate(obstacle);
                        newObject.GetComponent<Obstacle_MoveScript>().moveSpeed = obstacleMovementSpeed;
                        newObject.GetComponent<Obstacle_MoveScript>().scoreManager = scoreManager;
                        newObject.transform.position = new Vector3(gameLanes[i].transform.position.x, this.transform.position.y, 0);
                        break;
                    case 1:
                        newObject = Instantiate(enemyShip);
                        newObject.GetComponent<Obstacle_MoveScript>().moveSpeed = obstacleMovementSpeed;
                        newObject.GetComponent<Obstacle_MoveScript>().scoreManager = scoreManager;
                        switch (currentDifficulty)
                        {
                            case 10:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 20;
                                break;
                            case 9:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 15;
                                break;
                            case 8:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 15;
                                break;
                            case 7:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 15;
                                break;
                            case 6:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 10;
                                break;
                            case 5:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 10;
                                break;
                            case 4:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 10;
                                break;
                            case 3:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 5;
                                break;
                            case 2:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 5;
                                break;
                            case 1:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 5;
                                break;
                            case 0:
                                newObject.GetComponent<Obstacle_MoveScript>().highestRandShootingNumber = 3;
                                break;
                        }
                        newObject.transform.position = new Vector3(gameLanes[i].transform.position.x, this.transform.position.y, 0);
                        switch (i)
                        {
                            case 0:
                                newObject.transform.rotation = Quaternion.Euler(0, -30, 0);
                                break;
                            case 1:
                                newObject.transform.rotation = Quaternion.Euler(0, -15, 0);
                                break;
                            case 2:
                                newObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;
                            case 3:
                                newObject.transform.rotation = Quaternion.Euler(0, 15, 0);
                                break;
                            case 4:
                                newObject.transform.rotation = Quaternion.Euler(0, 30, 0);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        GameObject newScoreObject = Instantiate(scoreObject);
        newScoreObject.GetComponent<Obstacle_MoveScript>().moveSpeed = obstacleMovementSpeed;
        newScoreObject.transform.position = new Vector3(gameLanes[2].transform.position.x, this.transform.position.y, 0);
        cooldownActive = false;
    }

    //increase the difficulty (increase movement speed, and decrease time between obstacle spawns)
    public void IncreaseDifficulty()
    {
        if (currentDifficulty > 0)
        {
            Debug.Log("Difficulty increased!");
            currentDifficulty--;
            Debug.Log("Down to level: " + currentDifficulty);
            obstacleMovementSpeed += 1.25f;
            timeBetweenSpawns -= 0.15f;
        }
    }
}
