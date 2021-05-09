using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerComponent : BehaviourComponent
{
    

    protected PlayerComponent(Player player) : base(player.gameObject)
    {
        this.player = player;
        this.rigidbody = player.GetComponent<Rigidbody2D>();
    }

    protected Player player;
    protected Rigidbody2D rigidbody;
    public virtual Action ComponentAction {  get; protected set; }

    public abstract void AcceptInput(InputAction.CallbackContext value);
    
    
}
