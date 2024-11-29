using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHandler : MonoBehaviour
{
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] Transform playerTransform;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPosition();
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
