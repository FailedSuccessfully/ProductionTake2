using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Fields
    Rigidbody2D rb;


    #region ItayFields
    [SerializeField]
    float maxSpeed, acceleration, deceleration, stepHeight, jumpForce, airSpeed;
    [SerializeField]
    int jumpNum;
    bool isMoving = false;
    Vector2 moveDirection = Vector2.zero;
    #endregion
    #region UrsulaFields
    #endregion
    #endregion

    #region MonoBehaviour Functions
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            
        }
    #endregion

    #region  Actions

    #region  ItayActions

    private void MovePlayer(){

    }
    public void OnMove(InputAction.CallbackContext value){  Debug.Log("Move"); //WASD
        isMoving = !isMoving;
        
        Debug.Log(value.ReadValue<Vector2>());
        //TODO: Apply movement - Start on first callback stop on second callback
        //TODO: Adjuststable Accelaration
        //TODO: Max speed
        //TODO: Adjustable Decelaration
        //TODO: Limit slope height for climb
        //TODO: Stick to vertical wall
    
    }
    public void OnJump(InputAction.CallbackContext value){Debug.Log("Jump"); //Space
        //TODO: Apply ascension
        //TODO: Apply forward momentum 
        //TODO: Adjustable Airtime
        //TODO: Adjustable Air Movment
        //TODO: Adjustable Jump Number and check limit
    }
    public void OnDash(InputAction.CallbackContext value){Debug.Log("Dash"); //Left Shift
        //TODO: Figure direction of Dash (right and left only for now)
        //TODO: If hanging from wall dash will go up
        //TODO: Adjustable dash time and speed
        //TODO: Limit Midair Dashes
    }
    #endregion
    #region UrsulaFields
    public void OnShoot(){Debug.Log("Shoot"); //Left Mouse Button
        //TODO: Locate mouse and shoot in direction
        //TODO: Instantiate and Destantiate Shot
        //TODO: Adjustable Shot Speed
        //TODO: Shot should be self propelling
    }
    #endregion
    #endregion
}
