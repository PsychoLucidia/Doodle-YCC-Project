using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovementCC playerMovementCC;
    
    void Awake()
    {
        playerMovementCC = GetComponent<PlayerMovementCC>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        playerMovementCC.horizontal = Input.GetAxisRaw("Horizontal");
        playerMovementCC.vertical = Input.GetAxisRaw("Vertical");
    }
}
