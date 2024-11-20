using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    [SerializeField] protected float entitySpeed = 5f;
    public Vector3 entityDirection;

    protected float turnSmoothVelocity;
    [SerializeField] protected float turnSmoothTime = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void MoveLogic();
}
