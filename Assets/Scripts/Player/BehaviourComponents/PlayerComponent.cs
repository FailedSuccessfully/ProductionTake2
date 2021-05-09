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
    /// <summary>
    /// A delegate that will be called by the player whenever the component needs to invoke
    /// the component will define function calls to the delegate as needed
    /// </summary>
    /// <value></value>
    public virtual Action ComponentAction {  get; protected set; }

    /// <summary>
    /// The component must define the way it accepts input from unity InputSystem
    /// </summary>
    /// <param name="value"> Context from input system call </param>
    public abstract void AcceptInput(InputAction.CallbackContext value);
    
    
}
