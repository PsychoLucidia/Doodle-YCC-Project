using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovementCC playerMovementCC;
    [SerializeField] PlayerAttackHandler playerAttackHandler;
    
    void Awake()
    {
        playerMovementCC = GetComponent<PlayerMovementCC>();
        playerAttackHandler = GetComponent<PlayerAttackHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        playerMovementCC.horizontal = Input.GetAxisRaw("Horizontal");
        playerMovementCC.vertical = Input.GetAxisRaw("Vertical");
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAttackHandler.PlayerAttack();
        }
    }
}
