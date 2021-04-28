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
    float currSpeed = 0;
    Vector2 moveDirection = Vector2.zero;
    #endregion
    #region UrsulaFields
    public PelletController PelletPrefab;
    #endregion
    #endregion

    #region MonoBehaviour Functions
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (isMoving){
                Accelarate();
            }
            else if (currSpeed > 0){
                    Decelerate();
            }
            MovePlayer();
        }
    #endregion

    #region Unity Callbacks



    #endregion
    #region  Actions

    #region  ItayActions

    private void MovePlayer(){
        Vector2 newPos = (Vector2)transform.position + (moveDirection * currSpeed * Time.deltaTime);
        transform.position = newPos;

        if (currSpeed < 0){
            moveDirection = Vector2.zero;
            currSpeed = 0;
        }
    }
    private void Accelarate(){
        if (currSpeed < maxSpeed){
            currSpeed += acceleration * Time.deltaTime;
        }
    }    
    private void Decelerate(){
        currSpeed -= deceleration * Time.deltaTime;
    }
    public void OnMove(InputAction.CallbackContext value){  Debug.Log("Move"); //WASD
        Vector2 dir = value.ReadValue<Vector2>();
        if (dir != Vector2.zero){
            moveDirection = dir;
            isMoving = true;
        }
        else {
            isMoving = false;
        }
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
