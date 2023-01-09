using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public enum MovementType
{
    Smooth,
    SmoothSnapTurn,
    Teleport
}

public class Locomotion : MonoBehaviour
{
    private CharacterController CC;
    private CapsuleCollider CCollider;
    
    public float moveSpeed, rotateSpeed; 
    public MovementType movementType;
    
    public InputAction rightHandJoystick, leftHandJoystick;

    public Transform head;
    private void Start()
    {
        rightHandJoystick.Enable();
        leftHandJoystick.Enable();
        
        CCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        Movement();
        Rotation();
        SolveCC();
    }

    void Movement()
    {
        Vector3 forward = Vector3.ProjectOnPlane(head.forward, Vector3.up);
        Vector3 dir = (forward + new Vector3(leftHandJoystick.ReadValue<Vector2>().x, 0, leftHandJoystick.ReadValue<Vector2>().y).normalized * moveSpeed) * Time.fixedDeltaTime;
        
        transform.Translate(Vector3.ProjectOnPlane(head.TransformDirection(dir), Vector3.up));
    }

    void Rotation()
    {
        transform.RotateAround(head.position,Vector3.up,rightHandJoystick.ReadValue<Vector2>().x * rotateSpeed);
    }

    void SolveCC()
    {
        float height = head.position.y - transform.position.y;
        
        CCollider.center = new Vector3(head.position.x, height / 2, head.position.z) - transform.position;
    }
}
