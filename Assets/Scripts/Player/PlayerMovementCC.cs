using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCC : BaseMovement
{
    [Header("Public Components")]
    public CharacterController characterController;
    public Transform hurtboxTransform;

    // Non-serialized
    GameObject rootObj;

    void Awake()
    {
        rootObj = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    void Initialization()
    {
        hurtboxTransform = rootObj.transform.Find("Hurtbox");
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLogic();
    }

    public override void MoveLogic()
    {
        Vector3 direction = new Vector3(horizontal, vertical, 0);

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(hurtboxTransform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            hurtboxTransform.rotation = Quaternion.Euler(0f, 0f, smoothAngle);

            Vector3 moveDir = Quaternion.Euler(0f, 0f, targetAngle) * Vector3.right;

            characterController.Move(entitySpeed * Time.deltaTime * moveDir);
        }
    }
}
