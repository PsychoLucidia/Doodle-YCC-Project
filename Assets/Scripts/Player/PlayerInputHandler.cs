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
        HandlePauseMenu();

        if (GameManager.Instance.pauseState == PauseState.Paused) { return; }

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
            if (playerAttackHandler.playerAttackState == PlayerAttackState.Attacking) { return; }
            playerAttackHandler.PlayerAttack();
        }
    }

    void HandlePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.pauseState == PauseState.Unpaused && !GameManager.Instance.buffPanelOpen)
            {
                GameManager.Instance.PauseGame(PauseState.Paused);
            }
            else if (GameManager.Instance.pauseState == PauseState.Paused && !GameManager.Instance.buffPanelOpen)
            {
                GameManager.Instance.PauseGame(PauseState.Unpaused);
            }
        }
    }
}
