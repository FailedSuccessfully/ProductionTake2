using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerComponent
{
    float maxSpeed => player.myStats.maxSpeed;
    float acceleration => player.myStats.acceleration;
    float deceleration => player.myStats.deceleration;
    public Vector2 myDirection {get; private set;}
    Vector2 velocity => rigidbody.velocity;
    

    public PlayerMovement(Player player) : base(player)
    {
        myDirection = Vector2.zero;
        ComponentAction += ChangeVelocity;
        ComponentAction += EnsureRotation;
    }
    public override void AcceptInput(InputAction.CallbackContext value){
        myDirection = value.ReadValue<Vector2>().normalized;
        if (myDirection.x != 0f){
            player.lastDirectionalInput = myDirection;
        }
    }

    void ChangeVelocity() {
        Vector2 force = Vector2.zero;

// if velocity is zero or dir times velocity is positive
// accelerate
// else
// decelerate
        //Debug.Log(myDirection);
        float dir = Math.Sign(myDirection.x);
        float vel = Math.Sign(velocity.x);

        //compare movement direction
        // if velocity matches direction / dir is zero or dir is not vel 
        if (vel == 0f || dir * vel > 0){
            //accelerate
            force += myDirection * acceleration;
        }
        else{
            //decelerate
            force += myDirection * deceleration;
        }

        // Scale to mass
        force *= rigidbody.mass;
        rigidbody.AddForce(force);
    }
    void EnsureRotation(){
        if (myDirection != Vector2.zero)
            transform.GetComponentInChildren<Animator>().transform.rotation = myDirection == Vector2.left ? Quaternion.AngleAxis(0, Vector3.up) : Quaternion.AngleAxis(180, Vector3.up);
    }

}