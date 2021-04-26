using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Fields
    #region ItayFields
    #endregion
    #region UrsulaFields
    public PelletController PelletPrefab;
    public Rigidbody2D rb;
    #endregion
    #endregion

    #region Unity Callbacks



    #endregion
    #region  Actions

    #region  ItayActions
    public void OnMove(){  Debug.Log("Move"); //WASD
        //TODO: Apply movement - Start on first callback stop on second callback
        //TODO: Adjuststable Accelaration
        //TODO: Max speed
        //TODO: Adjustable Decelaration
        //TODO: Limit slope height for climb
        //TODO: Stick to vertical wall
    
    }
    public void OnJump(){Debug.Log("Jump"); //Space
        //TODO: Apply ascension
        //TODO: Apply forward momentum 
        //TODO: Adjustable Airtime
        //TODO: Adjustable Air Movment
        //TODO: Adjustable Jump Number and check limit
    }
    public void OnDash(){Debug.Log("Dash"); //Left Shift
        //TODO: Figure direction of Dash (right and left only for now)
        //TODO: If hanging from wall dash will go up
        //TODO: Adjustable dash time and speed
        //TODO: Limit Midair Dashes
    }
    #endregion
    #region UrsulaFields
    public void OnShoot(){Debug.Log("Shoot"); //Left Mouse Button

        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 dir = MousePos - (Vector2)transform.position;

        PelletController pellet = Instantiate(PelletPrefab);
        pellet.transform.position = transform.position;

        pellet.movement = dir.normalized;

        //TODO: Locate mouse and shoot in direction
        //TODO: Instantiate and Destantiate Shot
        //TODO: Adjustable Shot Speed
        //TODO: Shot should be self propelling
    }
    #endregion

    public void OnBoost()
    {
        Debug.Log("Boost");
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = MousePos - (Vector2)transform.position;

        rb.AddForce(dir.normalized * 4, ForceMode2D.Impulse);

    }

    public void OnBoostAim()
    {
        Debug.Log("Aiming");
    }
    #endregion
}
