using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHandler : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] Transform playerTransform;

    private float timeOfShots;
    public float startTimeOfShots;

    public GameObject projectiles;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeOfShots = startTimeOfShots;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();

        if (timeOfShots <= 0)
        {
            Instantiate(projectiles, transform.position, Quaternion.identity);
            timeOfShots = startTimeOfShots;
        } else
        {
            timeOfShots -= Time.deltaTime;
        }
    }

    void GetPlayerPosition()
    {
        var playerPosition = playerTransform.position;
        var currentPosition = transform.position;
        var direction = (playerPosition - currentPosition).normalized;

        enemyMovement.horizontal = direction.x;
        enemyMovement.vertical = direction.y;
    }
}
