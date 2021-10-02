using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]

    [SerializeField]
    private GameObject obstacle;

    [SerializeField]
    private GameObject enemyShip;

    [Header("Lanes")]

    [SerializeField]
    private GameObject[] gameLanes;

    [Header("Spawn settings")]

    [SerializeField]
    private float minSpawnTime = 0.25f;

    [SerializeField]
    private float maxSpawnTime = 2f;

    private int currentDifficulty = 10;
    private float timeBetweenSpawns;

    private float obstacleMovementSpeed = 5f;

    private bool cooldownActive = false;

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

    Dictionary<int, Pattern> m_patterns;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseDifficulty();
        }
    }

    private IEnumerator SpawnObstacles()
    {
        int patternKey;
        if (currentDifficulty > 2)
        {
            patternKey = Random.Range(0, (30 / (currentDifficulty / 4)));
        }
        else
        {
            patternKey = Random.Range(0, 30);
        }
        Debug.Log(patternKey);
        yield return new WaitForSeconds(timeBetweenSpawns);
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
                        newObject.transform.position = new Vector3(gameLanes[i].transform.position.x, newObject.transform.position.y, 0);
                        break;
                    case 1:
                        newObject = Instantiate(enemyShip);
                        newObject.GetComponent<Obstacle_MoveScript>().moveSpeed = obstacleMovementSpeed;
                        newObject.transform.position = new Vector3(gameLanes[i].transform.position.x, newObject.transform.position.y, 0);
                        break;
                    default:
                        break;
                }
            }
        }
        cooldownActive = false;
    }

    public void IncreaseDifficulty()
    {
        if (currentDifficulty > 0)
        {
            Debug.Log("Difficulty increased!");
            currentDifficulty--;
            obstacleMovementSpeed += 1.25f;
        }
    }
}
