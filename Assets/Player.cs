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
    float maxSpeed, acceleration, deceleration, jumpForce, dashForce, boostForce, dashCooldown;
    int setpAngle;
    [SerializeField]
    int jumpNum;
    bool isMoving = false;
    float currSpeed = 0, dashTime = 0;
    int currJump = 0;
    Vector2 moveDirection = Vector2.zero;
    #endregion
    #region UrsulaFields
    public PelletController PelletPrefab;
    public bool Aiming;
    #endregion

    #region AvrahamFields
    public bool isGrounded = false;
    #endregion

    #endregion

    #region MonoBehaviour Functions
    private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {

        //Avraham: If Player is standing on platform with Tag "floor" ---> Do Something here.
        if (isGrounded)
        {
        
        
        }



        if (isMoving){
                Accelarate();
            }
            else if (currSpeed > 0){
                    Decelerate();
            }
            MovePlayer();

        }
		
            



         void OnCollisionEnter2D(Collision2D other) {
            currJump = 0;
        if (other.gameObject.tag == "floor")
        {
            isGrounded = true;
            Debug.Log("isGrounded = True");

        }

    }
    
	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "floor")
        {
            isGrounded = false;
            Debug.Log("isGrounded = False");
        }
    }

	#endregion

	#region Unity Callbacks

	#endregion
	#region  Actions

	#region  ItayActions

	private void MovePlayer(){
        Vector2 newPos = (Vector2)transform.position + (moveDirection * currSpeed * Time.deltaTime);
        transform.position = newPos;
    }
    private void Accelarate(){
        if (currSpeed < maxSpeed){
            currSpeed += acceleration * Time.deltaTime;
        }
        else{
            currSpeed -= acceleration * Time.deltaTime;
        }
    }    
    private void Decelerate(){
        currSpeed -= deceleration * Time.deltaTime;

        if (moveDirection == Vector2.zero && Mathf.Abs(currSpeed) < 0.05f){
            currSpeed = 0f;
        }
    }
    public void OnMove(InputAction.CallbackContext value){  Debug.Log("Move"); //WASD
        Vector2 dir = value.ReadValue<Vector2>();
        if (dir == Vector2.zero){
            isMoving = false;
        }
        else {
            if (dir != moveDirection){
                currSpeed *= -1;
            }
            moveDirection = dir;
            isMoving = true;
        }
        //TODO: Limit slope height for climb
        //TODO: Stick to vertical wall
    
    }
    
    public void OnJump(InputAction.CallbackContext value){Debug.Log("Jump"); //Space
        if (value.performed){
        //TODO: Apply ascension
        if (currJump < jumpNum){
                
            rb.AddForce(Vector2.up * (jumpForce * jumpForce), ForceMode2D.Impulse);
            currJump++;
        }
        }
        Debug.Log(currJump);
        //TODO: Apply forward momentum 
        //TODO: Adjustable Airtime
        //TODO: Adjustable Air Movment
        //TODO: Adjustable Jump Number and check limit
    }
    public void OnDash(InputAction.CallbackContext value){Debug.Log("Dash"); //Left Shift
        //TODO: Figure direction of Dash (right and left only for now)
        float now = Time.time;
        if (now - dashTime >= dashCooldown){
            currSpeed += dashForce;
            if (currSpeed < 0){
                currSpeed *= -0.5f;
            }
            dashTime = now;
        }
        //TODO: If hanging from wall dash will go up
        //TODO: Adjustable dash time and speed
        //TODO: Limit Midair Dashes
    }
    #endregion
    #region UrsulaFields
    public void OnShoot(InputAction.CallbackContext value){Debug.Log("Shoot"); //Left Mouse Button
        
        if (value.performed){
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector2 dir = MousePos - (Vector2)transform.position;

        PelletController pellet = Instantiate(PelletPrefab);
        pellet.transform.position =  transform.position;

        pellet.movement = dir.normalized;
        }

        //TODO: Locate mouse and shoot in direction
        //TODO: Instantiate and Destantiate Shot
        //TODO: Adjustable Shot Speed
        //TODO: Shot should be self propelling
    }
    #endregion

    public void OnBoost(InputAction.CallbackContext value)
    {
        Debug.Log("Boost");
        if (value.performed){
        Time.timeScale = 1f;
        Aiming = false;
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = MousePos - (Vector2)transform.position;

        rb.AddForce(dir.normalized * boostForce * boostForce , ForceMode2D.Impulse);
        }
        currSpeed *= rb.velocity.normalized.x;

    }

    public void OnBoostAim(InputAction.CallbackContext value)
    {
        if (value.performed){
        Debug.Log("Aiming");
        Aiming = true;
        Time.timeScale = 0.1f;
        }
    }
    #endregion
}
